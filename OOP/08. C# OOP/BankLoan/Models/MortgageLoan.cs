namespace BankLoan.Models
{
    public class MortgageLoan : Loan
    {
        private const int mortgageLoanInterestRate = 3;
        private const double mortgageLoanAmount = 50000;
        public MortgageLoan() : base(mortgageLoanInterestRate, mortgageLoanAmount) { }
    }
}
