namespace EDriveRent.Models
{
    using Contracts;
    using Utilities.Messages;
    using System;
    using System.Text;

    public abstract class Vehicle : IVehicle
    {
        private string brand;
        private string model;
        private string licensePlateNumber;
        private int batteryLevel;
        private bool isDamage;
        protected Vehicle(string brand, string model, double maxMileage, string licensePlateNumber)
        {
            Brand = brand;
            Model = model;
            LicensePlateNumber = licensePlateNumber;
            MaxMileage = maxMileage;
            BatteryLevel = 100;
            IsDamaged = false;
        }
        public string Brand
        {
            get => brand;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException(string.Format(ExceptionMessages.BrandNull));
                brand = value;
            }
        }
        public string Model
        {
            get => model;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException(string.Format(ExceptionMessages.ModelNull));
                model = value;
            }
        }

        public double MaxMileage { get; private set; }

        public string LicensePlateNumber
        {
            get => licensePlateNumber;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException(string.Format(ExceptionMessages.LicenceNumberRequired));
                licensePlateNumber = value;
            }
        }

        public int BatteryLevel { get => batteryLevel; private set => batteryLevel = value; }

        public bool IsDamaged { get => isDamage; private set => isDamage = value; }
        public void Drive(double mileage)
        {
            double procentige = Math.Round(mileage / MaxMileage * 100);
            if (this.GetType().Name == nameof(CargoVan))
                procentige += 5;
            batteryLevel -= (int)procentige;
        }
        public void Recharge()
        {
            batteryLevel = 100;
        }
        public void ChangeStatus()
        {
            IsDamaged = !IsDamaged;
        }
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append($"{Brand} {Model} License plate: {LicensePlateNumber} Battery: {BatteryLevel}%");
            sb.Append(IsDamaged == true ? " Status: damage" : " Status: OK");
            return sb.ToString().TrimEnd();
        }
    }
}
