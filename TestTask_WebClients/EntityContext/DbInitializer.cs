using System;
using System.Linq;
using TestTask_WebClients.Models;

namespace TestTask_WebClients.EntityContext
{
    public class DbInitializer
    {

        public static void Initialize(ContextDb context)
        {
            context.Database.EnsureCreated();

            // Look for any Customers.
            if (context.Customers.Any())
            {
                return;   // DB has been seeded
            }

            var customers = new Customer[]
            {
                new Customer 
                { 
                    FirstName = "Chester",
                    LastName = "Vallins",
                    Emails = "Chester_Vallins880@deons.tech",
                    Gender = false,
                    DayOfBirth = DateTime.Parse("2002-05-13")
                }, 
                new Customer 
                { 
                    FirstName = "Barry",
                    LastName = "Davies",
                    Emails = "Barry_Davies6697@typill.biz",
                    Gender = false,
                    DayOfBirth = DateTime.Parse("1986-01-20")
                }, 
                new Customer 
                { 
                    FirstName = "Gabriel",
                    LastName = "Fox",
                    Emails = "Gabriel_Fox8810@dionrab.com",
                    Gender = false,
                    DayOfBirth = DateTime.Parse("2000-09-17")
                }, 
                new Customer 
                { 
                    FirstName = "Colleen",
                    LastName = "Willson",
                    Emails = "Colleen_Willson8441@elnee.tech",
                    Gender = false,
                    DayOfBirth = DateTime.Parse("1990-02-25")
                }, 
                new Customer 
                { 
                    FirstName = "Makenzie",
                    LastName = "Ellison",
                    Emails = "Makenzie_Ellison7540@famism.biz",
                    Gender = false,
                    DayOfBirth = DateTime.Parse("2001-05-04")
                }, 
                new Customer 
                { 
                    FirstName = "Jacob",
                    LastName = "Chapman",
                    Emails = "Jacob_Chapman7333@liret.org",
                    Gender = false,
                    DayOfBirth = DateTime.Parse("1999-12-28")
                }, 
                new Customer 
                { 
                    FirstName = "Oliver",
                    LastName = "Allington",
                    Emails = "Oliver_Allington7637@nanoff.biz",
                    Gender = false,
                    DayOfBirth = DateTime.Parse("2000-02-23")
                }, 
                new Customer 
                { 
                    FirstName = "Maia",
                    LastName = "Whinter",
                    Emails = "Maia_Whinter1199@cispeto.com",
                    Gender = true,
                    DayOfBirth = DateTime.Parse("1978-06-09")
                }, 
                new Customer 
                { 
                    FirstName = "Phillip",
                    LastName = "Clark",
                    Emails = "Phillip_Clark2064@corti.com",
                    Gender = false,
                    DayOfBirth = DateTime.Parse("2002-05-01")
                }, 
                new Customer 
                { 
                    FirstName = "Julia",
                    LastName = "Weels",
                    Emails = "Julia_Fox7939@jiman.org",
                    Gender = true,
                    DayOfBirth = DateTime.Parse("1978-11-12")
                }               
            };
            foreach (Customer s in customers)
            {
                context.Customers.Add(s);
            }
            context.SaveChanges();
        }
    }
}
