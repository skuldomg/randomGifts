using StardewModdingAPI;
using StardewValley;
using StardewModdingAPI.Events;
using StardewModdingAPI.Utilities;
using System;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;

namespace randomSchedules
{
    public class ModEntry : Mod
    {
        private ModConfig config;

        public override void Entry(IModHelper helper)
        {
            this.config = this.Helper.ReadConfig<ModConfig>();            

            TimeEvents.AfterDayStarted += AfterDayStarted;
        }

        private void AfterDayStarted(object sender, EventArgs e)
        {
            DisposableList<NPC> allNPCs = Utility.getAllCharacters();

            Dictionary<String, String> abbySchedule = this.generateSchedule("Abigail");

            this.Monitor.Log("Abby's schedule: ");

            foreach(KeyValuePair<String, String> kvp in abbySchedule)            
                this.Monitor.Log(kvp.Key + ": " + kvp.Value);
            
/*
            this.Monitor.Log("All schedules: ");

            foreach(NPC npc in allNPCs)
            {
                this.Monitor.Log(npc.Name + "'s schedule: ");

                if (npc.Schedule != null)
                {
                    foreach (KeyValuePair<int, SchedulePathDescription> kvp in npc.Schedule)
                    {
                        this.Monitor.Log(kvp.Key + " --- " + kvp.Value.ToString());
                    }
                }
                else
                    this.Monitor.Log("Don't do shit");

                this.Monitor.Log("\n\n");
            }*/
        }

        // Generates a complete schedule for a character
        private Dictionary<String, String> generateSchedule(String name)
        {
            Dictionary<String, String> theSchedule = new Dictionary<String, String>();

            // Always generate a full week
            List<String> weekDays = new List<String>();
            weekDays.Add("Mon"); weekDays.Add("Tue"); weekDays.Add("Wed"); weekDays.Add("Thu"); weekDays.Add("Fri"); weekDays.Add("Sat"); weekDays.Add("Sun");

            for(int i=0; i<weekDays.Count; i++)
                theSchedule.Add(weekDays.ElementAt(i), generateDay("Abigail"));

            // TODO?: In addition, generate a couple of random dates (1-28) and season dates (winter_12, fall_Mon, ...) .. or is that even necessary?     

            // TODO: Make sure shop owners stay at their shop during proper times
            // TODO: Preserve marriage schedules
            switch(name)
            {
                case "Abigail":
                    theSchedule.Add("marriage_Mon", "830 SeedShop 6 19 0 \"Strings\\schedules\\Abigail:marriage_Mon.000\"/1300 Town 47 87 0 \"Strings\\schedules\\Abigail:marriage_Mon.001\"/1700 Saloon 33 18 0 \"Strings\\schedules\\Abigail:marriage_Mon.002\"/2030 BusStop -1 23 3");
                    theSchedule.Add("marriage_Fri", "800 SeedShop 36 9 0 \"Strings\\schedules\\Abigail:marriage_Fri.000\"/1100 Mountain 49 31 2 abigail_flute/1500 Saloon 42 17 2 abigail_sit_down \"Strings\\schedules\\Abigail:marriage_Fri.001\"/2030 BusStop -1 23 3");
                    break;

                case "Alex":
                    theSchedule.Add("marriage_Mon", "830 JoshHouse 6 18 2 \"Strings\\schedules\\Alex:marriage_Mon.000\"/1800 BusStop -1 23 3");
                    break;
            }

            return theSchedule;            
        }

        // Generates a randomized schedule day
        private String generateDay(String name)
        {            
            String theSchedule = "";
            Random rnd = new Random();

            // A single schedule move event looks like this:
            // time map         x   y   faceDirection   [animation]      where animation is optional
            // 900  SeedShop    39  5   0               abigail_flute

            // First decide how many different elements the day's schedule should have
            int dayEvents = Game1.random.Next(2, 5);

            /*if (config.debug)
                this.Monitor.Log("Character will have " + dayEvents + " different events.");*/

            // Now generate different times, locations, positions and directions
            List<int> times = new List<int>();
            IList<GameLocation> allLocs = Game1.locations;
            List<string> locations = new List<string>();
            List<Vector2> positions = new List<Vector2>();
            List<int> directions = new List<int>();

            // Remove completely implausible locations
            for(int i=0; i<allLocs.Count; i++)
            {
                switch(allLocs.ElementAt(i).Name)
                {
                    case "BeachNightMarket":
                    case "BuildableGameLocation":
                    case "Bus":
                    case "Cabin":
                    case "Cellar":
                    case "Club":
                    case "DecoratableLocation":
                    case "DecorationFacade":
                    case "FarmCave":
                    case "FarmHouse":
                    case "LocationFarmers":
                    case "MermaidHouse":
                    case "MineInfo":
                    case "MineShaft":
                    case "Sewer":
                    case "Submarine":
                    case "Summit":
                    case "Farm": // TODO: Maybe at a very low chance?                    
                    case "Tent": // TODO: Only for Linus
                    case "Mine": // TODO: Only for certain characters?
                    case "BugLand":
                    case "Desert": // TODO: Only for Sandy, maybe Emily?
                    case "SandyHouse": // TODO: Only for Emily, very low chance for others?
                    case "WizardHouseBasement": // TODO: Low Wizard chance?
                    case "WitchSwamp":
                    case "WitchHut":
                    case "WitchWarpCave":
                    case "Greenhouse":
                    case "SkullCave":
                    case "Tunnel": // TODO: Low chance for adventurous guys?
                    case "Trailer_Big": // TODO: Check for Pam upgrade?
                        allLocs.RemoveAt(i);
                        i--;
                        break;
                }
            }

            bool isMale = true;

            switch(name)
            {
                case "Abigail":
                case "Caroline":
                case "Emily":
                case "Evelyn":
                case "Haley":
                case "Jas":
                case "Jodi":
                case "Leah":
                case "Marnie":
                case "Maru":
                case "Pam":
                case "Penny":
                case "Robin":
                    isMale = false;
                    break;

            }
            
            // Remove Mens/Womens locker when appropriate
            for(int i=0; i<allLocs.Count; i++)
            {
                if (allLocs.ElementAt(i).Name.Equals("BathHouse_WomensLocker") && isMale)
                    allLocs.RemoveAt(i);

                if (allLocs.ElementAt(i).Name.Equals("BathHouse_MensLocker") && !isMale)
                    allLocs.RemoveAt(i);
            }
            
            /*
                        if(config.debug)
                        {
                            this.Monitor.Log("Possible locations: ");

                            foreach (GameLocation l in allLocs)
                                this.Monitor.Log(l.Name);
                        }
            */

            // TODO: Add a go to home event for every character at 2400
            // TODO: Make sure times are properly spaced out            
            // TODO: Remove time/location combinations that are not plausible and configure per-character plausible locations
            // TODO: Check what character we generate for to do special shit (animations, etc)
            for (int i=0; i<dayEvents; i++)
            {
                // Make sure that the first time is between 7 and 10
                if (i == 0)
                    times.Add(Game1.random.Next(7, 10));
                else
                {                    
                    int t = Game1.random.Next(times.ElementAt(0) + 1, 23);

                    // Do not add duplicate times
                    while (times.Contains(t))
                        t = Game1.random.Next(times.ElementAt(0) + 1, 23);                    

                    times.Add(t);
                }

                locations.Add(allLocs.ElementAt(Game1.random.Next(0, allLocs.Count)).Name);
                positions.Add(getPositionOnMap(locations.ElementAt(i)));
                directions.Add(Game1.random.Next(0, 4));
            }

            // Sort the times in ascending order
            times = times.OrderBy(i => i).ToList();

            // Pierce together today's events
            for(int i=0; i<dayEvents; i++)
            {
                theSchedule += times.ElementAt(i) + "00 " + locations.ElementAt(i) + " " + positions.ElementAt(i).X + " " + positions.ElementAt(i).Y + " " + directions.ElementAt(i);

                // Sometimes add an animation
                int animChance = Game1.random.Next(1, 100);
                int sleepTime = Game1.random.Next(times.ElementAt(times.Count - 1), 24);

                switch (name)
                {                    
                    // TODO: videogames / sit_down animation (must be checked for being appropriate)
                    case "Abigail":
                        // Originally she seems to have about 1.5% flute animation chance (in 2 out of 55 single events)
                        // That seems a bit harsh, let's go with about 10%
                        if (animChance <= 10)
                            theSchedule += " abigail_flute";

                        // Add a day end event
                        if(i == (dayEvents - 1))
                            theSchedule += "/"+sleepTime+"00 SeedShop 1 9 3";
                        break;

                    case "Alex":
                        if (animChance <= 11)
                            theSchedule += " alex_football";
                        else if (animChance >= 86)
                            theSchedule += " alex_lift_weights";

                        if (i == (dayEvents - 1))
                            theSchedule += "/" + sleepTime + "00 JoshHouse 21 4 1";
                        break;
                }

                if (i != (dayEvents - 1))
                    theSchedule += "/";
            }

            
            if (config.debug) {
                /*this.Monitor.Log("Times are: ");
                foreach(int i in times)
                    this.Monitor.Log(i.ToString());            
            
                this.Monitor.Log("Locations are: ");
                foreach(string s in locations)
                    this.Monitor.Log(s);

                this.Monitor.Log("Positions are: ");
                foreach (Vector2 v in positions)
                    this.Monitor.Log(v.X + "/" + v.Y);

                this.Monitor.Log("Directions are: ");
                foreach (int i in directions)
                    this.Monitor.Log(i.ToString());
                this.Monitor.Log("One day's schedule: " + theSchedule);*/
            }

            return theSchedule;
        }

        // Generates a valid position for the given map
        private Vector2 getPositionOnMap(string map)
        {
            GameLocation theLoc = Game1.getLocationFromName(map);
            Vector2 thePos = theLoc.getRandomTile();

            bool posFound = false;

            while(!posFound)
            {
                switch (map)
                {
                    // TODO: Edit a bit to more plausible locations for a human
                    case "Town":
                        if ((thePos.X >= 12 && thePos.X <= 65) && (thePos.Y >= 10 && thePos.Y <= 39))
                            posFound = true;
                        else if ((thePos.X >= 4 && thePos.X <= 110) && (thePos.Y >= 54 && thePos.Y <= 96))
                            posFound = true;

                        if (posFound && ((thePos.X >= 88 && thePos.X <= 106) && thePos.Y >= 63 && thePos.Y <= 75))
                            posFound = false;
                        break;

                    default:
                        posFound = true;
                        break;
                }

                thePos = theLoc.getRandomTile();
            }            

            return thePos;
        }
    }
}
