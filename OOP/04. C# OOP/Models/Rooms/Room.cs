namespace BookingApp.Models.Rooms
{
    using BookingApp.Utilities.Messages;
    using Contracts;
    using System;

    public abstract class Room : IRoom
    {
        private int bedCapacity;
        private double pricePerNight;
        protected Room(int bedCapacity)
        {
            this.bedCapacity = bedCapacity;
        }
        public int BedCapacity => bedCapacity; 

        public double PricePerNight
        {
            get => pricePerNight;
            private set
            {
                if (value < 0)
                    throw new ArgumentException(string.Format(ExceptionMessages.PricePerNightNegative));
                pricePerNight = value;
            }
        }
        public void SetPrice(double price)
        {
            this.PricePerNight = price;
        }
    }
}
