using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPGproject.CombatSystem;


namespace RPGproject.Enemies
{
    public abstract class Enemy : ICombatant
    {
        public string Name { get; set; }
        public int Health { get; set; }
        public int MaxHealth { get; set; }
        public int Mana { get; set; }
        public int Strength { get; set; }
        public int Agility { get; set; }
        public int Defense { get; set; }
        public EnemyRank Rank { get; set; }
        public IActionStrategy ActionStrategy { get; set; }
        public ICharacterState CurrentState { get; set; }
        public Weapon EquippedWeapon { get; set; }
        public Armor EquippedArmor { get; set; }

        protected Enemy(EnemyRank rank)
        {
            Rank = rank;
            InitializeStatsByRank(rank); // Initialize stats based on rank
            ActionStrategy = new DefaultAction();
            CurrentState = new IdleState();
        }

        protected abstract void InitializeStatsByRank(EnemyRank rank);

        public void SetActionStrategy(IActionStrategy actionStrategy)
        {
            ActionStrategy = actionStrategy;
        }

        public void SetState(ICharacterState state)
        {
            CurrentState = state;
        }

        public void PerformAction(ICombatant target)
        {
            if (ActionStrategy != null && target != null)
            {
                ActionStrategy.PerformAction(this, target);
            }
            else
            {
                Console.WriteLine($"{Name} cannot perform the action due to missing strategy or target.");
            }
        }

        public virtual void Attack(ICombatant target)
        {
            if (target == null) return;

            int damage = Strength;

            if (EquippedWeapon != null)
            {
                damage += EquippedWeapon.Damage;
            }

            if (target is Character characterTarget)
            {
                damage -= characterTarget.Defense;
            }
            else if (target is Enemy enemyTarget)
            {
                damage -= enemyTarget.Defense;
            }

            damage = Math.Max(damage, 0);

            Console.WriteLine($"{Name} attacks {target.Name} for {damage} damage!");
            target.TakeDamage(damage);
        }

        public void TakeDamage(int damage)
        {
            Health -= damage;
            if (Health <= 0)
            {
                Health = 0;
                Console.WriteLine($"{Name} has been defeated!");
            }
            else
            {
                Console.WriteLine($"{Name} takes {damage} damage. Remaining Health: {Health}/{MaxHealth}");
            }
        }

        public void Heal()
        {
            const int healAmount = 10;

            if (Health <= 0)
            {
                Console.WriteLine($"{Name} cannot heal because they are incapacitated.");
                return;
            }

            int newHealth = Health + healAmount;
            if (newHealth > MaxHealth)
            {
                newHealth = MaxHealth;
            }

            Health = newHealth;
            Console.WriteLine($"{Name} heals for {healAmount} health. Current health: {Health}/{MaxHealth}");
        }

        public void EquipWeapon(Weapon weapon)
        {
            if (EquippedWeapon != null)
            {
                Console.WriteLine($"{Name} unequips {EquippedWeapon.Name}.");
            }

            EquippedWeapon = weapon;
            Console.WriteLine($"{Name} equips weapon: {weapon.Name}.");
        }

        public void EquipArmor(Armor armor)
        {
            if (EquippedArmor != null)
            {
                Defense -= EquippedArmor.Defense;
                Console.WriteLine($"{Name} unequips {EquippedArmor.Name}. Defense decreases by {EquippedArmor.Defense}.");
            }

            EquippedArmor = armor;
            Defense += armor.Defense;
            Console.WriteLine($"{Name} equips armor: {armor.Name}. Defense increases by {armor.Defense}.");
        }

        public abstract void Move();
    }
}
