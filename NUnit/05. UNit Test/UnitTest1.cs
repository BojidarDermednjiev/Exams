namespace SmartDevice.Tests
{
    using System;
    using NUnit.Framework;
    [TestFixture]
    public class Tests
    {
        Device device;
        [SetUp]
        public void Setup()
        {
            device = new Device(16);
        }

        [Test]
        public void MemoryCapacity()
        {
            Assert.AreEqual(16, device.MemoryCapacity);
        }
        [Test]
        public void TakePhotoFalse()
        {
            bool result = device.TakePhoto(17);
            var expectedMemAvali = 16;
            var expectedPhotosCount = 0;
            var actualMemAvali = device.AvailableMemory;
            var actualPhotosCount = device.Photos;
            Assert.IsFalse(result);
            Assert.AreEqual(expectedMemAvali, actualMemAvali);
            Assert.AreEqual(expectedPhotosCount, actualPhotosCount);
        }

        [Test]
        public void Photos()
        {
            Assert.That(device.Photos.Equals(0));
        }
        [Test]
        public void Photos_Get_ReturnsDefaultValue()
        {
            int result = device.Photos;
            Assert.AreEqual(0, result);
        }
        [Test]
        public void TakePhotWorkPro()
        {
            bool result = device.TakePhoto(14);
            var expectedMemAvali = 2;
            var expectedPhotosCount = 1;
            var actualMemAvali = device.AvailableMemory;
            var actualPhotosCount = device.Photos;
            Assert.IsTrue(result);
            Assert.AreEqual(expectedMemAvali, actualMemAvali);
            Assert.AreEqual(expectedPhotosCount, actualPhotosCount);
        }
        [Test]
        public void InstallAppThrowExc()
        {
            Assert.Throws<InvalidOperationException>(() => device.InstallApp("facebook", 17), "Not enough available memory to install the app.");
            Assert.AreEqual(16, device.AvailableMemory);
            Assert.IsEmpty(device.Applications);
        }
        [Test]
        public void InstallAppWorkProp()
        {
            device.InstallApp("x", 14);
            var expectedMemAvali = 2;
            var actualMemAvali = device.AvailableMemory;
            var expectedApplic = 1;
            var actualApplic = device.Applications.Count;
            Assert.AreEqual(expectedMemAvali, actualMemAvali);
            Assert.AreEqual(expectedApplic, actualApplic);

        }
        [Test]
        public void ShowOutPut()
        {
            Assert.AreEqual(device.InstallApp("x", 14), "x is installed successfully. Run application?");
        }
        [Test]
        public void FormatDevice()
        {
            device.FormatDevice();
            var expectedPhotos = 0;
            var actualPhotos = device.Photos;
            var expectedApp = 0;
            var actualApp = device.Applications.Count;
            var expectedMemAva = 16;
            var actualMemAva = device.MemoryCapacity;
            Assert.AreEqual(expectedPhotos, actualPhotos);
            Assert.AreEqual(expectedApp, actualApp);
            Assert.AreEqual(expectedMemAva, actualMemAva);
        }
        [Test]
        public void GetDeviceStatusWorkProp()
        {
            device.TakePhoto(2);
            device.TakePhoto(2);
            device.InstallApp("x", 2);
            device.InstallApp("facebook", 2);
            device.InstallApp("instagram", 2);


            string expectedStatus = $"Memory Capacity: {device.MemoryCapacity} MB, Available Memory: {device.AvailableMemory} MB" +
                                    Environment.NewLine + $"Photos Count: {device.Photos}" +
                                    Environment.NewLine + $"Applications Installed: {string.Join(", ", device.Applications)}";

            string actualStatus = device.GetDeviceStatus();

            Assert.AreEqual(expectedStatus, actualStatus);
        }
    }
}