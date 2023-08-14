namespace BankLoan.Repositories
{
    using System.Collections.Generic;
    using System.Linq;
    using Contracts;
    using Models.Contracts;
    public class LoanRepository : IRepository<ILoan>
    {
        private List<ILoan> models;

        public LoanRepository()
        {
            this.models = new List<ILoan>();
        }
        public IReadOnlyCollection<ILoan> Models => models.AsReadOnly();

        public void AddModel(ILoan model)
        => models.Add(model);

        public ILoan FirstModel(string name)
        => this.models.FirstOrDefault(x => x.GetType().Name == name);

        public bool RemoveModel(ILoan model)
        => this.models.Remove(model);
    }

}
