namespace BookingApp.Repositories
{
    using System.Collections.Generic;
    using System.Linq;
    using Contracts;
    using Models.Hotels.Contacts;
    public class HotelRepository : IRepository<IHotel>
    {
        private List<IHotel> hotels;
        public HotelRepository()
        {
            hotels = new List<IHotel>();
        }
        public void AddNew(IHotel model)
        {
            hotels.Add(model);
        }
        public IReadOnlyCollection<IHotel> All()
            => hotels;
        public IHotel Select(string criteria)
            => hotels.FirstOrDefault(x => x.FullName == criteria);
    }
}
