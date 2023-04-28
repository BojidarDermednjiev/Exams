namespace UniversityCompetition.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Contracts;
    using Models.Contracts;
    public class StudentRepository : IRepository<IStudent>
    {
        private List<IStudent> students;
        public StudentRepository()
        {
            students = new List<IStudent>();
        }
        public IReadOnlyCollection<IStudent> Models => students;
        public void AddModel(IStudent model)
        {
            students.Add(model);
        }
        public IStudent FindById(int id)
        => students.FirstOrDefault(x => x.Id == id);
        public IStudent FindByName(string name)
        {
            var firstName = name.Split(" ")[0];
            var lastName = name.Split(" ")[1];
            return students.FirstOrDefault(x => x.FirstName == firstName && x.LastName == lastName);
        }
    }
}
