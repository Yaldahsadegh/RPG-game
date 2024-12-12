using RPGproject.Enemies;
using RPGproject.PlayerCharacter.Commands;
using RPGproject.PlayerCharacter.Commands.RPGproject.PlayerCharacter.Commands;
using RPGproject.quest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace RPGproject
{
    class Program
    {
        static void Main(string[] args)
        {
            GameWorld gameWorld = GameWorld.Instance;

            // Define biomes
            List<Biome> biomes = new List<Biome>
            {
                new Biome("Lake", 0, 50),
                new Biome("Forest", 51, 100),
                new Biome("Desert", 101, 150),
                new Biome("Mountain", 151, 200),
                new Biome("Plains", 201, 300)
            };

            // Set up the game world with a world map
            gameWorld.SetWorldMap(10, 10, biomes); // Example dimensions 10x10

            //creating npcs
            NPC zelda = new NPC("Zelda", "Guard");
            NPC mia = new NPC("Mia", "Trader");

            // Add NPCs to the game world
            gameWorld.AddNPC(zelda);
            gameWorld.AddNPC(mia);
            gameWorld.TimeOfDay = "Night";
            gameWorld.WeatherConditions = "Foggy";

            // Subscribe NPCs to the QuestManager (for updates)
            QuestManager.Instance.Subscribe(zelda);
            QuestManager.Instance.Subscribe(mia);

            //create enemies 
            var bossDragon = EnemyFactory.CreateEnemy("Dragon", EnemyRank.Boss);
            var eliteGoblin = EnemyFactory.CreateEnemy("Goblin", EnemyRank.Elite);
            var normalSlime = EnemyFactory.CreateEnemy("Slime", EnemyRank.Normal);

            // add  enemies to the game world
            GameWorld.Instance.AddEnemy(bossDragon);
            GameWorld.Instance.AddEnemy(eliteGoblin);
            GameWorld.Instance.AddEnemy(normalSlime);



            // Call the character creation method
            CreateCharacters(gameWorld);

            // Display game world info after character creation
            DisplayGameWorldInfo(gameWorld);

            // Perform actions with created characters
            PerformCharacterActions(gameWorld);

        }

        static void DisplayEnemyInfo(Enemy enemy)
        {
            Console.WriteLine($"- {enemy.GetType().Name} ({enemy.Rank}): Health {enemy.Health}/{enemy.MaxHealth}, Strength {enemy.Strength}, Defense {enemy.Defense}");
        }

        static void PerformCharacterActions(GameWorld gameWorld)
        {
            if (gameWorld.PlayerCharacters.Count == 0)
            {
                Console.WriteLine("No characters to perform actions with.");
                return;
            }

            bool exitActions = false;
            int currentCharacterIndex = 0;
            Controller controller = new Controller();

            // Set up command key mappings
            controller.MapKey(ConsoleKey.A, new AttackCommand(null)); // Replace 'null' with actual target when selected
            controller.MapKey(ConsoleKey.D, new DefendCommand());
            controller.MapKey(ConsoleKey.H, new HealCommand());
            controller.MapKey(ConsoleKey.M, new MoveCommand("North")); // Move example

            while (!exitActions)
            {
                Character currentCharacter = gameWorld.PlayerCharacters[currentCharacterIndex];

                // After character actions, trigger NPC interaction
                InteractWithNPCs(currentCharacter, gameWorld);

                // Show action menu prompt only once
                Console.WriteLine($"\n{currentCharacter.Name}'s turn. Press A (Attack), D (Defend), H (Heal), M (Move), O (Open Menu), Q (Quit)");

                bool quit = controller.Listen(currentCharacter);

                if (quit)
                {
                    exitActions = true; // Exit the action loop
                }
                else
                {
                    ShowActionMenu(currentCharacter, gameWorld); // Show the action menu when O is pressed
                }

                // Move to next character or ask if the player wants to end the round
                if (AskUserForNextMove(ref currentCharacterIndex, gameWorld))
                {
                    Console.WriteLine("\nDo you want to continue actions for the next round? (yes/no)");
                    string continueChoice = Console.ReadLine().ToLower();
                    if (continueChoice == "no")
                    {
                        exitActions = true;
                    }
                }
            }
        }
        static void ShowActionMenu(Character character, GameWorld gameWorld)
        {
            bool exitMenu = false;

            while (!exitMenu)
            {
                Console.WriteLine($"\n{character.Name}'s Action Menu:");
                Console.WriteLine("1. View Quest Log");
                Console.WriteLine("2. View Inventory");
                Console.WriteLine("3. Exit Menu");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        DisplayQuestLog(character);
                        break;
                    case "2":
                        DisplayInventory(character);
                        break;
                    case "3":
                        exitMenu = true;
                        break;
                    default:
                        Console.WriteLine("Invalid selection. Please choose a valid action.");
                        break;
                }
            }
        }

        static void DisplayQuestLog(Character character)
        {
            var characterQuests = QuestManager.Instance.GetCharacterQuests(character);

            if (characterQuests.Count == 0)
            {
                Console.WriteLine($"{character.Name} has no active quests.");
            }
            else
            {
                Console.WriteLine($"{character.Name}'s Quest Log:");
                foreach (var quest in characterQuests)
                {
                    Console.WriteLine($"- {quest.Title}: {quest.Status}");
                    Console.WriteLine($"  Description: {quest.Description}");
                    Console.WriteLine($"  Assigned by: {quest.AssignedNPC.Name}");
                    Console.WriteLine($"  Enemy: {quest.EnemyName}");
                }
            }
        }



        static void InteractWithNPCs(Character character, GameWorld gameWorld)
        {
            foreach (var npc in gameWorld.NPCs)
            {
                if (npc.Name == "Zelda" && gameWorld.TimeOfDay == "Night")
                {
                    HandleQuestForNPC(npc, gameWorld, character);  // Zelda interacts only at night
                }
                else if (npc.Name == "Mia" && gameWorld.TimeOfDay == "Day")
                {
                    HandleQuestForNPC(npc, gameWorld, character);  // Mia interacts only during the day
                }
                else if (npc.Name != "Zelda" && npc.Name != "Mia")
                {
                    HandleQuestForNPC(npc, gameWorld, character);  // For any other NPCs that should interact at all times
                }
            }
        }

        private static void HandleQuestForNPC(NPC npc, GameWorld gameWorld, Character character)
        {
            // Dynamically create a quest for this NPC if no quests are assigned
            if (!QuestManager.Instance.GetQuests().Any(q => q.AssignedNPC == npc))
            {
                // Example quest creation based on time of day and weather
                if (gameWorld.TimeOfDay == "Night" && gameWorld.WeatherConditions == "Foggy")
                {
                    Console.WriteLine($"{npc.Name} has a new quest for you!");
                    QuestManager.Instance.StartQuestForDefeatingEnemy(npc, "Dragon");
                }
                else if (gameWorld.TimeOfDay == "Day" && gameWorld.WeatherConditions == "Sunny")
                {
                    Console.WriteLine($"{npc.Name} has a new quest for you!");
                    QuestManager.Instance.StartQuestForDefeatingEnemy(npc, "Slime");
                }
            }

            // Retrieve quests assigned to this NPC that are not yet accepted
            var quests = QuestManager.Instance.GetQuests().Where(q => q.AssignedNPC == npc && q.Status == QuestStatus.NotAccepted).ToList();

            if (quests.Count == 0)
            {
                Console.WriteLine($"{npc.Name} has no new quests for you.");
            }
            else
            {
                foreach (var quest in quests)
                {
                    Console.WriteLine($"{npc.Name}: {quest.Title} - {quest.Description}");

                    bool validInput = false;
                    while (!validInput)
                    {
                        Console.WriteLine("Do you want to accept this quest? (Y/N)");
                        string input = Console.ReadLine()?.ToLower();

                        switch (input)
                        {
                            case "y":
                                QuestManager.Instance.AcceptQuest(quest.Title, character);  // Pass the character object here
                                Console.WriteLine($"{npc.Name}: Quest accepted. Good luck!");
                                validInput = true;
                                break;
                            case "n":
                                QuestManager.Instance.DenyQuest(quest.Title); // Pass the quest title
                                Console.WriteLine($"{npc.Name}: Maybe next time.");
                                validInput = true;
                                break;
                            default:
                                Console.WriteLine("Invalid input. Please type 'Y' to accept or 'N' to decline.");
                                break;
                        }
                    }
                }
            }
        }


        static void DisplayInventory(Character character)
        {
            // List all items in the character's inventory
            character.Inventory.ListItems();

            Console.WriteLine("\nEnter the name of the item to equip it, or type 'exit' to return to the action menu:");
            string itemName = Console.ReadLine();

            if (itemName.ToLower() == "exit")
            {
                return; // Exit the inventory menu and return to the action menu
            }

            var item = character.Inventory.GetItemByName(itemName); // Retrieve the item by name

            if (item != null)
            {
                // Use the selected item, which will both equip and remove it from the inventory
                character.UseItem(item.Name); // This calls the UseItem method
            }
            else
            {
                Console.WriteLine("Item not found in inventory.");
            }
        }

        static bool AskUserForNextMove(ref int currentCharacterIndex, GameWorld gameWorld)
        {
            Console.WriteLine("\nWhat would you like to do next?");
            Console.WriteLine("1. Continue with the same character.");
            Console.WriteLine("2. Switch to another character.");
            Console.WriteLine("3. End this character's turn.");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    return false;
                case "2":
                    currentCharacterIndex = (currentCharacterIndex + 1) % gameWorld.PlayerCharacters.Count;
                    return true;
                case "3":
                    return true;
                default:
                    Console.WriteLine("Invalid choice. Continuing with the same character.");
                    return false;
            }
        }

        static void CreateCharacters(GameWorld gameWorld)
        {
            Console.WriteLine("Welcome to Character Creation!");

            while (true)
            {
                Console.Write("Enter the character type (Paladin, Sorcerer, Ranger) or type 'exit' to stop: ");
                string characterType = Console.ReadLine();

                if (characterType.ToLower() == "exit")
                {
                    break;
                }

                Console.Write("Enter the character name: ");
                string characterName = Console.ReadLine();

                try
                {
                    // Create the character
                    Character newCharacter = CharacterFactory.CreateCharacter(characterType, characterName);

                    // Select the appropriate item factory based on character type
                    IItemFactory itemFactory;
                    switch (characterType.ToLower())
                    {
                        case "paladin":
                            itemFactory = new LegendaryItemFactory(); // Paladins get Legendary items
                            break;
                        case "sorcerer":
                            itemFactory = new RareItemFactory(); // Sorcerers get Rare items
                            break;
                        case "ranger":
                            itemFactory = new CommonItemFactory(); // Rangers get Common items
                            break;
                        default:
                            throw new ArgumentException("Invalid character type.");
                    }

                    // Create 3 weapons, 1 potion, and 1 armor for this character
                    var weapons = new List<Weapon>
                    {
                        itemFactory.CreateWeapon("Weapon 1"),
                        itemFactory.CreateWeapon("Weapon 2"),
                        itemFactory.CreateWeapon("Weapon 3")
                    };
                    var potion = itemFactory.CreatePotion("Healing Potion");
                    var armor = itemFactory.CreateArmor("Steel Armor");

                    // Add the generated items to the character's inventory
                    foreach (var weapon in weapons)
                    {
                        newCharacter.Inventory.AddItemToInventory(weapon);
                    }
                    newCharacter.Inventory.AddItemToInventory(potion);
                    newCharacter.Inventory.AddItemToInventory(armor);

                    // Display character info
                    newCharacter.DisplayInfo();

                    // Add the character to the game world
                    gameWorld.AddCharacter(newCharacter);
                    break;
                }



                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }


        static void DisplayGameWorldInfo(GameWorld gameWorld)
        {
            Console.WriteLine("Game World Locations:");
            Console.WriteLine(new string('-', gameWorld.Map.Width * 2));

            for (int y = 0; y < gameWorld.Map.Height; y++)
            {
                for (int x = 0; x < gameWorld.Map.Width; x++)
                {
                    var tile = gameWorld.Map.Map[x, y];
                    gameWorld.SetBiomeColor(tile.Biome);
                    Console.Write(tile.Biome[0] + " ");
                }
                Console.WriteLine();
                Console.ResetColor();
            }

            Console.WriteLine(new string('-', gameWorld.Map.Width * 2));

            Console.WriteLine("\nNPCs in the Game World:");
            foreach (var npc in gameWorld.NPCs)
            {
                Console.WriteLine($"- {npc.Name} ({npc.Role})");
            }

            Console.WriteLine("\nPlayer-Created Characters:");
            foreach (var character in gameWorld.PlayerCharacters)
            {
                Console.WriteLine($"- {character.GetType().Name}: {character.Name}");
            }

            Console.WriteLine("\nEnemies in the Game World:");
            if (gameWorld.Enemies.Count == 0)
            {
                Console.WriteLine("No enemies in the game world.");
            }
            else
            {
                foreach (var enemy in gameWorld.Enemies)
                {
                    DisplayEnemyInfo(enemy); // Display detailed information for each enemy
                }
            }

            Console.WriteLine($"\nCurrent Time of Day: {gameWorld.TimeOfDay}");
            Console.WriteLine($"Current Weather Conditions: {gameWorld.WeatherConditions}");
        }
    }
}

