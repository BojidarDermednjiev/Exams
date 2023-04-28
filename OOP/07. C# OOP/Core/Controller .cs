namespace EDriveRent.Core
{
    using EDriveRent.Core.Contracts;
    using EDriveRent.Models;
    using EDriveRent.Models.Contracts;
    using EDriveRent.Repositories;
    using EDriveRent.Repositories.Contracts;
    using EDriveRent.Utilities.Messages;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Controller : IController
    {
        private IRepository<IVehicle> vehicles;
        private IRepository<IUser> users;
        private IRepository<IRoute> routes;
        public Controller()
        {
            vehicles = new VehicleRepository();
            users = new UserRepository();
            routes = new RouteRepository();
        }
        public string RegisterUser(string firstName, string lastName, string drivingLicenseNumber)
        {
            if (users.FindById(drivingLicenseNumber) != default)
                return string.Format(OutputMessages.UserWithSameLicenseAlreadyAdded, drivingLicenseNumber);
            IUser user = new User(firstName, lastName, drivingLicenseNumber);
            users.AddModel(user);
            return string.Format(OutputMessages.UserSuccessfullyAdded, firstName, lastName, drivingLicenseNumber);
        }
        public string UploadVehicle(string vehicleType, string brand, string model, string licensePlateNumber)
        {
            if (!new string[] { "CargoVan", "PassengerCar" }.Contains(vehicleType))
                return string.Format(OutputMessages.VehicleTypeNotAccessible, vehicleType);
            if (vehicles.FindById(licensePlateNumber) != default)
                return string.Format(OutputMessages.LicensePlateExists, licensePlateNumber);
            IVehicle vehicle = null;
            if (vehicleType == nameof(CargoVan))
                vehicle = new CargoVan(brand, model, licensePlateNumber);
            else if (vehicleType == nameof(PassengerCar))
                vehicle = new PassengerCar(brand, model, licensePlateNumber);
            vehicles.AddModel(vehicle);
            return string.Format(OutputMessages.VehicleAddedSuccessfully, brand, model, licensePlateNumber);
        }
        public string AllowRoute(string startPoint, string endPoint, double length)
        {
            var cheker = routes.GetAll();
            int countOfRoute = routes.GetAll().Count + 1;
            IRoute existingRoute = this.routes.GetAll().FirstOrDefault(r => r.StartPoint == startPoint && r.EndPoint == endPoint);
            if (existingRoute != default)
            {
                if (existingRoute.Length == length)
                    return string.Format(OutputMessages.RouteExisting, startPoint, endPoint, length);
                if (existingRoute.Length < length)
                    return string.Format(OutputMessages.RouteIsTooLong, startPoint, endPoint);
                else
                    existingRoute.LockRoute();
            }
            IRoute route = new Route(startPoint, endPoint, length, countOfRoute);
            routes.AddModel(route);
            return string.Format(OutputMessages.NewRouteAdded, startPoint, endPoint, length);
        }

        public string MakeTrip(string drivingLicenseNumber, string licensePlateNumber, string routeId, bool isAccidentHappened)
        {
            var user = users.FindById(drivingLicenseNumber);
            var vehcile = vehicles.FindById(licensePlateNumber);
            var route = routes.FindById(routeId);
            if (user.IsBlocked == true)
                return string.Format(OutputMessages.UserBlocked, drivingLicenseNumber);
            if (vehcile.IsDamaged == true)
                return string.Format(OutputMessages.VehicleDamaged, licensePlateNumber);
            if (route.IsLocked == true)
                return string.Format(OutputMessages.RouteLocked, routeId);
            vehcile.Drive(route.Length);
            if (isAccidentHappened)
            {
                vehcile.ChangeStatus();
                user.DecreaseRating();
            }
            else
                user.IncreaseRating();
            return vehcile.ToString();

        }
        public string RepairVehicles(int count)
        {
            var selectVehicleWhoWasAccident = vehicles.GetAll().Where(x => x.IsDamaged == true).OrderBy(x => x.Brand).ThenBy(x => x.Model).Take(count).ToList();
            selectVehicleWhoWasAccident.ForEach(x => x.Recharge());
            selectVehicleWhoWasAccident.ForEach(x => x.ChangeStatus());
            return string.Format(OutputMessages.RepairedVehicles, selectVehicleWhoWasAccident.Count);
        }
        public string UsersReport()
        {
            var sb = new StringBuilder();
            var selectedUsers = users.GetAll().OrderByDescending(x => x.Rating).ThenBy(x => x.LastName).ThenBy(x => x.FirstName);
            sb.AppendLine("*** E-Drive-Rent ***");
            foreach (var user in selectedUsers)
                sb.AppendLine(user.ToString());
            return sb.ToString().TrimEnd();
        }
    }
}
