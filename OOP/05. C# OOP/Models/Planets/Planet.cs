namespace PlanetWars.Models.Planets
{
    using PlanetWars.Models.MilitaryUnits;
    using PlanetWars.Models.MilitaryUnits.Contracts;
    using PlanetWars.Models.Planets.Contracts;
    using PlanetWars.Models.Weapons;
    using PlanetWars.Models.Weapons.Contracts;
    using PlanetWars.Repositories;
    using PlanetWars.Utilities.Messages;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Planet : IPlanet
    {
        private string name;
        private double budget;
        private UnitRepository units;
        private WeaponRepository weapons;
        public Planet(string name, double budget)
        {
            Name = name;
            Budget = budget;
            units = new UnitRepository();
            weapons = new WeaponRepository();
        }
        public string Name
        {
            get => name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException(string.Format(ExceptionMessages.InvalidPlanetName));
                this.name = value;
            }
        }

        public double Budget
        {
            get => budget;
            private set
            {
                if (value < 0)
                    throw new ArgumentException(string.Format(ExceptionMessages.InvalidBudgetAmount));
                budget = value;
            }
        }

        public double MilitaryPower => Math.Round(this.CalculateMilitaryPower(), 3);

        private double CalculateMilitaryPower()
        {
            double totalAmount = units.Models.Sum(x => x.EnduranceLevel) + weapons.Models.Sum(x => x.DestructionLevel);
            if (units.Models.Any(x => x.GetType().Name == nameof(AnonymousImpactUnit)))
                totalAmount *= 1.30;
            if (weapons.Models.Any(x => x.GetType().Name == nameof(NuclearWeapon)))
                totalAmount *= 1.45;
            return totalAmount;
        }

        public IReadOnlyCollection<IMilitaryUnit> Army => units.Models;

        public IReadOnlyCollection<IWeapon> Weapons => weapons.Models;

        public void AddUnit(IMilitaryUnit unit)
        {
            units.AddItem(unit);
        }

        public void AddWeapon(IWeapon weapon)
        {
            weapons.AddItem(weapon);
        }
        public void TrainArmy()
        {
            foreach (var unit in Army)
                unit.IncreaseEndurance();
        }
        public void Spend(double amount)
        {
            if (Budget < amount)
                throw new InvalidOperationException(string.Format(ExceptionMessages.UnsufficientBudget));
            Budget -= amount;
        }
        public void Profit(double amount)
        {
            Budget += amount;
        }
        public string PlanetInfo()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Plane: {Name}");
            sb.AppendLine($"--Budget: {Budget} billion QUID");
            sb.AppendLine(units.Models.Any() ? $"--Forces: {string.Join(", ", units)}" : "--Forces: No units");
            sb.AppendLine(weapons.Models.Any() ? $"--Combat equipment: {String.Join(", ", weapons)}" : "--Combat equipment: No weapons");
            sb.Append($"--Military Power: {MilitaryPower}");
            return sb.ToString().TrimEnd();
        }
    }
}
