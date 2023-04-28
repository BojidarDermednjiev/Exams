namespace UniversityCompetition.Models
{
    using System;
    using System.Collections.Generic;
    using UniversityCompetition.Models.Contracts;
    using UniversityCompetition.Utilities.Messages;

    public class University : IUniversity
    {
        private int id;
        private string name;
        private string category;
        private int capacity;
        private List<int> requiredSubjects;
        public University(int universityId, string universityName, string category, int capacity, List<int> requiredSubjects)
        {
            Id = universityId;
            Name = universityName;
            Category = category;
            Capacity = capacity;
            this.requiredSubjects = requiredSubjects;
        }
        public int Id { get => id; private set => id = value; }

        public string Name
        {
            get => name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException(string.Format(ExceptionMessages.NameNullOrWhitespace));
                name = value;
            }
        }

        public string Category
        {
            get => category;
            private set
            {
                if (value != "Technical" && value != "Economical" && value != "Humanity")
                    throw new ArgumentException(string.Format(ExceptionMessages.CategoryNotAllowed));
                category = value;
            }
        }

        public int Capacity
        {
            get => capacity;
            private set
            {
                if (value < 0)
                    throw new ArgumentException(string.Format(ExceptionMessages.CapacityNegative));
                capacity = value;
            }
        }

        public IReadOnlyCollection<int> RequiredSubjects => requiredSubjects.AsReadOnly();
    }
}
