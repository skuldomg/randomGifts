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
        public List<String> weekDays = new List<String>();
        public String workOutDay = "";

        public override void Entry(IModHelper helper)
        {
            this.config = this.Helper.ReadConfig<ModConfig>();            

            TimeEvents.AfterDayStarted += AfterDayStarted;
        }

        private void AfterDayStarted(object sender, EventArgs e)
        {
            DisposableList<NPC> allNPCs = Utility.getAllCharacters();

            weekDays.Add("Mon"); weekDays.Add("Tue"); weekDays.Add("Wed"); weekDays.Add("Thu"); weekDays.Add("Fri"); weekDays.Add("Sat"); weekDays.Add("Sun");
            // Generate work out day, everyone's favorite!
            workOutDay = weekDays.ElementAt(Game1.random.Next(0, 6));

            if (config.debug)
                this.Monitor.Log("Work out day is: " + workOutDay);

            Dictionary<String, String> abbySchedule = this.generateSchedule("Abigail");
            Dictionary<String, String> alexSchedule = this.generateSchedule("Alex");
            Dictionary<String, String> carolineSchedule = this.generateSchedule("Caroline");
            Dictionary<String, String> clintSchedule = this.generateSchedule("Clint");
            Dictionary<String, String> demetriusSchedule = this.generateSchedule("Demetrius");
            Dictionary<String, String> elliottSchedule = this.generateSchedule("Elliott");


            this.Monitor.Log("Abby's schedule: ");

            foreach(KeyValuePair<String, String> kvp in abbySchedule)            
                this.Monitor.Log(kvp.Key + ": " + kvp.Value);

            this.Monitor.Log("\nAlex' schedule: ");

            foreach (KeyValuePair<String, String> kvp in alexSchedule)
                this.Monitor.Log(kvp.Key + ": " + kvp.Value);

            this.Monitor.Log("\nCaroline's schedule: ");

            foreach (KeyValuePair<String, String> kvp in carolineSchedule)
                this.Monitor.Log(kvp.Key + ": " + kvp.Value);

            this.Monitor.Log("\nClint's schedule: ");

            foreach (KeyValuePair<String, String> kvp in clintSchedule)
                this.Monitor.Log(kvp.Key + ": " + kvp.Value);

            this.Monitor.Log("\nDemetrius' schedule: ");

            foreach (KeyValuePair<String, String> kvp in demetriusSchedule)
                this.Monitor.Log(kvp.Key + ": " + kvp.Value);

            this.Monitor.Log("\nElliot's schedule: ");

            foreach (KeyValuePair<String, String> kvp in elliottSchedule)
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
        // TODO: Start day, end day
        private Dictionary<String, String> generateSchedule(String name)
        {
            Dictionary<String, String> theSchedule = new Dictionary<String, String>();           

            // Always generate a full week
            for (int i = 0; i < weekDays.Count; i++)
            {
                this.Monitor.Log("Generating for week day " + i);

                switch(name)
                {
                    case "Clint":
                        theSchedule.Add(weekDays.ElementAt(i), generateDay(name, 16, 23, Game1.random.Next(1, 3)));
                        break;

                    default:
                        theSchedule.Add(weekDays.ElementAt(i), generateDay(name));
                        break;
                }                
            }

            // TODO?: In addition, generate a couple of random dates (1-28) and season dates (winter_12, fall_Mon, ...) .. or is that even necessary?               
            // -> for now just randomize hospital appointments

            int dow = Game1.random.Next(1, 28);
            String season = "";
            int s = Game1.random.Next(1, 4);

            switch(s) {
                case 1:
                    season = "spring";
                    break;
                case 2:
                    season = "summer";
                    break;
                case 3:
                    season = "fall";
                    break;
                case 4:
                    season = "winter";
                    break;
            }

            String hospitalDay = season + "_" + dow;

            // TODO: Preserve marriage schedules and special schedules
            switch(name)
            {
                case "Abigail":
                    theSchedule.Add("marriage_Mon", "830 SeedShop 6 19 0 \"Strings\\schedules\\Abigail:marriage_Mon.000\"/1300 Town 47 87 0 \"Strings\\schedules\\Abigail:marriage_Mon.001\"/1700 Saloon 33 18 0 \"Strings\\schedules\\Abigail:marriage_Mon.002\"/2030 BusStop -1 23 3");
                    theSchedule.Add("marriage_Fri", "800 SeedShop 36 9 0 \"Strings\\schedules\\Abigail:marriage_Fri.000\"/1100 Mountain 49 31 2 abigail_flute/1500 Saloon 42 17 2 abigail_sit_down \"Strings\\schedules\\Abigail:marriage_Fri.001\"/2030 BusStop -1 23 3");
                    theSchedule.Add(hospitalDay, "900 SeedShop 11 5 0 \"Strings\\schedules\\Abigail:spring_4.000\"/1230 Hospital 13 14 0 \"Strings\\schedules\\Abigail:spring_4.001\"/1330 Hospital 4 6 1 \"Strings\\schedules\\Abigail:spring_4.002\"/1600 SeedShop 10 5 0/2000 SeedShop 1 9 3");
                    break;

                case "Alex":
                    theSchedule.Add("marriage_Mon", "830 JoshHouse 6 18 2 \"Strings\\schedules\\Alex:marriage_Mon.000\"/1800 BusStop -1 23 3");
                    theSchedule.Add(hospitalDay, "800 Town 64 64 2 alex_football/1030 Hospital 15 15 3 alex_sit_left \"Strings\\schedules\\Alex:summer_16.000\"/1330 Hospital 4 6 1 \"Strings\\schedules\\Alex:summer_16.001\"/1600 JoshHouse 15 4 2");
                    break;

                case "Caroline":
                    theSchedule.Remove(workOutDay);
                    theSchedule.Add(workOutDay, "800 SeedShop 22 14 0 \"Strings\\schedules\\Caroline:Tue.000\"/1030 SeedShop 24 17 3/1300 SeedShop 24 21 0 caroline_exercise/1600 SeedShop 23 15 1 \"Strings\\schedules\\Caroline:Tue.001\"/1810 SeedShop 34 5 0 \"Strings\\schedules\\Caroline:Tue.002\"/2100 SeedShop 25 4 1");
                    theSchedule.Add(hospitalDay, "800 SeedShop 37 6 3/1000 SeedShop 27 7 3 \"Strings\\schedules\\Caroline:fall_25.000\"/1200 Hospital 13 14 0 \"Strings\\schedules\\Caroline:fall_25.001\"/1330 Hospital 4 6 1 \"Strings\\schedules\\Caroline:fall_25.002\"/1600 SeedShop 25 18 2 square_9_9/2100 SeedShop 25 4 1");
                    break;

                case "Clint":
                    theSchedule.Add(hospitalDay, "900 Blacksmith 3 13 2 \"Strings\\schedules\\Clint:winter_16.000\"/1030 Hospital 12 14 0 \"Strings\\schedules\\Clint:winter_16.001\"/1330 Hospital 4 6 1 \"Strings\\schedules\\Clint:winter_16.002\"/1600 Saloon 19 23 3/2400 Blacksmith 10 4 1");
                    break;

                case "Demetrius":
                    theSchedule.Add(hospitalDay, "820 Hospital 12 14 0 \"Strings\\schedules\\Demetrius:summer_25.000\"/1330 Hospital 4 6 1 \"Strings\\schedules\\Demetrius:summer_25.001\"/1600 ScienceHouse 15 4 0/2200 ScienceHouse 19 4 1");
                    break;

                case "Elliott":
                    theSchedule.Add("marriage_Mon", "830 Beach 46 11 2 \"Strings\\schedules\\Elliott:marriage_Mon.000\"/1700 BusStop -1 23 3");
                    theSchedule.Add(hospitalDay, "900 ElliottHouse 1 5 0/1030 Hospital 12 14 0 \"Strings\\schedules\\Elliott:summer_9.000\"/1330 Hospital 4 6 1 \"Strings\\schedules\\Elliott:summer_9.001\"/1600 ElliottHouse 7 7 2/2100 ElliottHouse 1 5 0/2400 ElliottHouse 13 4 1");
                    break;
            }

            return theSchedule;            
        }

        // Generates a randomized schedule day
        private String generateDay(String name, int startTime = -1, int endTime = -1, int noOfEvents = -1)
        {            
            String theSchedule = "";
            Random rnd = new Random();

            // A single schedule move event looks like this:
            // time map         x   y   faceDirection   [animation]      where animation is optional
            // 900  SeedShop    39  5   0               abigail_flute

            // First decide how many different elements the day's schedule should have
            int dayEvents;

            // TODO: Just do noOfEvents = Game1.random.Next(2, 5) in the constructor
            if (noOfEvents == -1)
                dayEvents = Game1.random.Next(2, 5);
            else
                dayEvents = noOfEvents;

            if (config.debug)
                this.Monitor.Log("Character will have " + dayEvents + " different events.");

            // Now generate different times, locations, positions and directions
            List<int> times = new List<int>();            
            List<string> locations = new List<string>();
            List<Vector2> positions = new List<Vector2>();
            List<int> directions = new List<int>();

            // Get all location names
            List<String> allLocs = new List<string>();
            foreach(GameLocation loc in Game1.locations)
                allLocs.Add(loc.Name);

            this.Monitor.Log("Size of Game1.locations: " + allLocs.Count + " (before removing)");

            // Remove completely implausible locations
            for(int i=0; i<allLocs.Count; i++)
            {
                SDate date = SDate.Now();
                SDate earthquake = new SDate(3, "summer", 1);

                switch (allLocs.ElementAt(i))
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
                    case "Woods":
                    case "CommunityCenter": // TODO: Check if community center is restored
                    case "JojaMart": // TODO: Check if community center is restored                    
                        allLocs.RemoveAt(i);
                        i--;
                        break;

                    // Remove locations that are implausible character-wise
                    case "JoshHouse":
                        if (!name.Equals("Alex") && !name.Equals("Evelyn") && !name.Equals("George"))
                        {                            
                            allLocs.RemoveAt(i);
                            i--;
                        }
                        break;

                    case "HaleyHouse":
                        if(!name.Equals("Haley") && !name.Equals("Emily"))
                        {                            
                            allLocs.RemoveAt(i);
                            i--;
                        }
                        break;

                    case "SamHouse":
                        if(!name.Equals("Sam") && !name.Equals("Jodie") && !name.Equals("Kent") && !name.Equals("Vincent"))
                        {                            
                            allLocs.RemoveAt(i);
                            i--;
                        }
                        break;

                    case "ManorHouse":
                        if(!name.Equals("Lewis") && !name.Equals("Marnie"))
                        {
                            allLocs.RemoveAt(i);
                            i--;
                        }
                        break;

                    // TODO: Check if Pam's house is built
                    case "Trailer":
                        if(!name.Equals("Pam") && !name.Equals("Penny"))
                        {                            
                            allLocs.RemoveAt(i);
                            i--;
                        }
                        break;

                    case "HarveyRoom":
                        if(!name.Equals("Harvey"))
                        {                            
                            allLocs.RemoveAt(i);
                            i--;
                        }
                        break;

                    case "ElliottHouse":
                        if(!name.Equals("Elliott"))
                        {
                            allLocs.RemoveAt(i);
                            i--;
                        }
                        break;

                    case "ScienceHouse":
                        if(!name.Equals("Maru") && !name.Equals("Sebastian") && !name.Equals("Demetrius") && !name.Equals("Robin"))
                        {
                            allLocs.RemoveAt(i);
                            i--;
                        }
                        break;

                    case "SebastianRoom":
                        if(!name.Equals("Maru") && !name.Equals("Sebastian"))
                        {
                            allLocs.RemoveAt(i);
                            i--;
                        }
                        break;

                    case "Tent":
                        if(!name.Equals("Linus"))
                        {
                            allLocs.RemoveAt(i);
                            i--;
                        }
                        break;

                    case "WizardHouse":
                        if(!name.Equals("Wizard") && !name.Equals("Abigail"))
                        {
                            allLocs.RemoveAt(i);
                            i--;
                        }
                        break;

                    case "LeahHouse":
                        if(!name.Equals("Leah") && !name.Equals("Marnie"))
                        {
                            allLocs.RemoveAt(i);
                            i--;
                        }
                        break;

                    case "AdventureGuild":
                        if(!name.Equals("Abigail"))
                        {
                            allLocs.RemoveAt(i);
                            i--;
                        }
                        break;

                    // Remove these if it's still before the earthquake (Summer 3, Year 1)
                    case "Railroad":
                    case "BathHouse_Entry":
                    case "BathHouse_Pool":                                 
                        if(date < earthquake)
                        {                            
                            allLocs.RemoveAt(i);
                            i--;
                        }
                        break;

                    case "BathHouse_MensLocker":                        
                        if(date < earthquake)
                        {                            
                            allLocs.RemoveAt(i);
                            i--;
                        }

                        if (!name.Equals("Alex") && !name.Equals("Elliott") && !name.Equals("Harvey") && !name.Equals("Sam") && !name.Equals("Sebastian") && !name.Equals("Shane") && !name.Equals("Clint") && !name.Equals("Demetrius") && !name.Equals("Gus") && !name.Equals("Kent") && !name.Equals("Lewis") && !name.Equals("Linus") && !name.Equals("Pierre") && !name.Equals("Vincent") && !name.Equals("Willy") && !name.Equals("Wizard"))
                        {
                            allLocs.RemoveAt(i);
                            i--;
                        }
                        break;

                    case "BathHouse_WomensLocker":
                        if(date < earthquake)
                        {                            
                            allLocs.RemoveAt(i);
                            i--;
                        }

                        if(!name.Equals("Abigail") && !name.Equals("Emily") && !name.Equals("Haley") && !name.Equals("Leah") && !name.Equals("Maru") && !name.Equals("Penny") && !name.Equals("Caroline") && !name.Equals("Jas") && !name.Equals("Jodi") && !name.Equals("Marnie") && !name.Equals("Pam") && !name.Equals("Robin"))
                        {                            
                            allLocs.RemoveAt(i);
                            i--;
                        }
                        break;
                }
            }

            this.Monitor.Log("Size of Game1.locations: " + allLocs.Count + " (after removing)");

            /*  if(config.debug)
              {
                  this.Monitor.Log("Possible locations: ");

                  foreach (GameLocation l in allLocs)
                      this.Monitor.Log(l.Name);
              }*/


            // TODO: Make sure times are properly spaced out  
            for (int i=0; i<dayEvents; i++)
            {
                int theTime = -1;
                String theLoc = "";

                if (startTime == -1 || endTime == -1) {
                    // Make sure that the first time is between 7 and 10
                    if (i == 0)
                        //times.Add(Game1.random.Next(7, 10));
                        theTime = Game1.random.Next(7, 10);
                    else
                    {
                        int t = Game1.random.Next(times.ElementAt(0) + 1, 23);

                        // Do not add duplicate times
                        while (times.Contains(t))
                            t = Game1.random.Next(times.ElementAt(0) + 1, 23);

                        //times.Add(t);
                        theTime = t;
                    }
                }
                else
                {
                    int t = Game1.random.Next(startTime, endTime);

                    // Do not add duplicate times
                    while (times.Contains(t))
                        t = Game1.random.Next(startTime, endTime);

                    //times.Add(t);
                    theTime = t;
                }

                //locations.Add(allLocs.ElementAt(Game1.random.Next(0, allLocs.Count)).Name);
                theLoc = allLocs.ElementAt(Game1.random.Next(0, allLocs.Count));

                // TODO: Remove time/location combinations that are not plausible and configure per-character plausible locations
                // TODO: Combine locations and follow-up locations -> bathhouse entry -> bathhouse locker -> bathhouse pool

                times.Add(theTime);
                locations.Add(theLoc);
                positions.Add(getPositionOnMap(locations.ElementAt(i)));
                directions.Add(Game1.random.Next(0, 4));
            }

            // Sort the times in ascending order
            times = times.OrderBy(i => i).ToList();

            // For shop owners, first define their shop times
            switch (name)
            {
                case "Clint":
                    theSchedule += "900 Blacksmith 3 13 2/";
                    break;
            }

            // Pierce together today's events
            for (int i=0; i<dayEvents; i++)
            { 
                theSchedule += times.ElementAt(i) + "00 " + locations.ElementAt(i) + " " + positions.ElementAt(i).X + " " + positions.ElementAt(i).Y + " " + directions.ElementAt(i);

                // TODO: bool isOutside = isLocationOutside(locations.ElementAt(i)); --> for cross checking animations like football/flute etc

                // Sometimes add an animation
                int animChance = Game1.random.Next(1, 100);
                int sleepTime = Game1.random.Next(times.ElementAt(times.Count - 1)+1, 24);

                switch (name)
                {
                    // TODO: Check what character we generate for to do special shit (animations, etc)
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

                    // TODO: Check if _read animation is sitting down
                    case "Caroline":
                        if (animChance <= 5)
                            theSchedule += " caroline_read";

                        if (i == (dayEvents - 1))
                            theSchedule += "/" + sleepTime + "00 SeedShop 25 4 1";
                        break;

                    // TODO: _hammer animation
                    case "Clint":
                        if (i == (dayEvents - 1))
                            theSchedule += "/" + sleepTime + "00 Blacksmith 10 4 1";
                        break;

                        // TODO: _dance in the Saloon
                    case "Demetrius":
                        if (animChance <= 5)
                            theSchedule += " demetrius_read";
                        else if (animChance >= 88)
                            theSchedule += " demetrius_notes";

                        if (i == (dayEvents - 1))
                            theSchedule += "/" + sleepTime + "00 ScienceHouse 19 4 11";
                        break;

                        // TODO: _drink animation (Saloon)
                    case "Elliott":
                        if (animChance <= 5)
                            theSchedule += " elliott_read";

                        if (i == (dayEvents - 1))
                            theSchedule += "/" + sleepTime + "00 ElliottHouse 13 4 1";
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
                    // TODO: Check if occupied
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
