namespace Heroes.Core
{
    using Contracts;
    using Heroes.Models.Heroes;
    using Heroes.Models.Map;
    using Heroes.Models.Weapons;
    using Heroes.Utilities.Messages;
    using Models.Contracts;
    using Repositories;
    using Repositories.Contracts;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Controller : IController
    {
        private IRepository<IHero> heroes;
        private IRepository<IWeapon> weapons;
        public Controller()
        {
            heroes = new HeroRepository();
            weapons = new WeaponRepository();
        }
        public string CreateHero(string type, string name, int health, int armour)
        {
            var findHero = heroes.FindByName(name);
            if (findHero != null)
                throw new InvalidOperationException(string.Format(OutputMessages.HeroAlreadyExist, name));
            if (!new string[] { "Knight", "Barbarian" }.Contains(type))
                throw new InvalidOperationException(string.Format(OutputMessages.HeroTypeIsInvalid));
            IHero hero;
            if (type == nameof(Knight))
            {
                hero = new Knight(name, health, armour);
                heroes.Add(hero);
                return string.Format(OutputMessages.SuccessfullyAddedKnight, name);
            }
            else
            {
                hero = new Barbarian(name, health, armour);
                heroes.Add(hero);
                return string.Format(OutputMessages.SuccessfullyAddedBarbarian, name);
            }
        }
        public string CreateWeapon(string type, string name, int durability)
        {
            var findWeapon = weapons.FindByName(name);
            if (findWeapon != null)
                throw new InvalidOperationException(string.Format(OutputMessages.WeaponAlreadyExists, name));
            if (!new string[] { "Claymore", "Mace" }.Contains(type))
                throw new InvalidOperationException(string.Format(OutputMessages.WeaponTypeIsInvalid));
            IWeapon weapon;
            if (type == nameof(Mace))
                weapon = new Mace(name, durability);
            else
                weapon = new Claymore(name, durability);
            weapons.Add(weapon);
            return string.Format(OutputMessages.WeaponAddedSuccessfully, type.ToLower(), name);
        }
        public string AddWeaponToHero(string weaponName, string heroName)
        {
            var hero = heroes.FindByName(heroName);
            var weapon = weapons.FindByName(weaponName);
            if (hero == null)
                throw new InvalidOperationException(string.Format(OutputMessages.HeroDoesNotExist, heroName));
            if (weapon == null)
                throw new InvalidOperationException(string.Format(OutputMessages.WeaponDoesNotExist, weaponName));
            if (hero.Weapon != null)
                throw new InvalidOperationException(string.Format(OutputMessages.HeroAlreadyHasWeapon, heroName));
            hero.AddWeapon(weapon);
            weapons.Remove(weapon);
            return string.Format(OutputMessages.WeaponAddedToHero, heroName, weapon.GetType().Name.ToLower());
        }
        public string StartBattle()
        {
            var mapCreate = new Map();
            ICollection<IHero> battleHeroes = heroes.Models.Where(x => x.IsAlive && x.Weapon != null).ToList();
            return mapCreate.Fight(battleHeroes);
        }
        public string HeroReport()
        {
            var sb = new StringBuilder();
            foreach (var hero in heroes.Models.OrderBy(x => x.GetType().Name).ThenByDescending(x => x.Health).ThenBy(x => x.Name))
            {
                sb.AppendLine($"{hero.GetType().Name}: {hero.Name}");
                sb.AppendLine($"--Health: {hero.Health}");
                sb.AppendLine($"--Armour: {hero.Armour}");
                sb.Append("--Weapon: ");
                sb.AppendLine(hero.Weapon == null ? "Unarmed" : hero.Weapon.Name);
            }
            return sb.ToString().TrimEnd();
        }
    }
}
