namespace EDriveRent.Models
{
    using System;
    using Contracts;
    using Utilities.Messages;

    public class User : IUser
    {
        private string firstName;
        private string lastName;
        private string drivingLicenseNumber;
        private double rating;
        private bool isBlocked;
        public User(string firstName, string lastName, string drivingLicenceNumber)
        {
            FirstName = firstName;
            LastName = lastName;
            DrivingLicenseNumber = drivingLicenceNumber;
            rating = 0;
            isBlocked = false;
        }
        public string FirstName
        {
            get => firstName;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException(string.Format(ExceptionMessages.FirstNameNull));
                firstName = value;
            }
        }
        public string LastName
        {
            get => lastName;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException(string.Format(ExceptionMessages.LastNameNull));
                lastName = value;
            }
        }
        public double Rating => rating;

        public string DrivingLicenseNumber
        {
            get => drivingLicenseNumber;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException(string.Format(ExceptionMessages.DrivingLicenseRequired));
                drivingLicenseNumber = value;
            }
        }
        public bool IsBlocked => isBlocked;
        public void IncreaseRating()
        {
            rating += 0.5;
            if (rating == 10.0)
                rating = 10.0;
        }
        public void DecreaseRating()
        {
            rating -= 2.0;
            if (rating <= 0.0)
            {
                rating = 0.0;
                isBlocked = !isBlocked;
            }
        }
        public override string ToString()
            => $"{FirstName} {LastName} Driving license: {DrivingLicenseNumber} Rating: {Rating}";
    }
}
