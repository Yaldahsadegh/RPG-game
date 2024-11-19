using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGproject.PlayerCharacter.Commands
{
    public class MoveCommand : ICommand
    {
        private readonly string _destination;

        public MoveCommand(string destination)
        {
            _destination = destination;
        }

        public void Execute(Character character)
        {
            if (character == null)
            {
                throw new ArgumentNullException(nameof(character));
            }

            Console.WriteLine($"{character.Name} moves to {_destination}.");
            // Update character's position or handle movement logic here
        }
    }
}
