using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BankStartWeb.Pages.Customers.Accounts
{
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

        public IActionResult OnGet(int Id)
        {
            Account = mapper.Map<AccountDetailViewModel>(context.Accounts.Include(x => x.Transactions.OrderByDescending(t => t.Date)).FirstOrDefault(x => x.Id == Id));
            if (Account == null) return Redirect("/Index");
            return Page();  
                
        }

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

    }
}
