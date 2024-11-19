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
        public Goblin(EnemyRank rank) : base(rank) { }

        protected override void InitializeStatsByRank(EnemyRank rank)
        {
            // Stats and name are configured based on rank
            Name = "Goblin";
            MaxHealth = 80;
            Health = MaxHealth;
            Mana = 20;
            Strength = 10;
            Agility = 10;
            Defense = 4;

            // Adjustments for ranks handled here
            if (rank == EnemyRank.Elite)
            {
                Name = "Goblin Elite";
                MaxHealth = 120;
                Strength = 20;
                Defense = 6;
            }
            else if (rank == EnemyRank.Boss)
            {
                Name = "Goblin King";
                MaxHealth = 150;
                Strength = 30;
                Defense = 8;
            }
            Health = MaxHealth; // Ensure starting health matches max health
        }

        public override void Attack(ICombatant target)
        {
            Console.WriteLine($"{Name} swings a club at {target.Name}!");
            base.Attack(target);
        }

        public override void Move()
        {
            Console.WriteLine($"{Name} scurries around.");
        }
    }
}
