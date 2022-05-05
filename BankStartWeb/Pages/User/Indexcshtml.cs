using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BankStartWeb.Pages.User
{
    public class IndexModel: PageModel
    {
        private readonly ILogger<IndexModel>? _logger;
        private readonly ApplicationDbContext? _context;
    }

    public class User
    {
        public string UserName { get; set; }
    }
}
