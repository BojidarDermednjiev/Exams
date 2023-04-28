namespace Heroes.Models.Heroes
{
    using System;
    using Contracts;
    using Utilities.Messages;

    public abstract class Hero : IHero
    {
        private string name;
        private int health;
        private int armour;
        private IWeapon weapon;
        protected Hero(string name, int health, int armour)
        {
            Name= name;
            Health= health;
            Armour= armour;
        }
        public string Name
        {
            get => name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException(string.Format(ExceptionMessages.HeroNameNull));
                name = value;
            }
        }
        public int Health
        {
            get => health;
            private set
            {
                if (value < 0)
                    throw new ArgumentException(string.Format(ExceptionMessages.HeroHealthBelowZero));
                health = value;
            }
        }
        public int Armour
        {
            get => armour;
            private set
            {
                if (value < 0)
                    throw new ArgumentException(string.Format(ExceptionMessages.HeroArmourBelowZero));
                armour = value;
            }
        }
        public IWeapon Weapon
        {
            get => weapon;
            private set
            {
                if (value == null)
                    throw new ArgumentException(string.Format(ExceptionMessages.WeaponNull));
                weapon = value;
            }
        }
        public bool IsAlive => this.health > 0;
        public void AddWeapon(IWeapon weapon)
        {
            this.weapon = weapon;
        }
        public void TakeDamage(int points)
        {
            int armoutDamage = Math.Min(Armour, points);
            Armour -= armoutDamage;
            int healthDamage = Math.Min(Health, points - armoutDamage);
            Health -= healthDamage;
        }
    }
}
