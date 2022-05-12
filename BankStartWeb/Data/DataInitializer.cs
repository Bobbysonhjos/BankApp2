using Bogus;
using Bogus.DataSets;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace BankStartWeb.Data;

public class DataInitializer
{
    private readonly ApplicationDbContext _dbContext;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;



    public DataInitializer(ApplicationDbContext dbContext,
        UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _dbContext = dbContext;
        _userManager = userManager;
        _roleManager = roleManager;
    }


    public async Task SeedData()
    {
        _dbContext.Database.Migrate();
        SeedCustomers();
        await SeedUsers();
    }
    private async Task SeedUsers()
    {


        if (!_dbContext.Roles.Any())
        {
            var adminRole = new IdentityRole{Name="Admin",NormalizedName = "Admin"};
            var cashierRole = new IdentityRole { Name = "Cashier", NormalizedName = "Cashier" };
            _dbContext.Roles.Add(adminRole);
            _dbContext.Roles.Add(cashierRole);
            _dbContext.SaveChanges();

        }






        var adminUser = new IdentityUser
        {
            Email = "stefan.holmberg@systementor.se",
            NormalizedEmail = "STEFAN.HOLMBERG@SYSTEMENTOR.SE",
            UserName = "stefan.holmberg@systementor.se",
            NormalizedUserName = "STEFAN.HOLMBERG@SYSTEMENTOR.SE",
            EmailConfirmed = true

        };

        var cashierUser = new IdentityUser
        {
            Email = "stefan.holmberg@customer.banken.se",
            NormalizedEmail = "STEFAN.HOLMBERG@CUSTOMER.BANKEN.SE",
            UserName = "stefan.holmberg@customer.banken.se",
            NormalizedUserName = "STEFAN.HOLMBERG@CUSTOMER.BANKEN.SE",
            EmailConfirmed = true
        };

        if (!_dbContext.Users.Any())
        {
             _userManager.CreateAsync(adminUser, "Hejsan123#").Wait();

             _userManager.AddToRoleAsync(adminUser, "Admin").Wait();

             _userManager.CreateAsync(cashierUser, "Hejsan123#").Wait();

             _userManager.AddToRoleAsync(cashierUser, "Cashier").Wait();

        }




    }


    private void SeedCustomers()
    {
        while (_dbContext.Customers.Count() < 5000)
        {
            var number = 5000 - _dbContext.Customers.Count();
            var customers = new List<Customer>();
            if ( number > 0)
            {
                for (int i = 0; i < number; i++)
                {
                    customers.Add(GenerateCustomer());
                }
            }
           
            _dbContext.Customers.AddRange(customers);
            _dbContext.SaveChanges();
        }

    }

    private static Random random = new Random();
    private Customer GenerateCustomer()
    {
        // f.Date.Between(new DateTime(1999,1,1), new DateTime(1940,1,1))
        var n = random.Next(0, 100);
        Customer person = null;
        if (n < 20)
        {
            var testUser = new Faker<Customer>("nb_NO")
                .StrictMode(false)
                .RuleFor(e => e.Id, f => 0)
                .RuleFor(e => e.City, (f, u) => f.Address.City())
                .RuleFor(e => e.Country, (f, u) => "Norge")
                .RuleFor(e => e.CountryCode, (f, u) => "NO")
                .RuleFor(e => e.Birthday, (f, u) => f.Person.DateOfBirth)
                .RuleFor(e => e.EmailAddress, (f, u) => f.Internet.Email())
                .RuleFor(e => e.Givenname, (f, u) => f.Person.FirstName)
                .RuleFor(e => e.Surname, (f, u) => f.Person.LastName)
                .RuleFor(e => e.NationalId, (f, u) => f.Person.DateOfBirth.ToString("yyyyMMdd") + "-3333")
                .RuleFor(e => e.Streetaddress, (f, u) => f.Address.StreetAddress())
                .RuleFor(e => e.Telephone, (f, u) => f.Person.Phone)
                .RuleFor(e => e.Zipcode, (f, u) => f.Address.ZipCode())
                .RuleFor(e => e.TelephoneCountryCode, (f, u) => 47);
            person = testUser.Generate(1).First();
        }

        else if (n < 80)
        {
            var testUser = new Faker<Customer>("sv")
                .StrictMode(false)
                .RuleFor(e => e.Id, f => 0)
                .RuleFor(e => e.City, (f, u) => f.Address.City())
                .RuleFor(e => e.Country, (f, u) => "Sverige")
                .RuleFor(e => e.CountryCode, (f, u) => "SE")
                .RuleFor(e => e.Birthday, (f, u) => f.Person.DateOfBirth)
                .RuleFor(e => e.EmailAddress, (f, u) => f.Internet.Email())
                .RuleFor(e => e.Givenname, (f, u) => f.Person.FirstName)
                .RuleFor(e => e.Surname, (f, u) => f.Person.LastName)
                .RuleFor(e => e.NationalId, (f, u) => f.Person.DateOfBirth.ToString("yyyyMMdd") + "-1111")
                .RuleFor(e => e.Streetaddress, (f, u) => f.Address.StreetAddress())
                .RuleFor(e => e.Telephone, (f, u) => f.Person.Phone)
                .RuleFor(e => e.Zipcode, (f, u) => f.Address.ZipCode())
                .RuleFor(e => e.TelephoneCountryCode, (f, u) => 46);
            person = testUser.Generate(1).First();
        }


        else 
        {
            var testUser = new Faker<Customer>("fi")
                .StrictMode(false)
                .RuleFor(e => e.Id, f => 0)
                .RuleFor(e => e.City, (f, u) => f.Address.City())
                .RuleFor(e => e.Country, (f, u) => "Finland")
                .RuleFor(e => e.CountryCode, (f, u) => "FI")
                .RuleFor(e => e.Birthday, (f, u) => f.Person.DateOfBirth)
                .RuleFor(e => e.EmailAddress, (f, u) => f.Internet.Email())
                .RuleFor(e => e.Givenname, (f, u) => f.Person.FirstName)
                .RuleFor(e => e.Surname, (f, u) => f.Person.LastName)
                .RuleFor(e => e.NationalId, (f, u) => f.Person.DateOfBirth.ToString("yyyyMMdd") + "-2222")
                .RuleFor(e => e.Streetaddress, (f, u) => f.Address.StreetAddress())
                .RuleFor(e => e.Telephone, (f, u) => f.Person.Phone)
                .RuleFor(e => e.Zipcode, (f, u) => f.Address.ZipCode())
                .RuleFor(e => e.TelephoneCountryCode, (f, u) => 48);
            person = testUser.Generate(1).First();
        }

        for (int i = 0; i < random.Next(1, 5);i++)
        {
            person.Accounts.Add(GenerateAccount());
        }

        


        return person;
    }


    private Account GenerateAccount()
    {
        string[] accountType = {"Personal", "Checking", "Savings"};
        var testUser = new Faker<Account>()
            .StrictMode(false)
            .RuleFor(e => e.Id, f => 0)
            .RuleFor(e => e.AccountType, (f, u) => f.PickRandom(accountType))
            .RuleFor(e => e.Balance, (f, u) => 0);

        var account = testUser.Generate(1).First();
        var start = DateTime.Now.AddDays(-random.Next(1000, 10000));
        account.Created = start;
        account.Balance = 0;
        var transactions = random.Next(1, 30);
        for (int i = 0; i < transactions; i++)
        {
            var tran = new Transaction();
            tran.Amount = random.NextInt64(1, 50) * 100;
            start = start.AddDays(random.NextInt64(50, 600));
            if (start > DateTime.Now)
                break;
            tran.Date = start;
            account.Transactions.Add(tran);
            if (account.Balance - tran.Amount < 0)
                tran.Type = "Debit";
            else
            {
                if (random.NextInt64(0, 100) > 70)
                    tran.Type = "Debit";
                else
                    tran.Type = "Credit";
            }

            var r = random.Next(0, 100);
            if (tran.Type == "Debit")
            {
                account.Balance = account.Balance + tran.Amount;
                if (r < 20)
                    tran.Operation = "Deposit cash";
                else if(r < 66)
                    tran.Operation = "Salary";
                else
                    tran.Operation = "Transfer";
            }
            else
            {
                account.Balance = account.Balance - tran.Amount;
                if (r < 40)
                    tran.Operation = "ATM withdrawal";
                else if (r < 66)
                    tran.Operation = "Payment";
                else
                    tran.Operation = "Transfer";
            }

            tran.NewBalance = account.Balance;
        }
        return account;
    }
}