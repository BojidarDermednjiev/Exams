namespace EDriveRent.Repositories
{
    using EDriveRent.Models.Contracts;
    using EDriveRent.Repositories.Contracts;
    using System.Collections.Generic;
    using System.Linq;

    public class RouteRepository : IRepository<IRoute>
    {
        private readonly List<IRoute> routes;
        public RouteRepository()
        {
            routes = new List<IRoute>();
        }
        public void AddModel(IRoute model)
        {
            routes.Add(model);
        }
        public IRoute FindById(string identifier)
            => routes.FirstOrDefault(x => x.RouteId == int.Parse(identifier));
        public IReadOnlyCollection<IRoute> GetAll()
            => routes.AsReadOnly();
        public bool RemoveById(string identifier)
            => routes.Remove(this.FindById(identifier));
    }
}
