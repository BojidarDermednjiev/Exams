namespace BookigApp.Tests
{
    using FrontDeskApp;
    using NUnit.Framework;
    using System;

    public class Tests
    {
        Room room;
        Hotel hotel;
        [SetUp]
        public void Setup()
        {
            room = new Room(2, 25);
            hotel = new Hotel("HotelName", 4);
        }

        [Test]
        public void AddRoom_AddsRoomsCorrectly()
        {
            hotel.AddRoom(room);
            Assert.That(hotel.Rooms.Count, Is.EqualTo(1));
        }
        [Test]
        public void HotelCtor_SetsNameAndCategoryCorrectly()
        {
            Assert.That(hotel.FullName, Is.EqualTo("HotelName"));
            Assert.That(hotel.Category, Is.EqualTo(4));
            Assert.That(hotel.Turnover, Is.EqualTo(0));
        }
        [Test]
        public void HotelCtor_ThrowsExceptionForInvalidNameAndCategory()
        {
            Assert.Throws<ArgumentNullException>(() => new Hotel(" ", 5));
            Assert.Throws<ArgumentException>(() => new Hotel("HotelName", 6));
            Assert.Throws<ArgumentException>(() => new Hotel("HotelName", 0));
        }
        [TestCase(-1, 2, 3, 200)]
        [TestCase(1, -1, 3, 200)]
        [TestCase(1, 1, 0, 200)]
        [TestCase(0, 1, 0, 200)]
        [TestCase(1, -1, 1, 200)]
        public void BookRoom_ThrowsException(int adults, int children, int residenceDuration, double budget)
        {
            hotel.AddRoom(room);
            Assert.Throws<ArgumentException>(() => hotel.BookRoom(adults, children, residenceDuration, budget));
        }
        [TestCase(4, 1, 2, 200)]
        public void BookRoom_NoBookingForNotEnoughBeds(int adults, int children, int residenceDuration, double budget)
        {
            hotel.AddRoom(room);
            hotel.BookRoom(adults, children, residenceDuration, budget);
            Assert.That(hotel.Turnover.Equals(0));
        }
        [TestCase(1, 1, 2, 50)]
        public void BookRoom_WorksProperly(int adults, int children, int residenceDuration, double budget)
        {
            hotel.AddRoom(room);
            hotel.BookRoom(adults, children, residenceDuration, budget);
            Assert.AreEqual(budget, hotel.Turnover);
            Assert.That(hotel.Bookings.Count.Equals(1));
            Assert.That(hotel.Rooms.Count.Equals(1));
        }
        [TestCase(1, 1, 2, 0)]
        public void BookRoom_NoBookingIfTooLowBudget(int adults, int children, int residenceDuration, double budget)
        {
            hotel.AddRoom(room);
            hotel.BookRoom(adults, children, residenceDuration, budget);
            Assert.AreEqual(budget, hotel.Turnover);
            Assert.That(hotel.Bookings.Count.Equals(0));
            Assert.That(hotel.Rooms.Count.Equals(1));
        }
    }
}