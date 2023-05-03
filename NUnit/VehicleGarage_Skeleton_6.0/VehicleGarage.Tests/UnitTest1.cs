namespace VehicleGarage.Tests
{
    using NUnit.Framework;
    [TestFixture]
    public class GarageTests
    {
        [Test]
        public void AddVehicle_WhenGarageIsNotFullAndLicensePlateNumberIsUnique_ReturnsTrue()
        {
            // Arrange
            var garage = new Garage(2);
            var vehicle1 = new Vehicle("Ford", "Mustang", "ABC123");
            var vehicle2 = new Vehicle("Tesla", "Model S", "DEF456");

            // Act
            bool result1 = garage.AddVehicle(vehicle1);
            bool result2 = garage.AddVehicle(vehicle2);

            // Assert
            Assert.That(result1, Is.True);
            Assert.That(result2, Is.True);
            Assert.That(garage.Vehicles.Count, Is.EqualTo(2));
            Assert.That(garage.Vehicles.Contains(vehicle1), Is.True);
            Assert.That(garage.Vehicles.Contains(vehicle2), Is.True);
        }

        [Test]
        public void AddVehicle_WhenGarageIsFull_ReturnsFalse()
        {
            // Arrange
            var garage = new Garage(1);
            var vehicle1 = new Vehicle("Ford", "Mustang", "ABC123");
            var vehicle2 = new Vehicle("Tesla", "Model S", "DEF456");

            // Act
            bool result1 = garage.AddVehicle(vehicle1);
            bool result2 = garage.AddVehicle(vehicle2);

            // Assert
            Assert.That(result1, Is.True);
            Assert.That(result2, Is.False);
            Assert.That(garage.Vehicles.Count, Is.EqualTo(1));
            Assert.That(garage.Vehicles.Contains(vehicle1), Is.True);
            Assert.That(garage.Vehicles.Contains(vehicle2), Is.False);
        }

        [Test]
        public void AddVehicle_WhenLicensePlateNumberIsNotUnique_ReturnsFalse()
        {
            // Arrange
            var garage = new Garage(2);
            var vehicle1 = new Vehicle("Ford", "Mustang", "ABC123");
            var vehicle2 = new Vehicle("Tesla", "Model S", "ABC123");

            // Act
            bool result1 = garage.AddVehicle(vehicle1);
            bool result2 = garage.AddVehicle(vehicle2);

            // Assert
            Assert.That(result1, Is.True);
            Assert.That(result2, Is.False);
            Assert.That(garage.Vehicles.Count, Is.EqualTo(1));
            Assert.That(garage.Vehicles.Contains(vehicle1), Is.True);
            Assert.That(garage.Vehicles.Contains(vehicle2), Is.False);
        }
        [Test]
        public void ChargeVehicles_ChargeOneVehicle_ReturnsOne()
        {
            // Arrange
            Garage garage = new Garage(2);
            Vehicle vehicle1 = new Vehicle("Brand1", "Model1", "123");
            Vehicle vehicle2 = new Vehicle("Brand2", "Model2", "456");
            garage.AddVehicle(vehicle1);
            garage.AddVehicle(vehicle2);
            vehicle1.BatteryLevel = 50;
            vehicle2.BatteryLevel = 80;
            // Act
            int chargedVehicles = garage.ChargeVehicles(50);

            // Assert
            Assert.AreEqual(1, chargedVehicles);
            Assert.AreEqual(100, vehicle1.BatteryLevel);
            Assert.AreEqual(80, vehicle2.BatteryLevel);
        }

        [Test]
        public void ChargeVehicles_ChargeAllVehicles_ReturnsAll()
        {
            // Arrange
            Garage garage = new Garage(3);
            Vehicle vehicle1 = new Vehicle("Brand1", "Model1", "123");
            Vehicle vehicle2 = new Vehicle("Brand2", "Model2", "456");
            Vehicle vehicle3 = new Vehicle("Brand3", "Model3", "789");
            garage.AddVehicle(vehicle1);
            garage.AddVehicle(vehicle2);
            garage.AddVehicle(vehicle3);
            vehicle1.BatteryLevel = 50;
            vehicle2.BatteryLevel = 80;
            vehicle3.BatteryLevel = 50;
            // Act
            int chargedVehicles = garage.ChargeVehicles(80);

            // Assert
            Assert.AreEqual(3, chargedVehicles);
            Assert.AreEqual(100, vehicle1.BatteryLevel);
            Assert.AreEqual(100, vehicle2.BatteryLevel);
            Assert.AreEqual(100, vehicle3.BatteryLevel);
        }

        [Test]
        public void ChargeVehicles_NoVehiclesToCharge_ReturnsZero()
        {
            // Arrange
            Garage garage = new Garage(1);

            // Act
            int chargedVehicles = garage.ChargeVehicles(50);

            // Assert
            Assert.AreEqual(0, chargedVehicles);
        }
        [Test]
        public void DriveVehicle_VehicleExists()
        {
            // Arrange
            var garage = new Garage(1);
            var vehicle = new Vehicle("Toyota", "Camry", "ABC123");
            garage.AddVehicle(vehicle);
            vehicle.BatteryLevel = 50;
            // Act
            garage.DriveVehicle("ABC123", 60, false);

            // Assert
            Assert.AreEqual(50, vehicle.BatteryLevel);
        }

        [Test]
        public void DriveVehicle_VehicleExistsAndNotDamagedAndBatteryLevelInsufficient_VehicleBatteryLevelNotChanged()
        {
            // Arrange
            var garage = new Garage(1);
            var vehicle = new Vehicle("Toyota", "Camry", "ABC123");
            garage.AddVehicle(vehicle);

            // Act
            garage.DriveVehicle("ABC123", 110, false);

            // Assert
            Assert.AreEqual(100, vehicle.BatteryLevel);
        }

        [Test]
        public void DriveVehicle_VehicleExistsAndDamaged_VehicleBatteryLevelNotChanged()
        {
            // Arrange
            var garage = new Garage(1);
            var vehicle = new Vehicle("Toyota", "Camry", "ABC123");
            garage.AddVehicle(vehicle);
            vehicle.IsDamaged = true;

            // Act
            garage.DriveVehicle("ABC123", 30, false);

            // Assert
            Assert.AreEqual(100, vehicle.BatteryLevel);
        }

        [Test]
        public void DriveVehicle_VehicleNotExists_VehicleBatteryLevelNotChanged()
        {
            // Arrange
            var garage = new Garage(1);
            var vehicle = new Vehicle("Toyota", "Camry", "ABC123");
            garage.AddVehicle(vehicle);

            // Act
            garage.DriveVehicle("ABC123", 30, false);

            // Assert
            Assert.AreEqual(70, vehicle.BatteryLevel);
        }


        [Test]
        public void DriveVehicle_AccidentOccured_VehicleIsDamaged()
        {
            // Arrange
            var garage = new Garage(1);
            var vehicle = new Vehicle("Toyota", "Camry", "ABC123");
            garage.AddVehicle(vehicle);

            // Act
            garage.DriveVehicle("ABC123", 30, true);

            // Assert
            Assert.IsTrue(vehicle.IsDamaged);
        }

        [Test]
        public void DriveVehicle_BatteryDrainageGreaterThan100_VehicleBatteryLevelNotChanged()
        {
            // Arrange
            var garage = new Garage(1);
            var vehicle = new Vehicle("Toyota", "Camry", "ABC123");
            garage.AddVehicle(vehicle);

            // Act
            garage.DriveVehicle("ABC123", 110, false);

            // Assert
            Assert.AreEqual(100, vehicle.BatteryLevel);
        }
        [Test]
        public void TestRepairVehicles()
        {
            // Create a garage with a capacity of 2
            Garage garage = new Garage(2);

            // Create two damaged vehicles and add them to the garage
            Vehicle vehicle1 = new Vehicle("Toyota", "Camry", "ABC123");
            vehicle1.IsDamaged = true;
            garage.AddVehicle(vehicle1);

            Vehicle vehicle2 = new Vehicle("Honda", "Civic", "XYZ789");
            vehicle2.IsDamaged = true;
            garage.AddVehicle(vehicle2);

            // Call the RepairVehicles method and assert that it returns the expected value
            string result = garage.RepairVehicles();
            Assert.AreEqual("Vehicles repaired: 2", result);

            // Assert that both vehicles in the garage have been repaired
            Assert.IsFalse(vehicle1.IsDamaged);
            Assert.IsFalse(vehicle2.IsDamaged);
        }
    }
}