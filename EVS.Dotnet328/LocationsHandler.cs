using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVS.Dotnet328
{
    public class LocationsHandler
    {
        //================================ Country Section ==========================//
        public List<Country> GetCountries()
        {
            using (TheGarmentsContext context = new TheGarmentsContext())
            {
                return (from c in context.Countries
                        select c).ToList();
            }
        }

        public Country GetCountry(int id)
        {
            using (TheGarmentsContext context = new TheGarmentsContext())
            {
                return (from c in context.Countries
                        where c.Id == id
                        select c).First();
            }
        }

        public void AddCountry(Country country)
        {
            using (TheGarmentsContext context = new TheGarmentsContext())
            {
                context.Countries.Add(country);
                context.SaveChanges();
            }
        }

        public void UpdateCountry(int idToSearch, Country country)
        {
            using (TheGarmentsContext context = new TheGarmentsContext())
            {
                Country found = context.Countries.Find(idToSearch);
                if (!string.IsNullOrWhiteSpace(country.Name)) found.Name = country.Name;
                if (country.Code != null || country.Code > 0) found.Code = country.Code;
                context.SaveChanges();
            }
        }

        public void DeleteCountry(int idToSearch)
        {
            using (TheGarmentsContext context = new TheGarmentsContext())
            {
                Country found = context.Countries.Find(idToSearch);
                context.Countries.Remove(found);
                context.SaveChanges();
            }
        }

        //================================ Country Section END ==========================//

        //================================ Province Section ==========================//

        public List<Province> GetProvince()
        {
            using (TheGarmentsContext context = new TheGarmentsContext())
            {
                return (from c in context.Provinces
                        select c).ToList();
            }
        }

        public Province GetProvinceById(int id)
        {
            using (TheGarmentsContext context = new TheGarmentsContext())
            {
                return context.Provinces.Find(id);
            }
        }

        public List<Province> GetProvinceBycountry(int cid)
        {
            using (TheGarmentsContext context = new TheGarmentsContext())
            {
                return (from p in context.Provinces
                        where p.CountryId == cid
                        select p).ToList();
            }
        }

        public void AddProvince(Province province)
        {
            using (TheGarmentsContext context = new TheGarmentsContext())
            {
                context.Entry<Country>(province.Country).State = EntityState.Unchanged;
                context.Provinces.Add(province);
                context.SaveChanges();
            }
        }

        public void UpdateProvince(int pid, Province province)
        {
            TheGarmentsContext context = new TheGarmentsContext();
            Province p = (from c in context.Provinces where c.Id == pid select c).FirstOrDefault();
            if (!String.IsNullOrEmpty(province.Name))
            {
                p.Name = province.Name;
            }
            p.CountryId = province.CountryId;
            context.Entry<Country>(province.Country).State = EntityState.Unchanged;
            context.SaveChanges();
        }

        public void DeleteProvinceByID(int pid)
        {
            TheGarmentsContext context = new TheGarmentsContext();

            Province p = context.Provinces.Find(pid);
            context.Provinces.Remove(p);
            context.SaveChanges();

        }

        //================================ Province Section END ==========================//

        //================================ City Section ==========================//

        public List<City> GetCities()
        {
            using (TheGarmentsContext context = new TheGarmentsContext())
            {
                return (from c in context.Cities
                        .Include(c => c.Province.Country)
                        select c).ToList();
            }
        }

        public List<City> GetCitiesByProvinceId(int Pid)
        {
            using (TheGarmentsContext context = new TheGarmentsContext())
            {
                return (from c in context.Cities
                        where c.ProvinceId==Pid
                        select c).ToList();
            }
        }

        public City GetCityByID(int id)
        {
            using (TheGarmentsContext context = new TheGarmentsContext())
            {
                return (from c in context.Cities
                        .Include(c => c.Province.Country)
                        select c).FirstOrDefault();
            }
        }

        public void AddCity(City city)
        {
            using (TheGarmentsContext context = new TheGarmentsContext())
            {
                context.Entry<Province>(city.Province).State = EntityState.Unchanged;
                context.Cities.Add(city);
                context.SaveChanges();
            }
        }

        public void UpdateCity(int id,City city)
        {
            using (TheGarmentsContext context = new TheGarmentsContext())
            {
                context.Entry<Province>(city.Province).State = EntityState.Unchanged;
                City found = context.Cities.Find(id);

                if(! String.IsNullOrEmpty(city.Name))
                {
                    found.Name = city.Name;
                }
                found.ProvinceId = city.ProvinceId;
                context.SaveChanges();
            }
        }

        public void DeleteCity(int id)
        {
            using (TheGarmentsContext context = new TheGarmentsContext())
            {
                City found = context.Cities.Find(id);
                context.Entry<Province>(found.Province).State = EntityState.Unchanged;

                context.Cities.Remove(found);
                context.SaveChanges();
            }
        }

        //================================ City Section END ==========================//

    }
}
