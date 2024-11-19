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
        public Slime(EnemyRank rank) : base(rank) { }

        protected override void InitializeStatsByRank(EnemyRank rank)
        {
            Name = "Slime";
            MaxHealth = 50;
            Health = MaxHealth;
            Mana = 10;
            Strength = 5;
            Agility = 5;
            Defense = 1;

            if (rank == EnemyRank.Elite)
            {
                Name = "Elite Slime";
                MaxHealth = 100;
                Strength = 10;
                Defense = 3;
            }
            else if (rank == EnemyRank.Boss)
            {
                Name = "King Slime";
                MaxHealth = 200;
                Strength = 20;
                Defense = 5;
            }
            Health = MaxHealth;
        }

        public override void Attack(ICombatant target)
        {
            Console.WriteLine($"{Name} slaps with a slimy tendril at {target.Name}!");
            base.Attack(target);
        }

        public override void Move()
        {
            Console.WriteLine($"{Name} slithers slowly.");
        }
    }
}
