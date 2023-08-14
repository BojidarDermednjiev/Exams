namespace BankLoan.Repositories
{
    using System.Collections.Generic;
    using System.Linq;
    using Contracts;
    using Models.Contracts;
    public class BankRepository : IRepository<IBank>
    {
        private List<IBank> models;

        public BankRepository()
        {
            this.models = new List<IBank>();
        }
        public IReadOnlyCollection<IBank> Models => models.AsReadOnly();

        public void AddModel(IBank model)
        => models.Add(model);

        public IBank FirstModel(string name)
        => this.models.FirstOrDefault(x => x.Name == name);

        public bool RemoveModel(IBank model)
        => this.models.Remove(model);
    }
}
