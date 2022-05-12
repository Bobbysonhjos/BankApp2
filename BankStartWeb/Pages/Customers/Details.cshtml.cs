using AutoMapper;
using BankStartWeb.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BankStartWeb.Pages.Customers
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

        public CustomerDetailViewModel Customer { get; private set; }

       
        public class CustomerDetailViewModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Country { get; set; }
            public string City { get; set; }
            public string Streetaddress { get; set; }
            public string Zipcode { get; set; }
            public string EmailAddress { get; set; }
            public string Telephone { get; set; }
            public decimal Balance { get; set; }
            public List<CustomerAccountViewModel> Accounts { get; set; } = new List<CustomerAccountViewModel>();
        }
        


        public class CustomerAccountViewModel
        {
            public int Id { get; set; }
            public string AccountType { get; set; }

            public DateTime Created { get; set; }
            public decimal Balance { get; set; }
        }
      



        public void OnGet(int id)
        {
            Customer = mapper.Map<CustomerDetailViewModel>(context.Customers.Include(x => x.Accounts).FirstOrDefault(x => x.Id == id));
        }


    }

}
