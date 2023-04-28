namespace RobotService.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Contracts;
    using RobotService.Utilities.Messages;

    public abstract class Robot : IRobot
    {
        private string model;
        private int batteryCapacity;
        private int batteryLevel;
        private int convertionCapacityIndex;
        private List<int> interfaceStandards;
        protected Robot(string model, int batteryCapacity, int conversionCapacityIndex)
        {
            Model= model;
            BatteryCapacity= batteryCapacity;
            batteryLevel = batteryCapacity;
            convertionCapacityIndex = conversionCapacityIndex;
            interfaceStandards = new List<int>();
        }
        public string Model
        {
            get => model;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException(String.Format(ExceptionMessages.ModelNullOrWhitespace));
                model = value;
            }
        }

        public int BatteryCapacity
        {
            get => batteryCapacity;
            private set
            {
                if (value < 0)
                    throw new ArgumentException(String.Format(ExceptionMessages.BatteryCapacityBelowZero));
                batteryCapacity = value;
            }
        }

        public int BatteryLevel => batteryLevel;

        public int ConvertionCapacityIndex => convertionCapacityIndex;

        public IReadOnlyCollection<int> InterfaceStandards => interfaceStandards;

        public void Eating(int minutes)
        {
            int totalCapacity = ConvertionCapacityIndex * minutes;
            if (totalCapacity > BatteryCapacity - BatteryLevel)
                batteryLevel = batteryCapacity;
            else
                batteryLevel += totalCapacity;
        }
        public void InstallSupplement(ISupplement supplement)
        {
            BatteryCapacity -= supplement.BatteryUsage;
            batteryLevel -= supplement.BatteryUsage;
            interfaceStandards.Add(supplement.InterfaceStandard);
        }
        public bool ExecuteService(int consumedEnergy)
        {
            if (consumedEnergy <= this.batteryLevel)
            {
                this.batteryLevel -= consumedEnergy;
                return true;
            }
            return false;
        }
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"{GetType().Name} {Model}:");
            sb.AppendLine($"Maximum battery capacity: {BatteryCapacity}");
            sb.AppendLine($"Current battery level: {BatteryLevel}");
            sb.Append($"--Supplements installed: ");
            sb.Append(InterfaceStandards.Count == 0 ? "none" : string.Join(" ", this.InterfaceStandards));
            return sb.ToString().TrimEnd();
        }
    }
}
