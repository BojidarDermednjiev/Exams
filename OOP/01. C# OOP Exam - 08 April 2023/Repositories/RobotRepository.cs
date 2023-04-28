namespace RobotService.Repositories
{
    using RobotService.Models.Contracts;
    using RobotService.Repositories.Contracts;
    using System.Collections.Generic;
    using System.Linq;

    public class RobotRepository : IRepository<IRobot>
    {
        private readonly List<IRobot> robots;
        public RobotRepository()
        {
            robots = new List<IRobot>();
        }
        public void AddNew(IRobot model)
        {
            robots.Add(model);
        }

        public IRobot FindByStandard(int interfaceStandard)
         => robots.FirstOrDefault(x => x.InterfaceStandards.Any(x => x == interfaceStandard));
        public IReadOnlyCollection<IRobot> Models()
            => robots.AsReadOnly();
        public bool RemoveByName(string typeName)
         => robots.Remove(robots.FirstOrDefault(x => x.Model == typeName));
    }
}
