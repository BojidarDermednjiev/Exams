namespace Heroes.Models.Weapons
{
    using System;
    using Contracts;
    using Utilities.Messages;

    public abstract class Weapon : IWeapon
    {
        private string name;
        private int durability;
        protected Weapon(string name, int durability)
        {
            Name = name;
            Durability = durability;
        }
        public string Name
        {
            get => name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException(string.Format(ExceptionMessages.WeaponTypeNull));
                name = value;
            }
        }

        public int Durability
        {
            get => durability;
            protected set
            {
                if (value < 0)
                    throw new ArgumentException(ExceptionMessages.DurabilityBelowZero);
                durability = value;
            }
        }
        public abstract int DoDamage();
    }
}
