using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGproject.PlayerCharacter.Commands
{
    public class AttackCommand : ICommand
    {
        private readonly Character _target; // The target character to attack

        public AttackCommand(Character target)
        {
            _target = target;
        }

        public void Execute(Character attacker)
        {
            if (attacker == null || _target == null)
            {
                throw new ArgumentNullException("Attacker or target character cannot be null.");
            }

            // Example damage calculation based on attacker's strength
            int damage = attacker.Strength; // Assume damage equals strength for simplicity
            _target.Health -= damage;

            // Ensure target's health doesn't drop below zero
            if (_target.Health < 0)
            {
                _target.Health = 0;
            }

            Console.WriteLine($"{attacker.Name} attacks {_target.Name} for {damage} damage. {_target.Name}'s health is now {_target.Health}/{_target.MaxHealth}.");
        }
    }
}
