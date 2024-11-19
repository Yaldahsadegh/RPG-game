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
        public Dragon(EnemyRank rank) : base(rank) { }

        protected override void InitializeStatsByRank(EnemyRank rank)
        {
            Name = "Dragon";
            MaxHealth = 500;
            Health = MaxHealth;
            Mana = 200;
            Strength = 50;
            Agility = 15;
            Defense = 10;

            if (rank == EnemyRank.Boss)
            {
                Name = "Ancient Dragon";
                MaxHealth = 1000;
                Strength = 100;
                Agility = 30;
                Defense = 20;
            }
            Health = MaxHealth;
        }

        public override void Attack(ICombatant target)
        {
            Console.WriteLine($"{Name} breathes fire on {target.Name}!");
            base.Attack(target);
        }

        public override void Move()
        {
            Console.WriteLine($"{Name} flies majestically.");
        }
    }
}
