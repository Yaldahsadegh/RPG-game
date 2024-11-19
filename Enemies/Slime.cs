using RPGproject.CombatSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGproject.Enemies
{
    public class Slime : Enemy
    {
        public Slime(EnemyRank rank)
        {
            Name = rank == EnemyRank.Boss ? "King Slime" : "Slime";
            Health = rank == EnemyRank.Boss ? 200 : 50;
            Mana = 10;
            Strength = rank == EnemyRank.Boss ? 20 : 5;
            Agility = 5;
            Rank = rank;
        }

        // Override the Attack method from the base class
        public override void Attack(ICombatant target)
        {
            Console.WriteLine($"{Name} slaps with a slimy tendril at {target.Name}!");
            base.Attack(target);  // Call the base class Attack method for damage calculation
        }

        public override void Move()
        {
            Console.WriteLine($"{Name} slithers slowly.");
        }
    }
}
