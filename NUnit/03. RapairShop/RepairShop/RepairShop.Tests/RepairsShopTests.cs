namespace RepairShop.Tests
{
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class Tests
    {
        public class RepairsShopTests
        {
            Garage garage;
            Car vehicle;
            [SetUp]
            public void SetUp()
            {
                garage = new Garage("BestAuto", 2);
                vehicle = new Car("Porsche 911 GT3 R", 3);
            }
            [Test]
            public void Constructor_Should_SetNameCorrectly()
            {
                Assert.That(garage.Name, Is.EqualTo("BestAuto"));
            }
            [Test]
            public void Constructor_ThrowsException_InvalidPlanetName()
            {
                Assert.Throws<ArgumentNullException>(() => new Garage(null, 2), "Invalid garage name.");
            }
            [Test]
            public void Constructor_Should_SetMechanicsAvailableCount()
            {
                Assert.That(garage.MechanicsAvailable, Is.EqualTo(2));
            }
            [TestCase(0)]
            [TestCase(-1)]
            public void Constructor_ThrowsException_InvalidMechanicsCount(int avaibleMechanics)
            {
                Assert.Throws<ArgumentException>(() => new Garage("BestAuto", avaibleMechanics), "At least one mechanic must work in the garage.");
            }
            [Test]
            public void Constructor_Correctly_CreatesCollectionOfAvailableMechanics()
            {
                Assert.That(garage.MechanicsAvailable, Is.EqualTo(2));
            }
            [Test]
            public void Constructor_Weapon_Correctly_CreatesGarage()
            {
                Assert.That(garage.Name, Is.EqualTo("BestAuto"));
                Assert.That(garage.MechanicsAvailable, Is.EqualTo(2));
            }
            [Test]
            public void Constructor_Vehicle_Correctly_CreatesCar()
            {
                Assert.That(vehicle.CarModel, Is.EqualTo("Porsche 911 GT3 R"));
                Assert.That(vehicle.NumberOfIssues, Is.EqualTo(3));
            }
            [Test]
            public void AddWCar_CarIsAddedToPlanetCollectionOfCar()
            {
                garage.AddCar(vehicle);
                Assert.That(garage.CarsInGarage, Is.EqualTo(1));
            }
            [Test]
            public void AddWCar_CarIsAddedButDontHaveAvailbleMechanics()
            {
                garage.AddCar(vehicle);
                var secondCar = new Car("SecondCar", 2);
                var thirdCar = new Car("ThirdCar", 3);
                garage.AddCar(secondCar);
                Assert.Throws<InvalidOperationException>(() => garage.AddCar(thirdCar), "No mechanic available.");
            }
            [Test]
            public void FixedCar_TestWorkProperly()
            {
                garage.AddCar(vehicle);
                garage.FixCar("Porsche 911 GT3 R");
                Assert.That(vehicle.NumberOfIssues, Is.EqualTo(0));
            }
            [Test]
            public void FixCar_ThrowExceptionWhenCatDoesntExist()
            {
                Assert.Throws<InvalidOperationException>(() => garage.FixCar("Porsche 911 GT3 R"), "The car Porsche 911 GT3 R doesn't exist.");
            }
            [Test]
            public void RemovedCarWhenBeenFixed()
            {
                garage.AddCar(vehicle);
                garage.FixCar("Porsche 911 GT3 R");
                Assert.That(vehicle.IsFixed, Is.EqualTo(true));
                garage.RemoveFixedCar();
                Assert.That(garage.CarsInGarage, Is.EqualTo(0));
            }
            [Test]
            public void RemovedCarWhenBeenFixedThrowExceptionWhenDoesntHave()
            {
                Assert.Throws<InvalidOperationException>(() => garage.RemoveFixedCar(), "No fixed cars available.");
            }
            [Test]
            public void Garage_Report_ReturnsCorrectString()
            {
                var firstVehicle = new Car("Ferrari SF90", 2);
                var secondVehicle = new Car("Nissan GT-R (R35)", 3);
                garage.AddCar(firstVehicle);
                garage.AddCar(secondVehicle);
                var expectedUnfixedCarCount = 2;
                var expectedOutputMessage = $"There are {expectedUnfixedCarCount} which are not fixed: {firstVehicle.CarModel}, {secondVehicle.CarModel}.";
                Assert.That(garage.Report(), Is.EqualTo(expectedOutputMessage));
            }
        }
    }
}