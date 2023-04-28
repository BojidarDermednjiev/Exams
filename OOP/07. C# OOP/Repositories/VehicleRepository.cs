using EDriveRent.Models.Contracts;
using EDriveRent.Repositories.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace EDriveRent.Repositories
{
    public class VehicleRepository : IRepository<IVehicle>
    {
        private readonly List<IVehicle> vehicles;
        public VehicleRepository()
        {
            vehicles = new List<IVehicle>();
        }
        public void AddModel(IVehicle model)
        {
            vehicles.Add(model);
        }

        public IVehicle FindById(string identifier)
            => vehicles.FirstOrDefault(x => x.LicensePlateNumber == identifier);

        public IReadOnlyCollection<IVehicle> GetAll()
            => vehicles.AsReadOnly();
        public bool RemoveById(string identifier)
            => vehicles.Remove(this.FindById(identifier));
    }
}
