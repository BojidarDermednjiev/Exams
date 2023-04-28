namespace PlanetWars.Repositories
{
    using PlanetWars.Models.MilitaryUnits.Contracts;
    using PlanetWars.Repositories.Contracts;
    using System.Collections.Generic;
    using System.Linq;

    public class UnitRepository : IRepository<IMilitaryUnit>
    {
        private readonly List<IMilitaryUnit> units;
        public UnitRepository()
        {
            units = new List<IMilitaryUnit>();
        }
        public IReadOnlyCollection<IMilitaryUnit> Models => units;

        public void AddItem(IMilitaryUnit model)
        {
            units.Add(model);
        }
        public IMilitaryUnit FindByName(string name)
            => units.FirstOrDefault(x => x.GetType().Name == name);
        public bool RemoveItem(string name)
            => units.Remove(units.FirstOrDefault(x => x.GetType().Name == name));
    }
}
