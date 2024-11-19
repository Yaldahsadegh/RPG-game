using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGproject.PlayerCharacter.Commands
{
    public class HealCommand : ICommand
    {
        private const int HealAmount = 10; // Define a constant heal amount

        public void Execute(Character character)
        {
            if (character == null)
            {
                throw new ArgumentNullException(nameof(character));
            }

            // Calculate new health
            int newHealth = character.Health + HealAmount;

            // Ensure health does not exceed maximum health
            if (newHealth > character.MaxHealth)
            {
                newHealth = character.MaxHealth;
            }

            character.Health = newHealth; // Update character's health
            Console.WriteLine($"{character.Name} heals for {HealAmount} health. Current health: {character.Health}/{character.MaxHealth}");
        }
    }
}
