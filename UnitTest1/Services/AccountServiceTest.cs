using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankStartWeb.Data;
using BankStartWeb.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest1.Services
{
    [TestClass]
    public class AccountServiceTest
    {
        private readonly AccountService _sut;
        private ApplicationDbContext _context;


        public AccountServiceTest()
        {

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "Test")
                .Options;
            _context = new ApplicationDbContext(options);
            _sut = new AccountService(_context);
        }

        [TestMethod]
        public void When_making_a_deposit_transaction_should_occur()
        {
            var accountId = 1;
            var amount = 100;
            _context.SeedAccountData();
            var response = _sut.MakeDeposit(accountId, amount);

            var account = _context.Accounts.First();

            Assert.AreEqual(IAccountService.ErrorCode.ok, response);
            Assert.AreEqual(1100, account.Balance);

        }

        [TestMethod]
        public void When_making_a_deposit_with_negative_amount_should_not_occur()
        {
            var accountId = 1;
            var amount = -100;
            _context.SeedAccountData();
            var response = _sut.MakeDeposit(accountId, amount);

            var account = _context.Accounts.First();

            Assert.AreEqual(IAccountService.ErrorCode.AmountisNegative, response);
            Assert.AreEqual(1000, account.Balance);

        }
        [TestMethod]
        public void Making_a_withdrawal_with_insufficient_balance_Should_not_be_possible()
        {
            var accountId = 1;
            var amount = 1001;
            _context.SeedAccountData();
            var response = _sut.MakeWithdrawal(accountId, amount);

            var account = _context.Accounts.First();

            Assert.AreEqual(IAccountService.ErrorCode.BalanceIsToLow, response);
            Assert.AreEqual(1000, account.Balance);

        }
        [TestMethod]
        public void Making_a_withdrawal_with_negative_amount_should_not_be_possible()
        {
            var accountId = 1;
            var amount = -100;
            _context.SeedAccountData();
            var response = _sut.MakeWithdrawal(accountId, amount);

            var account = _context.Accounts.First();

            Assert.AreEqual(IAccountService.ErrorCode.AmountisNegative, response);
            Assert.AreEqual(1000, account.Balance);

        }
        [TestMethod]
        public void Making_a_transfer_with_negative_amount_should_not_be_possible()
        {
            var accountId = 1;
            var amount = -100;
            var receiverId = 2;
            _context.SeedAccountData();
            var response = _sut.Transfer(accountId,receiverId, amount);

            var account = _context.Accounts.First();

            Assert.AreEqual(IAccountService.ErrorCode.AmountisNegative, response);
            Assert.AreEqual(1000, account.Balance);

        }
        [TestMethod]
        public void Making_a_transfer_with_insufficient_funds_should_not_be_possible()
        {
            var accountId = 1;
            var amount = 1001;
            var receiverId = 2;
            _context.SeedAccountData();
            var response = _sut.Transfer(accountId, receiverId, amount);

            var account = _context.Accounts.First();

            Assert.AreEqual(IAccountService.ErrorCode.BalanceIsToLow, response);
            Assert.AreEqual(1000, account.Balance);

        }
        
      


    }

    public static class SeedHelper
    {
        public static void SeedAccountData(this ApplicationDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Accounts.Add(new Account()
            {
                AccountType = "Personal",
                Balance = 1000,
                Created = DateTime.Now,
                Transactions = new List<Transaction>()
            }); 
            context.Accounts.Add(new Account()
            {
                AccountType = "Personal",
                Balance = 1000,
                Created = DateTime.Now,
                Transactions = new List<Transaction>()
            });
            context.SaveChanges();
        }
    }
}
