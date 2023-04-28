namespace UniversityCompetition.Repositories
{
    using System.Collections.Generic;
    using System.Linq;
    using Models.Contracts;
    using Contracts;
    public class UniversityRepository : IRepository<IUniversity>
    {
        private List<IUniversity> universities;
        public UniversityRepository()
        {
            universities = new List<IUniversity>();
        }
        public IReadOnlyCollection<IUniversity> Models => universities;
        public void AddModel(IUniversity model)
        {
            universities.Add(model);
        }
        public IUniversity FindById(int id)
         => universities.FirstOrDefault(x => x.Id == id);
        public IUniversity FindByName(string name)
         => universities.FirstOrDefault(x => x.Name == name);
    }
}
