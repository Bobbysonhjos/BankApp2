using System.ComponentModel.DataAnnotations;
using BankStartWeb.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BankStartWeb.Pages.Customers.Accounts
{
    public class WithdrawalModel : PageModel
    {
        private readonly IAccountService accountService;

        public WithdrawalModel(IAccountService accountService)
        {
            this.accountService = accountService;
        }
        [BindProperty]
        [Required]
        [Range(1,10000000)]
        public decimal Amount { get; set; }

        [BindProperty(SupportsGet = true)]
        public int AccountId { get; private set; }


        public void OnGet(int accountId)
        {
            AccountId = accountId;
        }



        public IActionResult OnPost(int accountId)
        {
            if (ModelState.IsValid)
            {
                var response = accountService.MakeWithdrawal(accountId, Amount);
                if (response == IAccountService.ErrorCode.ok)
                    return RedirectToPage("./Details");



            }

            return Page();
        }




    }
}
    
        
       
        
       
