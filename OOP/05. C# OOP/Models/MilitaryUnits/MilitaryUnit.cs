namespace PlanetWars.Models.MilitaryUnits
{
    using Contracts;
    using PlanetWars.Utilities.Messages;
    using System;

    public abstract class MilitaryUnit : IMilitaryUnit
    {
        private double cost;
        private int enduranceLevel;
        protected MilitaryUnit(double cost)
        {
            Cost = cost;
            this.enduranceLevel = 1;
        }
        public double Cost { get => cost; private set => cost = value; }
        public int EnduranceLevel => enduranceLevel;
        public void IncreaseEndurance()
        {
            if (enduranceLevel == 20)
                throw new ArgumentException(string.Format(ExceptionMessages.EnduranceLevelExceeded));
            enduranceLevel++;
        }
    }
}
