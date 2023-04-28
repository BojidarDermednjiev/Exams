namespace PlanetWars.Repositories
{
    using PlanetWars.Models.Planets.Contracts;
    using PlanetWars.Repositories.Contracts;
    using System.Collections.Generic;
    using System.Linq;

    public class PlanetRepository : IRepository<IPlanet>
    {
        private readonly List<IPlanet> planets;
        public PlanetRepository()
        {
            planets = new List<IPlanet>();
        }
        public IReadOnlyCollection<IPlanet> Models => planets;

        public void AddItem(IPlanet model)
        {
            planets.Add(model);
        }

        public IPlanet FindByName(string name)
            => planets.Find(x => x.Name == name);
        public bool RemoveItem(string name)
            => planets.Remove(planets.FirstOrDefault(x => x.Name == name));
    }
}
