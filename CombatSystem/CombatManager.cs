using RPGproject.CombatSystem;
using RPGproject.Enemies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGproject
{
    public class CombatManager
    {
        private List<ICombatant> combatants; // List of combatants (players and enemies)
        private int currentTurnIndex; // Index to track the current turn

        public CombatManager(List<ICombatant> combatants)
        {
            this.combatants = combatants;
            currentTurnIndex = 0;
        }

        public void StartCombat()
        {
            bool combatOngoing = true;

            while (combatOngoing)
            {
                ICombatant currentCombatant = combatants[currentTurnIndex];

                if (currentCombatant.Health <= 0)
                {
                    Console.WriteLine($"{currentCombatant.Name} is down and cannot act this turn.");
                    NextTurn(); // Skip downed combatants
                    continue;
                }

                // Determine if it's a player or enemy's turn and handle actions
                Console.WriteLine($"{currentCombatant.Name}'s turn:");

                // Player's turn or enemy's AI logic
                if (currentCombatant is Character player)
                {
                    HandlePlayerTurn(player);
                }
                else if (currentCombatant is Enemy enemy)
                {
                    HandleEnemyTurn(enemy);
                }

                // Check if combat is over
                combatOngoing = !IsCombatOver();
                NextTurn();
            }

            // Combat over, declare winner
            if (AllEnemiesDefeated())
            {
                Console.WriteLine("Players have won the battle!");
            }
            else
            {
                Console.WriteLine("Enemies have won the battle!");
            }
        }

        private void NextTurn()
        {
            // Move to the next turn in the combat cycle
            currentTurnIndex = (currentTurnIndex + 1) % combatants.Count;
        }

        private bool IsCombatOver()
        {
            return AllEnemiesDefeated() || AllPlayersDefeated();
        }

        private bool AllPlayersDefeated()
        {
            foreach (var combatant in combatants)
            {
                if (combatant is Character player && player.Health > 0)
                {
                    return false;
                }
            }
            return true;
        }

        private bool AllEnemiesDefeated()
        {
            foreach (var combatant in combatants)
            {
                if (combatant is Enemy enemy && enemy.Health > 0)
                {
                    return false;
                }
            }
            return true;
        }

        private void HandlePlayerTurn(Character player)
        {
            // Player's decision logic (attack, heal, defend, etc.)
            Console.WriteLine("Player chooses an action...");
            // Call action method based on user input
        }

        private void HandleEnemyTurn(Enemy enemy)
        {
            // Enemy AI logic (randomly choose between attack, heal, defend)
            Random random = new Random();
            int action = random.Next(0, 3); // 0: Attack, 1: Heal, 2: Defend
            switch (action)
            {
                case 0:
                    // Enemy attack logic
                    Console.WriteLine($"{enemy.Name} attacks a player!");
                    break;
                case 1:
                    // Enemy heal logic
                    Console.WriteLine($"{enemy.Name} heals itself!");
                    break;
                case 2:
                    // Enemy defend logic
                    Console.WriteLine($"{enemy.Name} defends!");
                    break;
            }
        }

        // Method to get a list of enemies
        public List<Enemy> GetEnemies()
        {
            List<Enemy> enemies = new List<Enemy>();
            foreach (var combatant in combatants)
            {
                if (combatant is Enemy enemy)
                {
                    enemies.Add(enemy);
                }
            }
            return enemies;
        }
    }
}
