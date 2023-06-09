﻿namespace BookingApp.Repositories
{
    using BookingApp.Models.Rooms.Contracts;
    using BookingApp.Repositories.Contracts;
    using System.Collections.Generic;
    using System.Linq;

    public class RoomRepository : IRepository<IRoom>
    {
        private List<IRoom> rooms;
        public RoomRepository()
        {
            rooms = new List<IRoom>();
        }
        public void AddNew(IRoom model)
        {
            rooms.Add(model);
        }

        public IReadOnlyCollection<IRoom> All()
         => rooms;

        public IRoom Select(string criteria)
            => rooms.FirstOrDefault(x => x.GetType().Name == criteria);
    }
}
