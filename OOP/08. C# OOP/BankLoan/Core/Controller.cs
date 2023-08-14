namespace BankLoan.Core
{
    using Contracts;
    using Repositories;
    using Repositories.Contracts;
    using Models.Contracts;
    using BankLoan.Utilities.Messages;
    using BankLoan.Models;
    using System.Net.Security;
    using System;
    using System.Linq;
    using System.Text;
    using System.Xml.Linq;

    public class Controller : IController
    {
        private IRepository<ILoan> loans;
        private IRepository<IBank> banks;

        public Controller()
        {
            this.loans = new LoanRepository();
            this.banks = new BankRepository();
        }
        public string AddBank(string bankTypeName, string name)
        {
            IBank bank = bankTypeName switch
            {
                nameof(BranchBank) => new BranchBank(name),
                nameof(CentralBank) => new CentralBank(name),
                _ => throw new ArgumentException(string.Format(ExceptionMessages.BankTypeInvalid))
            };
            banks.AddModel(bank);
            return string.Format(OutputMessages.BankSuccessfullyAdded, bankTypeName);
        }

        public string AddClient(string bankName, string clientTypeName, string clientName, string id, double income)
        {
            IClient client = clientTypeName switch
            {
                nameof(Adult) => new Adult(clientName, id, income),
                nameof(Student) => new Student(clientName, id, income),
                _ => throw new ArgumentException(string.Format(ExceptionMessages.LoanTypeInvalid))
            };
            var bank = banks.FirstModel(bankName);
            if ((bank.GetType().Name == nameof(BranchBank) && clientTypeName != nameof(Student)) ||
                (bank.GetType().Name == nameof(CentralBank) && clientTypeName != nameof(Adult)))
                return string.Format(OutputMessages.UnsuitableBank);
            bank.AddClient(client);
            return string.Format(OutputMessages.ClientAddedSuccessfully, clientTypeName, bankName);
        }

        public string AddLoan(string loanTypeName)
        {
            ILoan loan = loanTypeName switch
            {
                nameof(MortgageLoan) => new MortgageLoan(),
                nameof(StudentLoan) => new StudentLoan(),
                _ => throw new ArgumentException(string.Format(ExceptionMessages.LoanTypeInvalid))
            };
            loans.AddModel(loan);
            return string.Format(OutputMessages.LoanSuccessfullyAdded, loanTypeName);
        }

        public string FinalCalculation(string bankName)
        {
            IBank bank = banks.Models.FirstOrDefault(x => x.Name == bankName);
            var sumLoans = bank.Loans.Sum(l => l.Amount);
            var sumClients = bank.Clients.Sum(c => c.Income);
            string funds = (sumLoans + sumClients).ToString();
            return string.Format(OutputMessages.BankFundsCalculated, bankName, funds);
        }

        public string ReturnLoan(string bankName, string loanTypeName)
        {
            ILoan loan = loans.FirstModel(loanTypeName);
            IBank bank = this.banks.FirstModel(bankName);
            if (loan == null)
                throw new ArgumentException(string.Format(ExceptionMessages.MissingLoanFromType, loanTypeName));
            bank.AddLoan(loan);
            loans.RemoveModel(loan);
            return string.Format(OutputMessages.LoanReturnedSuccessfully, loanTypeName, bankName);
        }

        public string Statistics()
        {
            var sb = new StringBuilder();
            foreach (var bank in this.banks.Models)
                sb.AppendLine(bank.GetStatistics());
            return sb.ToString().TrimEnd();
        }
    }
}
