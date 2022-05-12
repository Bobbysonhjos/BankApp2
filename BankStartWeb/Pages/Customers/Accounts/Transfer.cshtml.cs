using System.ComponentModel.DataAnnotations;
using System.Net.Mime;
using BankStartWeb.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BankStartWeb.Pages.Customers.Accounts
{
    public class TransferModel : PageModel
    {
        private readonly IAccountService accountService;
        private readonly ApplicationDbContext _context;

        public TransferModel(ApplicationDbContext context, IAccountService accountService )
        {
            
            this.accountService = accountService;
            _context = context;
            


        }
        [BindProperty]
        [Required]
        [Range(1, 10000000, ErrorMessage = "Please conduct a transfer within the allowed range of 1-10000000.")]
        public decimal Amount { get; set; }
        [BindProperty(SupportsGet =true)]

        public int AccountId { get; set; }
        [BindProperty]
        [Required]
        [Range(1, Int32.MaxValue, ErrorMessage= "Please conduct a transfer to a valid account!")]
        public int ReceiverId { get; set; }

        

       
        

        


        public void OnGet(int accountId)
        {
            
            
        }

        public IActionResult OnPost(int accountId)
        {
            var receiveraccountfound= 
            _context.Accounts.Find(ReceiverId) !=null;
            if (!receiveraccountfound)
            {
                ModelState.AddModelError(nameof(ReceiverId), "Please conduct a transfer to a valid account!");
            }
            if (ModelState.IsValid)
            {

                var response = accountService.Transfer(accountId, receiverId: ReceiverId, amount: Amount);
                if (response == IAccountService.ErrorCode.ok)
                    return RedirectToPage("./Details",new {Id=accountId});


                if (response== IAccountService.ErrorCode.AccountNotFound)
                {
                    ModelState.AddModelError(nameof(ReceiverId),"Account Not Found");
                }


            }




            return Page();


        }
    }
} 
