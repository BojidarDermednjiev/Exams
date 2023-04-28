namespace UniversityCompetition.Repositories
{
    using System.Collections.Generic;
    using System.Linq;
    using Models.Contracts;
    using Repositories.Contracts;
    public class SubjectRepository : IRepository<ISubject>
    {
        private List<ISubject> subjects;
        public SubjectRepository()
        {
            subjects = new List<ISubject>();
        }
        public IReadOnlyCollection<ISubject> Models => subjects;
        public void AddModel(ISubject model)
        {
            subjects.Add(model);
        }
        public ISubject FindById(int id)
         => subjects.FirstOrDefault(x => x.Id == id);
        public ISubject FindByName(string name)
         => this.subjects.FirstOrDefault(x => x.Name == name);
    }
}
