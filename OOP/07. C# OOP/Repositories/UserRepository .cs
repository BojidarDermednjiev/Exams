namespace EDriveRent.Repositories
{
    using EDriveRent.Models.Contracts;
    using EDriveRent.Repositories.Contracts;
    using System.Collections.Generic;
    using System.Linq;

    public class UserRepository : IRepository<IUser>
    {
        private readonly List<IUser> users;
        public UserRepository()
        {
            users = new List<IUser>();
        }
        public void AddModel(IUser model)
        {
            users.Add(model);
        }
        public IUser FindById(string identifier)
            => users.FirstOrDefault(x => x.DrivingLicenseNumber == identifier);
        public IReadOnlyCollection<IUser> GetAll()
            => users.AsReadOnly();
        public bool RemoveById(string identifier)
            => users.Remove(this.FindById(identifier));
    }
}
