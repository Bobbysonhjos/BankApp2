using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BankStartWeb.Pages
{
    public class IndexModel : PageModel
    {

        
        private readonly ILogger<IndexModel> _logger;
        private readonly ApplicationDbContext context;

        public IndexModel(ILogger<IndexModel> logger, ApplicationDbContext context)
        {
            _logger = logger;
            this.context = context;
        }

        public int CustomerCount { get; private set; }
        public int AccountCount { get; private set; }
        public decimal BalanceCount { get; private set; }

        public void OnGet()
        {
             CustomerCount = context.Customers.Count();
             AccountCount = context.Accounts.Count();
             BalanceCount = context.Accounts.Sum(x => x.Balance); 
        }
    }
}