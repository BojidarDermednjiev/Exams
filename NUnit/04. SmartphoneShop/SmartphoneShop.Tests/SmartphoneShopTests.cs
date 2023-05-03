namespace SmartphoneShop.Tests
{
    using System;
    using NUnit.Framework;

    [TestFixture]
    public class SmartphoneShopTests
    {
        Shop shop;
        Smartphone phone;
        [SetUp]
        public void SetUp()
        {
            shop = new Shop(2);
            phone = new Smartphone("Xiomi Redmi 8", 80);
        }
        [Test]
        public void Constructor_SmartPhoneSetCorrectly()
        {
            Assert.That(phone.ModelName, Is.EqualTo("Xiomi Redmi 8"));
            Assert.That(phone.MaximumBatteryCharge, Is.EqualTo(80));
            Assert.That(phone.CurrentBateryCharge, Is.EqualTo(80));
        }
        [Test]
        public void Constructor_ShopSetCorrectly()
        {
            Assert.That(shop.Capacity, Is.EqualTo(2));
        }
        [Test]
        public void Constructor_ShopCapacityIsNegativeThrowException()
        {
            Assert.Throws<ArgumentException>(() => new Shop(-1), "Invalid capacity.");
        }
        [Test]
        public void Constructor_CorrectlyPhoneCapacity()
        {
            Assert.That(shop.Count, Is.EqualTo(0));
        }
        [Test]
        public void AddPhone_SmartPhoneAddToShopCorrecly()
        {
            shop.Add(phone);
            Assert.That(shop.Count, Is.EqualTo(1));
        }
        [Test]
        public void Shop_CheckForCorrectlyCapacy()
        {
            Assert.That(shop.Capacity, Is.EqualTo(2));
        }
        [Test]
        public void AddPhone_SmartPhoneAddToShopTwiceShoulThrowException()
        {
            shop.Add(phone);
            Assert.Throws<InvalidOperationException>(() => shop.Add(phone), $"The phone model {phone.ModelName} already exist.");
        }
        [Test]
        public void AddPhone_SmartPhoneAddToShopMoreThenCapacityOfTheShopShoulThrowException()
        {
            var phoneOne = new Smartphone("Nokia", 80);
            var phoneTwo = new Smartphone("Samsung", 70);
            var phoneThree = new Smartphone("IPhone", 60);
            shop.Add(phoneOne);
            shop.Add(phoneTwo);
            Assert.Throws<InvalidOperationException>(() => shop.Add(phoneThree), "The shop is full.");
        }
        [Test]
        public void Remove_SmartPhoneRemoveToShopCorrecly()
        {
            shop.Add(phone);
            shop.Remove("Xiomi Redmi 8");
            Assert.That(shop.Count, Is.EqualTo(0));
        }
        [Test]
        public void Remove_WhenTryRemoveSmartPhoneDoesntExistShoutThrowException()
        {
            Assert.Throws<InvalidOperationException>(() => shop.Remove("Xiomi Redmi 8"));
        }
        [Test]
        public void TestPhone_TestSmartPhoneFromShopCorrecly()
        {
            shop.Add(phone);
            shop.TestPhone("Xiomi Redmi 8", 30);
            Assert.That(phone.CurrentBateryCharge, Is.EqualTo(50));
        }
        [TestCase(81)]
        [TestCase(90)]
        public void TestPhone_TestSmartPhoneButDoesntHaveEnoughBatteryForTesting(int usageTime)
        {
            shop.Add(phone);
            Assert.Throws<InvalidOperationException>(() => shop.TestPhone("Xiomi Redmi 8", usageTime), $"The phone model {phone.ModelName} is low on batery.");

        }
        [Test]
        public void TestPhone_TestSmartPhoneButDoesntExist()
        {
            Assert.Throws<InvalidOperationException>(() => shop.TestPhone("Xiomi Redmi 8", 50), $"The phone model {phone.ModelName} doesn't exist.");
        }
        [Test]
        public void ChargePhone_SmartPhoneTryToChargeCorrecly()
        {
            shop.Add(phone);
            shop.ChargePhone("Xiomi Redmi 8");
            Assert.That(phone.CurrentBateryCharge, Is.EqualTo(phone.MaximumBatteryCharge));
        }
        [Test]
        public void ChargePhone_SmartPhoneTryToChargeButDoesntExistingPhone()
        {
            Assert.Throws<InvalidOperationException>(() => shop.ChargePhone("Xiomi Redmi 8"), $"The phone model {phone.ModelName} doesn't exist.");
        }
    }
}