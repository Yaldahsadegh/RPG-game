using RPGproject.CombatSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGproject.Enemies
{
    public class Goblin : Enemy
    {
        public Goblin(EnemyRank rank)
        {
            Name = rank == EnemyRank.Boss ? "Goblin King" : "Goblin";
            Health = rank == EnemyRank.Boss ? 150 : 80;
            Mana = 20;
            Strength = rank == EnemyRank.Boss ? 30 : 10;
            Agility = 10;
            Rank = rank;
        }

        // Override the Attack method from the base class
        public override void Attack(ICombatant target)
        {
            Console.WriteLine($"{Name} swings a club at {target.Name}!");
            base.Attack(target);  // Call the base class Attack method for damage calculation
        }

        public override void Move()
        {
            Console.WriteLine($"{Name} scurries around.");
        }
    }
}
