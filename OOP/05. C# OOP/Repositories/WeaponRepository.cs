namespace PlanetWars.Repositories
{
    using PlanetWars.Models.Weapons.Contracts;
    using PlanetWars.Repositories.Contracts;
    using System.Collections.Generic;
    using System.Linq;
    public class WeaponRepository : IRepository<IWeapon>
    {
        private readonly List<IWeapon> weapons;
        public WeaponRepository()
        {
            weapons = new List<IWeapon>();
        }
        public IReadOnlyCollection<IWeapon> Models => weapons;

        public void AddItem(IWeapon model)
        {
            weapons.Add(model);
        }

        public IWeapon FindByName(string name)
            => weapons.FirstOrDefault(x => x.GetType().Name == name);
        public bool RemoveItem(string name)
            => weapons.Remove(weapons.FirstOrDefault(x => x.GetType().Name == name));
    }
}
