namespace Handball.Repositories
{
    using System.Collections.Generic;
    using System.Linq;
    using Contracts;
    using Models.Contracts;
    public class TeamRepository : IRepository<ITeam>
    {
        private readonly List<ITeam> models;
        public TeamRepository()
        {
            models = new List<ITeam>();
        }
        public IReadOnlyCollection<ITeam> Models => models.AsReadOnly();

        public void AddModel(ITeam model)
            => models.Add(model);
        public bool RemoveModel(string name)
        {
            if (models.Any(x => x.Name == name))
            {
                var find = models.First(x => x.Name == name);
                models.Remove(find);
                return true;
            }
            return false;
        }
        public bool ExistsModel(string name)
        {
            if (models.Any(x => x.Name == name))
                return true;
            return false;
        }

        public ITeam GetModel(string name)
            => models.FirstOrDefault(x => x.Name == name);
    }
}
