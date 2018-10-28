using StardewModdingAPI;
using StardewValley;
using StardewModdingAPI.Events;
using StardewModdingAPI.Utilities;
using System;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;

// TODO: Randomize the random functions more (ie. with steps taken, days passed, current day, or something)

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
            weekDays.Add("Mon"); weekDays.Add("Tue"); weekDays.Add("Wed"); weekDays.Add("Thu"); weekDays.Add("Fri"); weekDays.Add("Sat"); weekDays.Add("Sun");

            TimeEvents.AfterDayStarted += AfterDayStarted;            
        }

        private void AfterDayStarted(object sender, EventArgs e)
        {
            DisposableList<NPC> allNPCs = Utility.getAllCharacters();
            
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
            Dictionary<String, String> emilySchedule = this.generateSchedule("Emily");
            Dictionary<String, String> evelynSchedule = this.generateSchedule("Evelyn");
            Dictionary<String, String> georgeSchedule = this.generateSchedule("George");
            Dictionary<String, String> jasSchedule = this.generateSchedule("Jas");
            Dictionary<String, String> jodiSchedule = this.generateSchedule("Jodi");
            Dictionary<String, String> kentSchedule = this.generateSchedule("Kent");
            Dictionary<String, String> leahSchedule = this.generateSchedule("Leah");
            Dictionary<String, String> lewisSchedule = this.generateSchedule("Lewis");
            Dictionary<String, String> linusSchedule = this.generateSchedule("Linus");
            Dictionary<String, String> marnieSchedule = this.generateSchedule("Marnie");
            Dictionary<String, String> maruSchedule = this.generateSchedule("Maru");
            Dictionary<String, String> pamSchedule = this.generateSchedule("Pam");

            /* this.Monitor.Log("Abby's schedule: ");

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

             this.Monitor.Log("\nEmily's schedule: ");

             foreach (KeyValuePair<String, String> kvp in emilySchedule)
                 this.Monitor.Log(kvp.Key + ": " + kvp.Value);

             this.Monitor.Log("\nEvelyn's schedule: ");

             foreach (KeyValuePair<String, String> kvp in evelynSchedule)
                 this.Monitor.Log(kvp.Key + ": " + kvp.Value);

             this.Monitor.Log("\nGeorge's schedule: ");

             foreach (KeyValuePair<String, String> kvp in georgeSchedule)
                 this.Monitor.Log(kvp.Key + ": " + kvp.Value);

            this.Monitor.Log("\nJas' schedule: ");

            foreach (KeyValuePair<String, String> kvp in jasSchedule)
                this.Monitor.Log(kvp.Key + ": " + kvp.Value);

            this.Monitor.Log("\nJodi's schedule: ");

            foreach (KeyValuePair<String, String> kvp in jodiSchedule)
                this.Monitor.Log(kvp.Key + ": " + kvp.Value);

            this.Monitor.Log("\nKent's schedule: ");

            foreach (KeyValuePair<String, String> kvp in kentSchedule)
                this.Monitor.Log(kvp.Key + ": " + kvp.Value);

            this.Monitor.Log("\nLeah's schedule: ");

            foreach (KeyValuePair<String, String> kvp in leahSchedule)
                this.Monitor.Log(kvp.Key + ": " + kvp.Value);

            this.Monitor.Log("\nLewis' schedule: ");

            foreach (KeyValuePair<String, String> kvp in lewisSchedule)
                this.Monitor.Log(kvp.Key + ": " + kvp.Value);

            this.Monitor.Log("\nLinus' schedule: ");

            foreach (KeyValuePair<String, String> kvp in linusSchedule)
                this.Monitor.Log(kvp.Key + ": " + kvp.Value);

            this.Monitor.Log("\nMarnie's schedule: ");

            foreach (KeyValuePair<String, String> kvp in marnieSchedule)
                this.Monitor.Log(kvp.Key + ": " + kvp.Value);

            this.Monitor.Log("\nMaru's schedule: ");

            foreach (KeyValuePair<String, String> kvp in maruSchedule)
                this.Monitor.Log(kvp.Key + ": " + kvp.Value);*/

            this.Monitor.Log("\nPam's schedule: ");

            foreach (KeyValuePair<String, String> kvp in pamSchedule)
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
                //this.Monitor.Log("Generating for week day " + i);

                switch(name)
                {
                    case "Clint":
                        theSchedule.Add(weekDays.ElementAt(i), generateDay("Clint", 16, 23, Game1.random.Next(1, 3)));
                        break;

                    case "Marnie":
                        // Marnie doesn't work Mondays and Thursdays. Lazy bitch.
                        if (weekDays.ElementAt(i).Equals("Mon") || weekDays.ElementAt(i).Equals("Thu"))
                            theSchedule.Add(weekDays.ElementAt(i), generateDay("Marnie"));
                        else
                            theSchedule.Add(weekDays.ElementAt(i), generateDay("Marnie", 16, 23, Game1.random.Next(1, 3)));
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

            // TODO: Make sure no two people have the same hospital day
            String hospitalDay = season + "_" + dow;

            // TODO: Preserve marriage schedules and special schedules
            // TODO: Presence at the beach market
            // TODO: rain schedules
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

                case "Emily":
                    theSchedule.Add("marriage_Mon", "830 HaleyHouse 12 17 1 \"Strings\\schedules\\Emily:marriage_Mon.000\"/1100 Town 82 15 1 \"Strings\\schedules\\Emily:marriage_Mon.001\"/1500 Saloon 15 17 0 \"Strings\\schedules\\Emily:marriage_Mon.002\"/2200 BusStop -1 23 3");
                    theSchedule.Add("marriage_Fri", "830 Beach 10 37 3 \"Strings\\schedules\\Emily:marriage_Fri.000\"/1300 SeedShop 2 26 2 \"Strings\\schedules\\Emily:marriage_Fri.001\"/1500 Saloon 15 17 0 \"Strings\\schedules\\Emily:marriage_Fri.002\"/2200 BusStop -1 23 3");
                    theSchedule.Remove(workOutDay);
                    theSchedule.Add(workOutDay, "900 HaleyHouse 16 5 2/1000 SeedShop 27 16 2/1300 SeedShop 26 19 2 emily_exercise/1600 Saloon 15 17 2 square_5_1_0/2430 HaleyHouse 21 4 1");
                    theSchedule.Add(hospitalDay, "900 HaleyHouse 16 5 2/1030 Hospital 12 14 0 \"Strings\\schedules\\Emily:winter_11.000\"/1330 Hospital 4 6 1 \"Strings\\schedules\\Emily:winter_11.001\"/1600 Saloon 15 17 0 square_5_1_0/2430 HaleyHouse 21 4 1");
                    break;

                case "Evelyn":
                    // Evelyn goes to the hospital once a month
                    hospitalDay = hospitalDay.Substring(hospitalDay.IndexOf("_") + 1);
                    theSchedule.Add(hospitalDay, "800 JoshHouse 3 17 0 square_3_1_0/1040 Hospital 10 16 0 \"Strings\\schedules\\Evelyn:23.000\"/1330 Hospital 4 7 0 \"Strings\\schedules\\Evelyn:23.001\"/1610 JoshHouse 4 17 0 square_3_1_0/1900 JoshHouse 17 17 0/2130 JoshHouse 2 5 1");
                    break;

                case "George":
                    // George goes to the hospital once a month
                    hospitalDay = hospitalDay.Substring(hospitalDay.IndexOf("_") + 1);
                    theSchedule.Add(hospitalDay, "630 JoshHouse 5 21 3 \"Strings\\schedules\\George:23.000\"/1030 Hospital 10 15 0 \"Strings\\schedules\\George:23.001\"/1330 Hospital 4 6 1 \"Strings\\schedules\\George:23.002\"/1600 JoshHouse 16 22 0/2000 JoshHouse 3 5 0");
                    break;

                case "Jas":
                    theSchedule.Add(hospitalDay, "900 AnimalShop 7 5 0 \"Strings\\schedules\\Jas:winter_18.000\"/1030 Hospital 12 14 0/1330 Hospital 4 6 1 \"Strings\\schedules\\Jas:winter_18.001\"/1600 AnimalShop 17 5 3 jas_read/2000 AnimalShop 1 7 3");
                    break;

                case "Jodi":
                    theSchedule.Remove(workOutDay);
                    theSchedule.Add(workOutDay, "800 SamHouse 6 5 0 jodi_dishes/940 SamHouse 5 5 0/1000 SeedShop 22 17 1/1300 SeedShop 21 17 2 jodi_exercise/1600 SeedShop 24 14 2 \"Strings\\schedules\\Jodi:Tue.000\"/1800 SamHouse 2 17 3/2100 SamHouse 10 22 3/2200 SamHouse 22 5 1");
                    theSchedule.Add(hospitalDay, "900 SamHouse 3 5 2 \"Strings\\schedules\\Jodi:spring_11.000\"/1130 Hospital 13 14 3 \"Strings\\schedules\\Jodi:spring_11.001\"/1330 Hospital 2 7 1 \"Strings\\schedules\\Jodi:spring_11.002\"/1600 SamHouse 12 22 3 \"Strings\\schedules\\Jodi:spring_11.003\"/2000 SamHouse 4 12 2 jodi_sit_down/2100 SamHouse 10 22 3/2200 SamHouse 22 5 1");
                    break;

                case "Kent":
                case "Linus":
                    // Doesn't have a hospital day
                    break;

                case "Leah":
                    theSchedule.Add(hospitalDay, "900 LeahHouse 3 7 3 leah_sculpt/1030 Hospital 12 14 0 \"Strings\\schedules\\Leah:spring_16.000\"/1330 Hospital 4 6 1 \"Strings\\schedules\\Leah:spring_16.001\"/1600 Saloon 2 20 1/2340 LeahHouse 2 4 3");
                    break;

                case "Lewis":
                    theSchedule.Add(hospitalDay, "900 AnimalShop 17 5 0/1030 Hospital 12 14 0/1330 Hospital 4 6 1 \"Strings\\schedules\\Lewis:fall_9.000\"/1600 AnimalShop 24 15 0/2100 AnimalShop 12 5 3");
                    break;

                case "Marnie":
                    theSchedule.Remove(workOutDay);
                    theSchedule.Add(workOutDay, "900 AnimalShop 17 5 0/1000 SeedShop 23 16 2/1300 SeedShop 28 17 2 marnie_exercise/1600 SeedShop 25 15 3 \"Strings\\schedules\\Marnie:Tue.000\"/1810 AnimalShop 24 15 0/2100 AnimalShop 12 5 3");
                    theSchedule.Add(hospitalDay, "900 AnimalShop 17 5 0/1030 Hospital 12 14 0/1330 Hospital 4 6 1 \"Strings\\schedules\\Marnie:fall_18.000\"/1600 AnimalShop 24 15 0/2100 AnimalShop 12 5 3");
                    break;

                case "Maru":
                    // Preserve Maru's job at the hospital
                    // Use work out day for her work day. Sssshhhhhh, nobody will ever notice.
                    theSchedule.Remove(workOutDay);
                    theSchedule.Add(workOutDay, "730 Hospital 6 15 2/1400 Hospital 9 15 3/1600 Hospital 8 17 1/1640 ScienceHouse 26 13 1/2200 ScienceHouse 9 6 1/2400 ScienceHouse 2 4 3");
                    theSchedule.Add("marriage_Mon", "800 ScienceHouse 6 20 0 \"Strings\\schedules\\Maru:marriage_Mon.000\"/1200 ScienceHouse 16 18 0 \"Strings\\schedules\\Maru:marriage_Mon.001\"/1400 Town 63 15 2 maru_sit_down \"Strings\\schedules\\Maru:marriage_Mon.002\"/1800 BusStop -1 23 3");
                    theSchedule.Add("marriageJob", "730 Hospital 6 15 2 \"Strings\\schedules\\Maru:marriageJob.000\"/1400 Hospital 9 15 3 \"Strings\\schedules\\Maru:marriageJob.001\"/1600 Hospital 8 17 1 \"Strings\\schedules\\Maru:marriageJob.002\"/1640 BusStop -1 23 3");
                    break;

                case "Pam":
                    theSchedule.Add(hospitalDay, "800 Trailer 15 4 2 pam_sit_down/1130 Hospital 4 17 2 pam_sit_down \"Strings\\schedules\\Pam:spring_25.000\"/1330 Hospital 4 6 1 \"Strings\\schedules\\Pam:spring_25.001\"/1600 Saloon 7 18 1/2400 Trailer 15 4 2 pam_sit_down");
                    theSchedule.Add("bus", "800 Trailer 15 4 2 pam_sit_down/830 BusStop 11 10 2/1700 Saloon 7 18 1/2400 Trailer 15 4 2 pam_sit_down");
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

            //if (config.debug)
                //this.Monitor.Log("Character will have " + dayEvents + " different events.");

            // Now generate different times, locations, positions and directions
            List<int> times = new List<int>();            
            List<string> locations = new List<string>();
            List<Vector2> positions = new List<Vector2>();
            List<int> directions = new List<int>();

            // Get all location names
            List<String> allLocs = new List<string>();
            foreach(GameLocation loc in Game1.locations)
                allLocs.Add(loc.Name);

            //this.Monitor.Log("Size of Game1.locations: " + allLocs.Count + " (before removing)");

            SDate date = SDate.Now();
            SDate earthquake = new SDate(3, "summer", 1);

            // Remove completely implausible locations
            for (int i=0; i<allLocs.Count; i++)
            {
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

                        if (name.Equals("Evelyn"))
                            allLocs.RemoveAt(i);
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

                    case "Saloon":
                        if (name.Equals("Jas") || name.Equals("Vincent"))
                        {
                            allLocs.RemoveAt(i);
                            i--;
                        }
                        break;                        
                }
            }

            if(name.Equals("Evelyn"))
            {                
                allLocs.Clear();
                allLocs.Add("Town");
                allLocs.Add("JoshHouse");
                allLocs.Add("SeedShop");
                allLocs.Add("Hospital");
                allLocs.Add("ArchaeologyHouse");
                allLocs.Add("Beach");
            }

            if(name.Equals("George"))
            {
                allLocs.Clear();
                allLocs.Add("JoshHouse");
                allLocs.Add("Town");
                allLocs.Add("Hospital");
                // TODO: Make him stay inside the majority of times
            }

            if(name.Equals("Linus"))
            {
                allLocs.Clear();
                allLocs.Add("Tent");
                allLocs.Add("Mountain");
                allLocs.Add("Town");
                allLocs.Add("Beach");
                allLocs.Add("Forest");
                allLocs.Add("BusStop");
                allLocs.Add("Backwoods");
                allLocs.Add("Tunnel");

                if(date >= earthquake) { 
                    allLocs.Add("Railroad");
                    allLocs.Add("BathHouse_Entry");
                    allLocs.Add("BathHouse_MensLocker");
                    allLocs.Add("BathHouse_Pool");
                }
            }

            // TODO: Make Jodi stay in her house the majority of times (akin to Vanilla)?

            //this.Monitor.Log("Size of Game1.locations: " + allLocs.Count + " (after removing)");

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
                        int t = 0;

                        /*if (name.Equals("Evelyn"))
                            t = Game1.random.Next(times.ElementAt(0) + 1, 22);
                        else*/
                            t = Game1.random.Next(times.ElementAt(0) + 1, 23);

                        // Do not add duplicate times
                        while (times.Contains(t))
                        {
                           /* if (name.Equals("Evelyn"))
                                t = Game1.random.Next(times.ElementAt(0) + 1, 22);
                            else*/
                                t = Game1.random.Next(times.ElementAt(0) + 1, 23);
                        }

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

                case "Marnie":
                    if (startTime > -1)
                        theSchedule += "830 AnimalShop 17 5 0/850 AnimalShop 12 14 2/";
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
                        if (locations.ElementAt(i).Equals("Saloon") && animChance <= 25)
                            theSchedule += " clint_dance";

                        if (i == (dayEvents - 1))
                            theSchedule += "/" + sleepTime + "00 Blacksmith 10 4 1";
                        break;
                        
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

                    case "Emily": // TODO: Figure something special out
                        if (i == (dayEvents - -1))
                            theSchedule += "/" + sleepTime + "00 HaleyHouse 21 4 1";
                        break;

                    case "Evelyn":
                        // TODO: Sleep times
                        //sleepTime = Game1.random.Next(times.ElementAt(times.Count - 1) + 1, 22);

                        if (animChance <= 10)
                            theSchedule += " evelyn_sit_left"; // TODO: Check for direction

                        if (i == (dayEvents - 1))
                            theSchedule += "/" + sleepTime + "00 JoshHouse 2 5 1";
                        break;

                    case "George": // TODO: Watch TV
                        if (i == (dayEvents - 1))
                            theSchedule += "/" + sleepTime + "00 JoushHouse 3 5 0";
                        break;

                    case "Jas":
                        if (animChance <= 5)
                            theSchedule += " jas_read";
                        else if (animChance >= 90)
                            theSchedule += " jas_jumprope";

                        if (i == (dayEvents - 1))
                            theSchedule += "/" + sleepTime + "00 AnimalShop 1 7 3";
                        break;

                    case "Jodi":
                        // TODO: jodi_dishes & jodi_sit_down

                        if (i == (dayEvents - 1))
                            theSchedule += "/" + sleepTime + "00 SamHouse 22 5 1";
                        break;

                    case "Kent":
                        // TODO: Something interesting
                        if (i == (dayEvents - 1))
                            theSchedule += "/" + sleepTime + "00 SamHouse 21 5 3";
                        break;

                    case "Leah":
                        if (animChance <= 5)
                            theSchedule += " leah_draw";

                        // TODO: leah_sculpt

                        if (i == (dayEvents - 1))
                            theSchedule += "/" + sleepTime + "00 LeahHouse 2 4 3";
                        break;

                    case "Lewis":
                        // TODO: lewis_garden
                        if (locations.ElementAt(i).Equals("Saloon") && animChance <= 50)
                            theSchedule += " lewis_drink";

                        // Lewis sleeps at Marnie's place at roughly 1/28th of the days (ie. once a month)
                        if (i == (dayEvents - 1)) {
                            if (animChance > 4)
                                theSchedule += "/" + sleepTime + "00 ManorHouse 22 4 1";
                            else
                                theSchedule += "/" + sleepTime + "00 AnimalShop 12 5 3";
                        }
                        break;

                    case "Linus":
                        // TODO: Something interesting
                        if (i == (dayEvents - 1))
                            theSchedule += "/" + sleepTime + "00 Tent 2 2 2";
                        break;

                    case "Marnie":
                        // TODO: Either make one day saloon day or make all grown ups more likely to hang out at the saloon at night
                        if (locations.ElementAt(i).Equals("Saloon") && animChance <= 50)
                            theSchedule += " marnie_drink";

                        // Marnie sleeps at Lewis' place at roughly 1/28th of the days (ie. once a month)
                        // At these chances, the case that there is a day where Lewis sleeps at Marnie's and Marnie sleeps at Lewis' are 1/784, so roughly 0.13%. Good enough.
                        if(i == (dayEvents - 1))
                        {
                            // if (animChance > 4)
                                theSchedule += "/" + sleepTime + "00 AnimalShop 12 5 3";
                            // TODO: Figure out her position in lewis' house
                        }
                        break;

                    case "Maru":
                        // TODO: maru_tinker, maru_sit_down

                        if (i == (dayEvents - 1))
                            theSchedule += "/" + sleepTime + "00 ScienceHouse 2 4 3";
                        break;

                    case "Pam":
                        if (i == (dayEvents - 1))
                            theSchedule += "/" + sleepTime + "00 Trailer 15 4 2 pam_sit_down";
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
        // TODO: pass character name as argument in order to check for character based impossible locations on maps (ie. private areas for AnimalShop etc)
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
