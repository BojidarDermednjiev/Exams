namespace BookingApp.Models.Hotels
{
    using Contacts;
    using Bookings.Contracts;
    using Rooms.Contracts;
    using Repositories.Contracts;
    using System.Collections.Generic;
    using BookingApp.Models.Rooms;
    using BookingApp.Models.Bookings;
    using System;
    using BookingApp.Utilities.Messages;
    using System.Linq;
    using BookingApp.Repositories;

    public class Hotel : IHotel
    {
        private string fullName;
        private int category;
        public Hotel(string fullName, int category)
        {
            FullName = fullName;
            Category = category;
            Rooms = new RoomRepository();
            Bookings = new BookingRepository();
        }

        public string FullName
        {
            get => fullName;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException(string.Format(ExceptionMessages.HotelNameNullOrEmpty));
                fullName = value;
            }
        }

        public int Category
        {
            get => category;
            private set
            {
                if (value < 1 && value > 5)
                    throw new ArgumentException(string.Format(ExceptionMessages.InvalidCategory));
                category = value;
            }
        }
        public double Turnover => Math.Round(Bookings.All().Sum(x => x.ResidenceDuration * x.Room.PricePerNight), 2);
        public IRepository<IRoom> Rooms { get; private set; }

        public IRepository<IBooking> Bookings { get; private set; }
    }
}
