using System;
using System.Collections.Generic;
using System.Linq;
using EVS.Dotnet328;

namespace EVS.Dotnet328.GarmentsShop.ConsoleTesting
{
    class Program
    {
        static void Main(string[] args)
        {

            //LocationsHandler temp = new LocationsHandler();

            //List<Country> countries = temp.GetCountries();
            //Country c=  temp.GetCountry(1);
            //temp.AddCountry(c);



            using (TheGarmentsContext context = new TheGarmentsContext())
            {
                //context.Countries.Add(new Country { Name = "Pakistan" });
                //context.Countries.Add(new Country { Name = "India" });
                //context.SaveChanges();

                //IEnumerable<Country> result = from c in context.Countries
                //                              where c.Name.Equals("Pakistan")
                //                              select c;

                //var result = (from c in context.Countries
                //              where c.Id == 1
                //              select c).First();


                var result = (from c in context.Countries
                              where c.Id == 1
                              select c).ToList();

                //foreach (var r in result)
                //{
                //    Console.WriteLine(r.Name);
                //}
                Console.ReadKey();
            }

        }
    }
}
