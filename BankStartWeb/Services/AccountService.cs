using Microsoft.EntityFrameworkCore;
using BankStartWeb.Data;

namespace BankStartWeb.Services
{
    public class AccountService : IAccountService
    {
        private readonly ApplicationDbContext _context;

        public AccountService(ApplicationDbContext context) { _context = context; }







        public IAccountService.ErrorCode MakeDeposit(int accountId, decimal amount)
        {
            if (amount <= 0)
            {
                return IAccountService.ErrorCode.AmountisNegative;
            }

            var account = _context.Accounts
                .Include(e => e.Transactions)
                .FirstOrDefault(e => e.Id == accountId);
            if (account == null) return IAccountService.ErrorCode.AccountNotFound;

            account.Balance += amount;

            var transaction = new Transaction();
            transaction.Type = "Debit";
            transaction.Amount = amount;
            transaction.Operation = "Deposit";
            transaction.Date = DateTime.Now;
            transaction.NewBalance = account.Balance;

            account.Transactions.Add(transaction);
            _context.SaveChanges();

            return IAccountService.ErrorCode.ok; // TODO ändra logiken så att den kollar att den är null.
        }


        public IAccountService.ErrorCode MakeWithdrawal(int accountId, decimal amount)
        {
            var account = _context.Accounts.FirstOrDefault(e => e.Id == accountId);

            if (account.Balance < amount)
            {
                return IAccountService.ErrorCode.BalanceIsToLow;
            }
            else if (amount < 0)
            {
                return IAccountService.ErrorCode.AmountisNegative;
            }

            account.Balance -= amount;

            var transaction = new Transaction();
            transaction.Type = "ATM";
            transaction.Amount = amount;
            transaction.Operation = "Withdraw";
            transaction.Date = DateTime.Now;
            transaction.NewBalance = account.Balance;
            account.Transactions.Add(transaction);
            _context.SaveChanges();

            return IAccountService.ErrorCode.ok;
        }



        public IAccountService.ErrorCode Transfer(int accountId, int receiverId, decimal amount)
        {
            if (amount < 0)
            {
                return IAccountService.ErrorCode.AmountisNegative;
            }

            var senderTransfer = _context.Accounts
                .Include(e => e.Transactions)
                .FirstOrDefault(e => e.Id == accountId);



            var receiverTransfer = _context.Accounts
                .Include(e => e.Transactions)
                .FirstOrDefault(e => e.Id == receiverId);

            var sender = new Transaction();
            {
                sender.Amount = amount;
                sender.Operation = "Transfer";
                sender.Date = DateTime.Now;
                sender.Type = "Credit";
                sender.NewBalance = senderTransfer.Balance - amount;
            }

            if (amount > senderTransfer.Balance)
            {
                return IAccountService.ErrorCode.BalanceIsToLow;
            }

            var receiver = new Transaction();
            {
                receiver.Amount = amount;
                receiver.Operation = "Transfer";
                receiver.Date = DateTime.Now;
                receiver.Type = "Credit";
                receiver.NewBalance = receiverTransfer.Balance + amount;
            }

            senderTransfer.Balance -= amount;
            receiverTransfer.Balance += amount;

            senderTransfer.Transactions.Add(sender);
            receiverTransfer.Transactions.Add(receiver);

            _context.SaveChanges();

            return IAccountService.ErrorCode.ok;
        }
    }
}

