namespace Handball.Repositories
{
    using System.Collections.Generic;
    using System.Linq;
    using Contracts;
    using Models.Contracts;
    public class PlayerRepository : IRepository<IPlayer>
    {
        private readonly List<IPlayer> models;
        public PlayerRepository()
        {
            models = new List<IPlayer>();
        }
        public IReadOnlyCollection<IPlayer> Models => models.AsReadOnly();

        public void AddModel(IPlayer model)
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

        public IPlayer GetModel(string name)
            => models.FirstOrDefault(x => x.Name == name);

    }
}
