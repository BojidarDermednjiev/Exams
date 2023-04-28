namespace BookingApp.Models.Bookings
{
    using BookingApp.Utilities.Messages;
    using Contracts;
    using Rooms.Contracts;
    using System;
    using System.Text;

    public class Booking : IBooking
    {
        private int residenceDuration;
        private int adultsCount;
        private int childrenCount;
        private int bookingNumber;
        public Booking(IRoom room, int residenceDuration, int adultsCount, int childrenCount, int bookingNumber)
        {
            Room = room;
            ResidenceDuration = residenceDuration;
            AdultsCount = adultsCount;
            ChildrenCount = childrenCount;
            this.bookingNumber = bookingNumber;
        }
        public IRoom Room { get; private set; }

        public int ResidenceDuration
        {
            get => residenceDuration;
            private set
            {
                if (value <= 0)
                    throw new ArgumentException(string.Format(ExceptionMessages.DurationZeroOrLess));
                residenceDuration = value;
            }
        }
        public int AdultsCount
        {
            get => adultsCount;
            private set
            {
                if (value <= 0)
                    throw new ArgumentException(string.Format(ExceptionMessages.AdultsZeroOrLess));
                adultsCount = value;
            }
        }
        public int ChildrenCount
        {
            get => childrenCount;
            private set
            {
                if (value < 0)
                    throw new ArgumentException(string.Format(ExceptionMessages.ChildrenNegative));
                childrenCount = value;
            }
        }
        public int BookingNumber => bookingNumber;
        public string BookingSummary()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Booking number: {BookingNumber}");
            sb.AppendLine($"Room type: {Room.GetType().Name}");
            sb.AppendLine($"Adults: {AdultsCount} Children: {ChildrenCount}");
            sb.AppendLine($"Total amount paid: {TotalPaid():f2}");
            return sb.ToString().TrimEnd();
        }
        private double TotalPaid()
            => Math.Round(residenceDuration * Room.PricePerNight, 2);
    }
}
