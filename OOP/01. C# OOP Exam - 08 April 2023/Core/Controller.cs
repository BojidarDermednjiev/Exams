namespace RobotService.Core
{
    using System.Linq;
    using System.Text;
    using Contracts;
    using Models;
    using Repositories;
    using Models.Contracts;
    using Utilities.Messages;

    internal class Controller : IController
    {
        private SupplementRepository supplements;
        private RobotRepository robots;
        public Controller()
        {
            supplements = new SupplementRepository();
            robots = new RobotRepository();
        }

        public string CreateRobot(string model, string typeName)
        {
            IRobot robot;
            if (typeName == nameof(DomesticAssistant))
                robot = new DomesticAssistant(model);
            else if (typeName == nameof(IndustrialAssistant))
                robot = new IndustrialAssistant(model);
            else
                return string.Format(OutputMessages.RobotCannotBeCreated, typeName);
            robots.AddNew(robot);
            return string.Format(OutputMessages.RobotCreatedSuccessfully, typeName, model);

        }

        public string CreateSupplement(string typeName)
        {
            ISupplement supplement;
            if (typeName == nameof(SpecializedArm))
                supplement = new SpecializedArm();
            else if (typeName == nameof(LaserRadar))
                supplement = new LaserRadar();
            else
                return string.Format(OutputMessages.SupplementCannotBeCreated, typeName);
            supplements.AddNew(supplement);
            return string.Format(OutputMessages.SupplementCreatedSuccessfully, typeName);
        }

        public string PerformService(string serviceName, int intefaceStandard, int totalPowerNeeded)
        {
            var selectedRobots = robots.Models().Where(x => x.InterfaceStandards.Any(x => x == intefaceStandard)).OrderByDescending(x => x.BatteryLevel);
            if (selectedRobots.Count() == 0)
                return string.Format(OutputMessages.UnableToPerform, intefaceStandard);
            var powerSum = selectedRobots.Sum(x => x.BatteryLevel);
            if (powerSum < totalPowerNeeded)
                return string.Format(OutputMessages.MorePowerNeeded, serviceName, totalPowerNeeded - powerSum);
            int usedRobotsCount = default;
            foreach (var robot in selectedRobots)
            {
                usedRobotsCount++;
                if (totalPowerNeeded <= robot.BatteryLevel)
                {
                    robot.ExecuteService(totalPowerNeeded);
                    break;
                }
                else
                {
                    totalPowerNeeded -= robot.BatteryLevel;
                    robot.ExecuteService(robot.BatteryLevel);
                }
            }
            return string.Format(OutputMessages.PerformedSuccessfully, serviceName, usedRobotsCount);
        }

        public string Report()
        {
            var sb = new StringBuilder();
            var robotReportCollection = robots.Models().OrderByDescending(x => x.BatteryLevel).ThenBy(x => x.BatteryCapacity);
            foreach ( var robot in robotReportCollection) 
                sb.AppendLine(robot.ToString());
            return sb.ToString().TrimEnd();
        }

        public string RobotRecovery(string model, int minutes)
        {
            var selectedRobots = robots.Models().Where(x => x.Model == model && x.BatteryLevel * 2 == x.BatteryCapacity);
            int fedCount = default;
            foreach (var robot in selectedRobots) 
            {
                robot.Eating(minutes);
                fedCount++;
            }
            return string.Format(OutputMessages.RobotsFed, fedCount);
        }

        public string UpgradeRobot(string model, string supplementTypeName)
        {
            ISupplement supplement = supplements.Models().FirstOrDefault(x => x.GetType().Name == supplementTypeName);
            var selectedModels = robots.Models().Where(r => r.Model == model);
            var stillNotUpgrade = selectedModels.Where(x => x.InterfaceStandards.All(x => x != supplement.InterfaceStandard));
            var robotForUpgrade = stillNotUpgrade.FirstOrDefault();
            if (robotForUpgrade == null)
                return string.Format(OutputMessages.AllModelsUpgraded, model);
            robotForUpgrade.InstallSupplement(supplement);
            supplements.RemoveByName(supplementTypeName);
            return string.Format(OutputMessages.UpgradeSuccessful, model, supplementTypeName);
        }
    }
}