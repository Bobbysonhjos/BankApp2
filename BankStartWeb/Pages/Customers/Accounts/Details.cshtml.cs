using AutoMapper;
using BankStartWeb.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BankStartWeb.Pages.Customers.Accounts

{
    [Authorize(Roles ="Admin, Cashier")]

    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public DetailsModel(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public AccountDetailViewModel Account { get; private set; }

       

        public class AccountDetailViewModel
        {
            public int Id { get; set; }

            public string AccountType { get; set; }

            public DateTime Created { get; set; }
            public decimal Balance { get; set; }

            public List<TransactionViewModel> Transactions { get; set; } = new List<TransactionViewModel>();


        }
        public class TransactionViewModel
        {
            public int Id { get; set; }

           
            public string Type { get; set; }
            
            public string Operation { get; set; }
            public DateTime Date { get; set; }
            public decimal Amount { get; set; }
            public decimal NewBalance { get; set; }
        }

        public IActionResult OnGetFetchMore(int customerId, int pageNum)
        {
            var query = context.Accounts.Where(e => e.Id == customerId)
                .SelectMany(e => e.Transactions)
                .OrderByDescending(e => e.Date);

            var r = query.GetPaged(pageNum, 5);

            var list = r.Results.Select(e => new TransactionViewModel
            {
                Id = e.Id,
                Amount = e.Amount,
                Type = e.Type,
                Operation = e.Operation,
                Date = e.Date,
                NewBalance = e.NewBalance
            }).ToList();

            var lastPage = pageNum == r.PageCount;

            return new JsonResult(new { items = list, lastPage = lastPage });
        }

        public IActionResult OnGet(int Id)
        {
            Account = mapper.Map<AccountDetailViewModel>(context.Accounts.Include(x => x.Transactions.OrderByDescending(t => t.Date).Take(5)).FirstOrDefault(x => x.Id == Id));
            if (Account == null) return Redirect("/Index");
            return Page();

        }

    }
}
