namespace BankLoan.Models
{
    public class StudentLoan : Loan
    {
        private const int studentInterestRate = 1;
        private const double studentAmount = 10000;
        public StudentLoan() : base(studentInterestRate, studentAmount) { }
    }
}
