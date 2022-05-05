using System.ComponentModel.DataAnnotations;
using BankStartWeb.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BankStartWeb.Pages.Customers.Accounts
{
    public class TransferModel : PageModel
    {
        private readonly IAccountService accountService;

        public TransferModel(IAccountService accountService)
        {
            this.accountService = accountService;
        }
        [BindProperty]
        [Required]
        [Range(1, 10000000)]
        public decimal Amount { get; set; }
        [BindProperty(SupportsGet =true)]

        public int AccountId { get; private set; }


        public void OnGet(int accountId)
        {
            AccountId = accountId;
        }

        public void OnPost(int accountId)
        {
            if (ModelState.IsValid)
            {
                
            }
        }
    }
} 
