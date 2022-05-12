using AutoMapper;

namespace BankStartWeb.Profiles
{
    public class MapperProfile: Profile
    {
        public MapperProfile()
        {
            CreateMap<Customer,Pages.Customers.DetailsModel.CustomerDetailViewModel>()
                .ForMember(x=>x.Name,opt=>opt.MapFrom(x=>$"{x.Givenname} {x.Surname}"))
                .ForMember(x=>x.Balance,opt=>opt.MapFrom(x=>x.Accounts.Sum(x=>x.Balance))).ReverseMap();
            CreateMap<Account,Pages.Customers.DetailsModel.CustomerAccountViewModel>().ReverseMap();



            CreateMap<Customer, Pages.Customers.IndexModel.CustomerViewModel>()
                 .ForMember(x => x.Name, opt => opt.MapFrom(x => $"{x.Givenname} {x.Surname}"))
                // .ForMember(x => x.Balance, opt => opt.MapFrom(x => x.Accounts.Sum(a => a.Balance)))
                 .ForMember(x=>x.Address, opt=>opt.MapFrom(x=>x.Streetaddress))
                    .ReverseMap();



            CreateMap<Account, Pages.Customers.Accounts.DetailsModel.AccountDetailViewModel>()
                .ReverseMap();
            CreateMap<Transaction, Pages.Customers.Accounts.DetailsModel.TransactionViewModel>()
                .ReverseMap();


        }


    }
}



