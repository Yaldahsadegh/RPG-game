using RPGproject.CombatSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



    namespace RPGproject
    {
        public interface ICharacterState
        {
            void Enter(Character character);
            void HandleState(Character character); // Must be implemented
        }

        // Idle state class
        public class IdleState : ICharacterState
        {
            public void Enter(Character character)
            {
                Console.WriteLine($"{character.Name} is entering idle state.");
            }

            public void HandleState(Character character)
            {
                Console.WriteLine($"{character.Name} is in idle state.");
            }
        }

    // Action state class
    public class ActionState : ICharacterState
    {
        private readonly IActionStrategy _actionStrategy;
        private readonly CombatManager _combatManager; // Reference to the CombatManager

        public ActionState(IActionStrategy actionStrategy, CombatManager combatManager)
        {
            _actionStrategy = actionStrategy;
            _combatManager = combatManager;
        }

        public void Enter(Character character)
        {
            Console.WriteLine($"{character.Name} is entering action state.");
        }

        public void HandleState(Character character)
        {
            Console.WriteLine($"{character.Name} is in action mode.");

            // Use the combat system to get the target for the action
            ICombatant target = GetTargetForAction(character);

            if (target != null)
            {
                // Now pass both the character and the target to PerformAction
                character.PerformAction(target); // Pass the target to the PerformAction method
            }
            else
            {
                Console.WriteLine($"{character.Name} has no valid target.");
            }
        }

        private ICombatant GetTargetForAction(Character character)
        {
            // Use the new method to get the list of enemies
            var enemies = _combatManager.GetEnemies();

            if (enemies.Count > 0)
            {
                // Example: Choose the first enemy from the list
                return enemies[0]; // Modify this logic to select a target based on your needs
            }

            return null; // No valid target if there are no enemies
        }
    }

    // Defending state class
    public class DefendingState : ICharacterState
        {
            public void Enter(Character character)
            {
                Console.WriteLine($"{character.Name} is entering defending state.");
            }

            public void HandleState(Character character)
            {
                Console.WriteLine($"{character.Name} is defending.");
            }
        }
    }

