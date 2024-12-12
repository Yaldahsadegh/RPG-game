using RPGproject.ActionStrategy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGproject.PlayerCharacter.Commands
{
    public class DefendCommand : ICommand
    {
        public void Execute(Character character) // Ensure this matches the interface
        {
            Console.WriteLine($"{character.Name} is defending.");
            character.SetActionStrategy(new DefendAction()); // Assuming DefendAction is defined
            //character.PerformAction(); // Execute the defend action
        }
    }
}


