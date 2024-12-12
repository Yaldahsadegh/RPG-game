using RPGproject;
using RPGproject.Enemies;
using RPGproject.quest;
using System;
using System.Collections.Generic;
using System.Linq;


namespace RPGproject
{
    // Biome class to represent each biome's properties
    public class Biome
    {
        public string Name { get; }
        public int MinElevation { get; }
        public int MaxElevation { get; }

        public Biome(string name, int minElevation, int maxElevation)
        {
            Name = name;
            MinElevation = minElevation;
            MaxElevation = maxElevation;
        }
    }

    // Tile class to represent each tile on the world map
    public class Tile
    {
        public string Biome { get; }
        public int Elevation { get; }

        public Tile(string biome, int elevation)
        {
            Biome = biome;
            Elevation = elevation;
        }
    }

    // Location class to represent different locations (Villages, Towns, Dungeons)
    public class Location
    {
        public string Name { get; }
        public string Type { get; } // Village, Town, Dungeon
        public int X { get; }
        public int Y { get; }
        public List<NPC> NPCs { get; } = new List<NPC>();

        public Location(string name, string type, int x, int y)
        {
            Name = name;
            Type = type;
            X = x;
            Y = y;
        }

        public void AddNPC(NPC npc)
        {
            NPCs.Add(npc);
        }
    }

    // WorldMap class to represent the overall map structure
    public class WorldMap
    {
        public Tile[,] Map { get; }
        public int Width { get; }
        public int Height { get; }

        public WorldMap(int width, int height)
        {
            Width = width;
            Height = height;
            Map = new Tile[width, height];
        }
    }

    public class GameWorld
    {
        private static GameWorld instance;

        // Changed from List<string> to WorldMap to store tiles
        public WorldMap Map { get; private set; }
        public List<NPC> NPCs { get; private set; }
        public List<Character> PlayerCharacters { get; private set; }
        public List<Location> Locations { get; private set; }
        public List<Enemy> Enemies { get; private set; } = new List<Enemy>();

        public string TimeOfDay { get; set; }
        public string WeatherConditions { get; set; }
        public QuestManager QuestManager { get; private set; }

        private static Random random = new Random();

        // Private constructor for Singleton
        private GameWorld()
        {
            NPCs = new List<NPC>();
            PlayerCharacters = new List<Character>();
            Locations = new List<Location>();
            QuestManager = new QuestManager();
        }


        // Singleton instance access method
        public static GameWorld Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GameWorld();
                }
                return instance;
            }
        }

        // Method to set the world map
        public void SetWorldMap(int width, int height, List<Biome> biomes)
        {
            Map = GenerateWorldMap(width, height, biomes);
            GenerateLocations();
            GenerateNPCsForLocations();
        }

        // Generate new game world map
        private WorldMap GenerateWorldMap(int width, int height, List<Biome> biomes)
        {
            WorldMap worldMap = new WorldMap(width, height);

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    // Generate elevation randomly
                    int elevation = random.Next(0, 200);

                    // Determine biome based on elevation
                    Biome selectedBiome = SelectBiome(biomes, elevation);

                    // Create the tile with selected biome and attributes
                    worldMap.Map[x, y] = new Tile(selectedBiome.Name, elevation);
                }
            }

            return worldMap;
        }

        // Determine which biome corresponds to a given elevation
        private Biome SelectBiome(List<Biome> biomes, int elevation)
        {
            foreach (var biome in biomes)
            {
                if (elevation >= biome.MinElevation && elevation <= biome.MaxElevation)
                {
                    return biome;
                }
            }

            // Default biome if no match
            return biomes[0];
        }

        // Generate new locations
        private void GenerateLocations()
        {
            Locations.Clear();
            Locations.Add(new Location("Greenwood Village", "Village", random.Next(0, Map.Width), random.Next(0, Map.Height)));
            Locations.Add(new Location("Stormhold Town", "Town", random.Next(0, Map.Width), random.Next(0, Map.Height)));
            Locations.Add(new Location("Darkthorn Dungeon", "Dungeon", random.Next(0, Map.Width), random.Next(0, Map.Height)));
        }

        // Generate NPCs for each location
        private void GenerateNPCsForLocations()
        {
            foreach (var location in Locations)
            {
                switch (location.Type)
                {
                    case "Village":
                        var zelda = new NPC("Zelda", "Villager");
                        location.AddNPC(zelda);
                        AddNPC(zelda);
                        break;
                    case "Town":
                        var mia = new NPC("Mia", "Merchant");
                        location.AddNPC(mia);
                        AddNPC(mia);
                        break;
                    case "Dungeon":
                        var morga = new NPC("Morga", "Queen");
                        location.AddNPC(morga);
                        AddNPC(morga);
                        break;
                }
            }
        }

        public void AddNPC(NPC npc)
        {
            NPCs.Add(npc);
            QuestManager.Subscribe(npc);
        }


        public void AssignQuestsToNPCs()
        {
            var zeldaNPC = NPCs.FirstOrDefault(n => n.Name == "Zelda");
            var miaNPC = NPCs.FirstOrDefault(n => n.Name == "Mia");
            var morgaNPC = NPCs.FirstOrDefault(n => n.Name == "Morga");

            if (zeldaNPC == null || miaNPC == null || morgaNPC == null)
            {
                Console.WriteLine("One or more NPCs not found.");
                return;
            }

            var activeQuests = QuestManager.Instance.GetQuests();
            bool zeldaHasQuest = activeQuests.Any(q => q.AssignedNPC == zeldaNPC && q.Status == QuestStatus.InProgress);
            bool miaHasQuest = activeQuests.Any(q => q.AssignedNPC == miaNPC && q.Status == QuestStatus.InProgress);
            bool morgaHasQuest = activeQuests.Any(q => q.AssignedNPC == morgaNPC && q.Status == QuestStatus.InProgress);

            if (!zeldaHasQuest)
            {
                var zeldaQuest = new Quest("Defeat the Slime", "A slime is causing trouble near Greenwood Village. Defeat it!", zeldaNPC, "Slime");
                QuestManager.Instance.StartQuestForDefeatingEnemy(zeldaQuest.AssignedNPC, "Slime");
                Console.WriteLine("Zelda's quest has been assigned in Greenwood Village.");
            }

            if (!miaHasQuest)
            {
                var miaQuest = new Quest("Defeat the Goblin", "A goblin is terrorizing Stormhold Town. Defeat it!", miaNPC, "Goblin");
                QuestManager.Instance.StartQuestForDefeatingEnemy(miaQuest.AssignedNPC, "Goblin");
                Console.WriteLine("Mia's quest has been assigned in Stormhold Town.");
            }

            if (!morgaHasQuest)
            {
                var morgaQuest = new Quest("Defeat the Dragon", "A dragon is lurking in Darkthorn Dungeon. Defeat it!", morgaNPC, "Dragon");
                QuestManager.Instance.StartQuestForDefeatingEnemy(morgaQuest.AssignedNPC, "Dragon");
                Console.WriteLine("Morga's quest has been assigned in Darkthorn Dungeon.");
            }
        }

        // Method to add a player-created character to the world
        public void AddCharacter(Character character)
        {
            PlayerCharacters.Add(character);
        }

        //Method to add an enemy to the world

        public void AddEnemy(Enemy enemy)
        {
            Enemies.Add(enemy);
        }


        // Method to print the currently generated map
        public void PrintMap()
        {
            for (int y = 0; y < Map.Height; y++)
            {
                for (int x = 0; x < Map.Width; x++)
                {
                    var tile = Map.Map[x, y];
                    SetBiomeColor(tile.Biome);
                    Console.Write(tile.Biome[0] + " ");  // Print the first letter of the biome
                    ResetColor();
                }
                Console.WriteLine();  // New line after each row
            }
        }

        // Terminal colors for biomes 
        public void SetBiomeColor(string biome)
        {
            switch (biome)
            {
                case "Lake":
                    Console.ForegroundColor = ConsoleColor.Blue;  // Blue for Lake
                    break;
                case "Forest":
                    Console.ForegroundColor = ConsoleColor.DarkGreen;  // DarkGreen for Forest
                    break;
                case "Desert":
                    Console.ForegroundColor = ConsoleColor.Yellow;  // Yellow for Desert
                    break;
                case "Mountain":
                    Console.ForegroundColor = ConsoleColor.Gray;  // Gray for Mountain
                    break;
                case "Plains":
                    Console.ForegroundColor = ConsoleColor.Green;  // Green for Plains
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.White;  // Default to white
                    break;
            }
        }

        // Reset console color to default
        private void ResetColor()
        {
            Console.ResetColor();
        }
    }
}
