namespace BankLoan.Models
{
    using System;
    using System.Linq;
    using System.Text;
    using System.Collections.Generic;
    using Contracts;
    using Utilities.Messages;
    public abstract class Bank : IBank
    {
        private string name;
        private int capacity;
        private List<ILoan> loans;
        private List<IClient> clients;

        protected Bank(string name, int capacity)
        {
            Name = name;
            Capacity = capacity;
            this.loans = new List<ILoan>();
            this.clients = new List<IClient>();
        }

        public string Name
        {
            get => name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.BankNameNullOrWhiteSpace);
                }
                this.name = value;
            }
        }
        public int Capacity
        {
            get => capacity;
            private set => capacity = value;
        }

        public IReadOnlyCollection<ILoan> Loans => this.loans;

        public IReadOnlyCollection<IClient> Clients => this.clients;

        public void AddClient(IClient client)
        {
            if (this.Clients.Count < this.Capacity)
            {
                this.clients.Add(client);
            }
        }

        public void AddLoan(ILoan loan) => this.loans.Add(loan);

        public double SumRates()
        {
            if (this.Loans.Count == 0)
                return 0;
            return double.Parse(this.Loans.Select(x => x.InterestRate).Sum().ToString());
        }

        public string GetStatistics()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Name: {Name}, Type: {GetType().Name}");
            sb.Append($"Clients: ");
            sb.Append(clients.Count == 0 ? "none" : string.Join(" ", clients.Select(x => x.Name)));
            sb.AppendLine($"Loans: {loans.Count}, Sum of Rates: {SumRates()}");
            return sb.ToString().TrimEnd();
        }

        public void RemoveClient(IClient Client) => this.clients.Remove(Client);
    }
}
