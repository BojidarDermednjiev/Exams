using NUnit.Framework;
using System;
using System.ComponentModel;

namespace PlanetWars.Tests
{
    public class Tests
    {
        [TestFixture]
        public class PlanetWarsTests
        {
            private Weapon weapon;
            private Planet planet;

            [SetUp]
            public void SetUp()
            {
                weapon = new Weapon("Laser", 1000.0, 5);
                planet = new Planet("Earth", 500.0);
            }
            [Test]
            public void Constructor_Should_SetNameCorrectly()
            {
                Assert.That(planet.Name, Is.EqualTo("Earth"));
            }
            [Test]
            public void Constructor_ThrowsException_InvalidPlanetName()
            {
                Assert.Throws<ArgumentException>(() => new Planet(null, 500.0), "Invalid planet Name");
            }
            [Test]
            public void Constructor_ThrowsException_InvalidBudget()
            {
                Assert.Throws<ArgumentException>(() => new Planet("Earth", -1), "Budget cannot drop below Zero!");
            }
            [Test]
            public void Weapon_PriceCannotBeNagative()
            {
                Assert.Throws<ArgumentException>(() => new Weapon("Lazer", -1, 5), "Price cannot be negative.");
            }
            [Test]
            public void Constructor_Correctly_CreatesCollectionOfWeapons()
            {
                Assert.That(planet.Weapons.Count, Is.EqualTo(0));
            }
            [Test]
            public void Constructor_Weapon_Correctly_CreatesNewWeapon()
            {
                Assert.That(weapon.Name, Is.EqualTo("Laser"));
                Assert.That(weapon.DestructionLevel, Is.EqualTo(5));
                Assert.That(weapon.Price, Is.EqualTo(1000.00));
            }
            [Test]
            public void AddWeapon_WeaponIsAddedToPlanetCollectionOfWeapons()
            {
                planet.AddWeapon(weapon);
                Assert.That(planet.Weapons.Count, Is.EqualTo(1));
            }
            [Test]
            public void AddWeapon_AlreadyAddedWeapon()
            {
                planet.AddWeapon(weapon);
                Assert.Throws<InvalidOperationException>(() => planet.AddWeapon(weapon), $"There is already a {weapon.Name} weapon.");
            }
            [Test]
            public void DestructionLevel_DestructionLevelIsReturned()
            {
                var secondWeapon = new Weapon("WeaponName", 500.0, 2);
                planet.AddWeapon(weapon);
                planet.AddWeapon(secondWeapon);
                Assert.That(planet.MilitaryPowerRatio, Is.EqualTo(7));
            }
            [Test]
            public void Profit_BudgetIncreasesWithGivenAmount()
            {
                planet.Profit(100);
                Assert.That(planet.Budget, Is.EqualTo(600));
            }
            [Test]
            public void SpendFunds_BudgetDecreasesWithGivenAmount()
            {
                planet.SpendFunds(100);
                Assert.That(planet.Budget, Is.EqualTo(400));
            }
            [Test]
            public void SpendFunds_BudgetCannotDropBelowZero()
            {
                Assert.Throws<InvalidOperationException>(() => planet.SpendFunds(501), "Not enough funds to finalize the deal.");
            }
            [Test]
            public void Weapon_IncreaseDestructionLevel_WorksProperly()
            {
                weapon.IncreaseDestructionLevel();
                Assert.That(weapon.DestructionLevel, Is.EqualTo(6));
            }
            [Test]
            public void Weapon_IsNuclear_WorksProperly()
            {
                var weaponNuclear = new Weapon("Nuclear", 1500, 11);
                var weaponGun = new Weapon("Gun", 20, 2);

                Assert.That(weaponNuclear.IsNuclear, Is.EqualTo(true));
                Assert.That(weaponGun.IsNuclear, Is.EqualTo(false));
            }
            [Test]
            public void RemoveWeapon_WorksProperly()
            {
                var wepondTwo = new Weapon("WeaponTwo", 500.0, 2);
                planet.AddWeapon(weapon);
                planet.AddWeapon(wepondTwo);
                Assert.That(planet.MilitaryPowerRatio, Is.EqualTo(7));
                planet.RemoveWeapon("WeaponTwo");
                Assert.That(planet.MilitaryPowerRatio, Is.EqualTo(5));
                Assert.That(planet.Weapons.Count, Is.EqualTo(1));
            }
            [Test]
            public void UpgradeWeapon_WorksProperly()
            {
                planet.AddWeapon(weapon);
                planet.UpgradeWeapon("Laser");
                Assert.That(planet.MilitaryPowerRatio, Is.EqualTo(6));
            }
            [Test]
            public void UpgradeWeapon_WeaponDoesNotExist()
            {
                Assert.Throws<InvalidOperationException>(() => planet.UpgradeWeapon("NotExistWepon"), $"NotExistWepon does not exist in the weapon repository of {planet.Name}");
            }
            [Test]
            public void DestructOpponent_Throws_IfOpponentIsTooStrong()
            {
                var firstPlanet = new Planet("Earth", 500.0);
                var secondPlanet = new Planet("Marce", 400.0);
                var firstPlanetWeapon = new Weapon("Lazer", 250.00, 5);
                var seccondPlanetWeapon = new Weapon("LazerGenobi", 249.0, 6);
                firstPlanet.AddWeapon(firstPlanetWeapon);
                secondPlanet.AddWeapon(seccondPlanetWeapon);
                Assert.Throws<InvalidOperationException>(() => firstPlanet.DestructOpponent(secondPlanet), $"{secondPlanet.Name} is too strong to declare war to!");
            }
            [Test]
            public void DestructOpponent_WorksProperly()
            {
                var firstPlanet = new Planet("Earth", 500.0);
                var secondPlanet = new Planet("Marce", 400.0);
                var firstPlanetWeapon = new Weapon("Lazer", 250.00, 8);
                var secondPlanetWeapon = new Weapon("LazerGenobi", 249.0, 6);
                firstPlanet.AddWeapon(firstPlanetWeapon);
                secondPlanet.AddWeapon(secondPlanetWeapon);
                var expectedMessage = $"{secondPlanet.Name} is destructed!";
                Assert.That(firstPlanet.DestructOpponent(secondPlanet), Is.EqualTo(expectedMessage));
            } 
        }
    }
}
