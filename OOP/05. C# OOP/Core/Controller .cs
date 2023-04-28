namespace PlanetWars.Core
{
    using PlanetWars.Core.Contracts;
    using PlanetWars.Models.MilitaryUnits;
    using PlanetWars.Models.MilitaryUnits.Contracts;
    using PlanetWars.Models.Planets;
    using PlanetWars.Models.Planets.Contracts;
    using PlanetWars.Models.Weapons;
    using PlanetWars.Models.Weapons.Contracts;
    using PlanetWars.Repositories;
    using PlanetWars.Utilities.Messages;
    using System;
    using System.Linq;
    using System.Text;

    public class Controller : IController
    {
        private PlanetRepository planets;
        public Controller()
        {
            planets = new PlanetRepository();
        }
        public string CreatePlanet(string name, double budget)
        {
            IPlanet planet = new Planet(name, budget);
            if (planets.FindByName(name) != default)
                return string.Format(OutputMessages.ExistingPlanet, name);
            planets.AddItem(planet);
            return string.Format(OutputMessages.NewPlanet, name);
        }
        public string AddUnit(string unitTypeName, string planetName)
        {
            IPlanet planet = planets.FindByName(planetName);
            if (planet == default)
                throw new InvalidOperationException(string.Format(ExceptionMessages.UnexistingPlanet, planetName));
            if (!new string[] { "SpaceForces", "StormTroopers", "AnonymousImpactUnit" }.Contains(unitTypeName))
                throw new InvalidOperationException(String.Format(ExceptionMessages.ItemNotAvailable, unitTypeName));
            if (planet.Army.Any(x => x.GetType().Name == unitTypeName))
                throw new InvalidOperationException(string.Format(ExceptionMessages.UnitAlreadyAdded, unitTypeName, planetName));
            IMilitaryUnit unit = null;
            switch (unitTypeName)
            {
                case "SpaceForces":
                    unit = new SpaceForces();
                    break;
                case "StormTroopers":
                    unit = new StormTroopers();
                    break;
                case "AnonymousImpactUnit":
                    unit = new AnonymousImpactUnit();
                    break;
            }
            planet.Spend(unit.Cost);
            planet.AddUnit(unit);
            return string.Format(OutputMessages.UnitAdded, unitTypeName, planetName);
        }

        public string AddWeapon(string planetName, string weaponTypeName, int destructionLevel)
        {
            IPlanet planet = planets.FindByName(planetName);
            if (planet == default)
                throw new InvalidOperationException(string.Format(ExceptionMessages.UnexistingPlanet, planetName));
            if (!new string[] { "BioChemicalWeapon", "NuclearWeapon", "SpaceMissiles" }.Contains(weaponTypeName))
                throw new InvalidOperationException(string.Format(ExceptionMessages.ItemNotAvailable, weaponTypeName));
            if (planet.Weapons.Any(x => x.GetType().Name == weaponTypeName))
                throw new InvalidOperationException(string.Format(ExceptionMessages.WeaponAlreadyAdded, weaponTypeName, planetName));
            IWeapon weapon = null;
            switch (weaponTypeName)
            {
                case "BioChemicalWeapon":
                    weapon = new BioChemicalWeapon(destructionLevel);
                    break;
                case "NuclearWeapon":
                    weapon = new NuclearWeapon(destructionLevel);
                    break;
                case "SpaceMissiles":
                    weapon = new SpaceMissiles(destructionLevel);
                    break;
            }
            planet.Spend(weapon.Price);
            planet.AddWeapon(weapon);
            return string.Format(OutputMessages.WeaponAdded, planetName, weaponTypeName);
        }
        public string SpecializeForces(string planetName)
        {
            IPlanet planet = planets.FindByName(planetName);
            if (planet == default)
                throw new InvalidOperationException(string.Format(ExceptionMessages.UnexistingPlanet, planetName));
            else if (planet.Army.Count() == 0)
                throw new InvalidOperationException(string.Format(ExceptionMessages.NoUnitsFound));
            else
            {
                double budget = 1.25;
                planet.TrainArmy();
                planet.Spend(budget);
                return string.Format(OutputMessages.ForcesUpgraded, planetName);
            }
        }
        public string SpaceCombat(string planetOne, string planetTwo)
        {
            IPlanet firstPlanet = planets.FindByName(planetOne);
            IPlanet secondPlanet = planets.FindByName(planetTwo);
            var winner = "none";
            if (firstPlanet.MilitaryPower > secondPlanet.MilitaryPower)
                winner = "first";
            else if (firstPlanet.MilitaryPower < secondPlanet.MilitaryPower)
                winner = "second";
            if (winner == "none")
            {
                bool firstHasNuclear = firstPlanet.Weapons.All(x => x.GetType().Name == "NuclearWeapon");
                bool secondHasNuclear = secondPlanet.Weapons.Any(x => x.GetType().Name == "NuclearWeapon");
                if (firstHasNuclear && !secondHasNuclear)
                    winner = "first";
                else if (!firstHasNuclear && secondHasNuclear)
                    winner = "second";
            }
            var output = string.Empty;
            switch (winner)
            {
                case "first":
                    output = CombatAfterFight(firstPlanet, secondPlanet);
                    break;
                case "second":
                    output = CombatAfterFight(secondPlanet, firstPlanet);
                    break;
                case "none":
                    firstPlanet.Spend(firstPlanet.Budget / 2);
                    secondPlanet.Spend(secondPlanet.Budget / 2);
                    output = string.Format(OutputMessages.NoWinner);
                    break;
            }
            return output;
        }

        private string CombatAfterFight(IPlanet winner, IPlanet loser)
        {
            double salvageProfit = loser.Army.Sum(x => x.Cost) + loser.Weapons.Sum(x => x.Price);
            winner.Spend(winner.Budget / 2);
            winner.Profit(loser.Budget / 2);
            winner.Profit(salvageProfit);
            planets.RemoveItem(loser.Name);
            return String.Format(OutputMessages.WinnigTheWar, winner.Name, loser.Name);
        }

        public string ForcesReport()
        {
            var sb = new StringBuilder();
            sb.AppendLine("***UNIVERSE PLANET MILITARY REPORT***");
            foreach (var planet in planets.Models.OrderByDescending(x => x.MilitaryPower).ThenBy(x => x.Name))
                sb.AppendLine(planet.PlanetInfo());
            return sb.ToString().TrimEnd();
        }
    }
}
