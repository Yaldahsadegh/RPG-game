using RPGproject.CombatSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGproject.Enemies
{
    public class Dragon : Enemy
    {
        public Dragon(EnemyRank rank)
        {
            Name = rank == EnemyRank.Boss ? "Ancient Dragon" : "Dragon";
            Health = rank == EnemyRank.Boss ? 1000 : 500;
            Mana = 200;
            Strength = rank == EnemyRank.Boss ? 100 : 50;
            Agility = rank == EnemyRank.Boss ? 30 : 15;
            Rank = rank;
        }

        // Override the Attack method from the base class
        public override void Attack(ICombatant target)
        {
            Console.WriteLine($"{Name} breathes fire on {target.Name}!");
            base.Attack(target);  // Call the base class Attack method for damage calculation
        }

        public override void Move()
        {
            Console.WriteLine($"{Name} flies majestically.");
        }
    }
}
