using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGproject.PlayerCharacter.Commands
{
    namespace RPGproject.PlayerCharacter.Commands
    {
        public class Controller
        {
            private readonly Dictionary<ConsoleKey, ICommand> _keyMappings = new Dictionary<ConsoleKey, ICommand>();

            public void MapKey(ConsoleKey key, ICommand command)
            {
                _keyMappings[key] = command;
            }

            public bool Listen(Character character)
            {
                Console.WriteLine("Press a key for an action: A (Attack), D (Defend), H (Heal), M (Move), O (Open Menu), Q (Quit)");

                while (true)
                {
                    if (Console.KeyAvailable)
                    {
                        var key = Console.ReadKey(intercept: true).Key;

                        if (key == ConsoleKey.Q) // Quit
                        {
                            return true; // Signal to quit
                        }
                        else if (key == ConsoleKey.O) // Open menu
                        {
                            return false; // Return false to indicate menu should be opened
                        }
                        else if (_keyMappings.TryGetValue(key, out ICommand command))
                        {
                            command.Execute(character);
                        }
                        else
                        {
                            Console.WriteLine("Key not mapped to any action.");
                        }
                    }
                }
            }
        }
    }
}

