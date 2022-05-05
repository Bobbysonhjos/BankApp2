
using AutoMapper;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BankStartWeb.Pages.Customers
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public List<CustomerViewModel> Customers { get; private set; }


        public string SortOrder { get; set; }
        public string SortCol { get; set; }
        public int PageNo { get; set; }

        public int TotalPageCount { get; set; }

        public string SearchWord { get; set; }
        

        public IndexModel(ApplicationDbContext context, IMapper mapper)

        {
            this.context = context;
            this.mapper = mapper;
        }



        public class CustomerViewModel
        {
            public int Id { get; set; }

            public string Name { get; set; }
            public string Country { get; set; }
            public string City { get; set; }
            public decimal Balance { get; set; }
            public string Address { get; set; }
            public string NationalId { get; set; }




        }
        public void OnGet( string searchWord , string col= "City", string order = "asc", int pageno = 1)
        {
            PageNo = pageno;
            SearchWord = searchWord;
            SortCol = col;
            SortOrder = order;


            var o = context.Customers.Include(c => c.Accounts).AsQueryable();

            if (!string.IsNullOrEmpty(SearchWord))
                o = o.Where(c => (c.Givenname + " " + c.Surname).ToLower().Contains(SearchWord.ToLower())
                                     || c.City.ToLower().Contains(SearchWord.ToLower())
                            );

            



            var limit = 15;
            PageNo = pageno;
            
            Customers = mapper.Map<List<CustomerViewModel>>(o.Skip((pageno - 1) * limit).Take(limit).ToList());
            TotalPageCount = (int)Math.Ceiling((double)context.Customers.Count() / limit);







        }

    }
}