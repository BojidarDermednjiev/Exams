namespace BookingApp.Core
{
    using System;
    using System.Linq;
    using System.Text;
    using Contracts;
    using Repositories;
    using Models.Rooms;
    using Models.Hotels;
    using Models.Bookings;
    using Utilities.Messages;
    using Models.Rooms.Contracts;
    using Models.Hotels.Contacts;
    using Models.Bookings.Contracts;

    public class Controller : IController
    {
        private readonly HotelRepository hotels;
        public Controller()
        {
            hotels = new HotelRepository();
        }
        public string AddHotel(string hotelName, int category)
        {
            if (hotels.Select(hotelName) != null)
                return string.Format(OutputMessages.HotelAlreadyRegistered, hotelName);
            IHotel hotel = new Hotel(hotelName, category);
            hotels.AddNew(hotel);
            return string.Format(OutputMessages.HotelSuccessfullyRegistered, category, hotelName);
        }
        public string UploadRoomTypes(string hotelName, string roomTypeName)
        {
            if (hotels.Select(hotelName) == default)
                return string.Format(OutputMessages.HotelNameInvalid, hotelName);
            IHotel hotel = hotels.Select(hotelName);
            if (hotel.Rooms.Select(roomTypeName) != default)
                return string.Format(OutputMessages.RoomTypeAlreadyCreated);
            IRoom roomToAdd;
            switch (roomTypeName)
            {
                case "Apartment": roomToAdd = new Apartment(); break;
                case "DoubleBed": roomToAdd = new DoubleBed(); break;
                case "Studio": roomToAdd = new Studio(); break;
                default: throw new ArgumentException(ExceptionMessages.RoomTypeIncorrect);
            }
            hotel.Rooms.AddNew(roomToAdd);
            return string.Format(OutputMessages.RoomTypeAdded, roomTypeName, hotelName);
        }
        public string SetRoomPrices(string hotelName, string roomTypeName, double price)
        {
            if (hotels.Select(hotelName) == default)
                return string.Format(OutputMessages.HotelNameInvalid);
            if (roomTypeName != nameof(DoubleBed) && roomTypeName != nameof(Studio) && roomTypeName != nameof(Apartment))
                throw new ArgumentException(string.Format(ExceptionMessages.RoomTypeIncorrect));
            IHotel hotel = hotels.Select(hotelName);
            if (hotel.Rooms.Select(roomTypeName) == default)
                return string.Format(OutputMessages.RoomTypeNotCreated);
            IRoom room = hotel.Rooms.Select(roomTypeName);
            if (room.PricePerNight > 0)
                throw new InvalidOperationException(string.Format(ExceptionMessages.PriceAlreadySet));
            room.SetPrice(price);
            return string.Format(OutputMessages.PriceSetSuccessfully, roomTypeName, hotelName);
        }
        public string BookAvailableRoom(int adults, int children, int duration, int category)
        {
            if (hotels.All().FirstOrDefault(x => x.Category == category) == default)
                return string.Format(OutputMessages.CategoryInvalid, category);
            var orderedHotels = hotels.All().Where(x => x.Category == category).OrderBy(x => x.Turnover).ThenBy(x => x.FullName);
            foreach (var hotel in orderedHotels)
            {
                var selectedRoom = hotel.Rooms.All().Where(x => x.PricePerNight > 0).Where(x => x.BedCapacity >= adults + children).OrderBy(x => x.BedCapacity).FirstOrDefault();
                if (selectedRoom != null)
                {
                    int bookingNumber = hotels.All().Sum(x => x.Bookings.All().Count) + 1;
                    IBooking booking = new Booking(selectedRoom, duration, adults, children, bookingNumber);
                    hotel.Bookings.AddNew(booking);
                    return string.Format(OutputMessages.BookingSuccessful, bookingNumber, hotel.FullName);
                }
            }
            return string.Format(OutputMessages.RoomNotAppropriate);
        }

        public string HotelReport(string hotelName)
        {
            var sb = new StringBuilder();
            IHotel hotel = hotels.Select(hotelName);
            if (hotel == null)
                return string.Format(OutputMessages.HotelNameInvalid, hotelName);
            sb.AppendLine($"Hotel name: {hotel.FullName}");
            sb.AppendLine($"--{hotel.Category} star hotel");
            sb.AppendLine($"--Turnover: {hotel.Turnover:f2}");
            sb.AppendLine($"--Bokings:");
            sb.AppendLine();
            if (hotel.Bookings.All().Count == 0)
                sb.AppendLine("none");
            else
                foreach (var booking in hotel.Bookings.All())
                {
                    sb.AppendLine($"{booking.BookingSummary()}");
                    sb.AppendLine();
                }
            return sb.ToString().TrimEnd();
        }
    }
}
