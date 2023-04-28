namespace RobotService.Repositories
{
    using RobotService.Models.Contracts;
    using RobotService.Repositories.Contracts;
    using System.Collections.Generic;
    using System.Linq;

    public class SupplementRepository : IRepository<ISupplement>
    {
        private readonly List<ISupplement> supplements;
        public SupplementRepository()
        {
            supplements= new List<ISupplement>();
        }
        public void AddNew(ISupplement model)
        {
            supplements.Add(model);
        }

        public ISupplement FindByStandard(int interfaceStandard)
         => supplements.FirstOrDefault(x => x.InterfaceStandard == interfaceStandard);
        public IReadOnlyCollection<ISupplement> Models()
         => supplements.AsReadOnly();
        public bool RemoveByName(string typeName)
            => supplements.Remove(supplements.FirstOrDefault(x => GetType().Name == typeName)); 
    }
}
