using EVS.Dotnet328.GarmentsShop.Models.Categories;
using EVS.Dotnet328.GarmentsShop.Models.Users;
using EVS.Dotnet328.UsersMgt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EVS.Dotnet328.GarmentsShop.Models
{
    public static class ModelHelper
    {

        public static List<SelectListItem> ToSelectItemList(this IEnumerable<IListable> values)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            foreach (var v in values)
            {
                items.Add(new SelectListItem { Text = v.Name, Value = Convert.ToString(v.Id) });
            }
            return items;
        }
        
        public static Country ToEntity(this CountryModel model)
        {
            Country e = new Country();
            e.Id = model.Id;
            e.Name = model.Name;
            e.Code = model.Code;
            return e;
        }


        public static List<Country> ToEntityList(this IEnumerable<CountryModel> modelList)
        {
            List<Country> countries = new List<Country>();
            foreach (var m in modelList)
            {
                countries.Add(m.ToEntity());
            }
            countries.TrimExcess();
            return countries;
        }


        public static CountryModel ToModel(this Country entity)
        {
            CountryModel m = new CountryModel();
            m.Id = entity.Id;
            m.Name = entity.Name;
            m.Code = entity.Code;
            return m;
        }

        
        public static List<CountryModel> ToModelList(this IEnumerable<Country> entityList)
        {
            List<CountryModel> countries = new List<CountryModel>();
            foreach (var e in entityList)
            {
                countries.Add(e.ToModel());
            }
            countries.TrimExcess();
            return countries;
        }

        public static List<SummaryModel> ToSummaryModelList(this IEnumerable<Product> products)
        {
            List<SummaryModel> modelList = new List<SummaryModel>();
            foreach (var p in products)
            {
                SummaryModel m = new SummaryModel { Id = p.Id, Name = p.Name, Price=p.Price };
                if (p.Images.Count > 0)
                {
                    m.MainImageUrl = p.Images.ToList()[0].Url;
                    m.AlternateImageUrl = (p.Images.Count > 1) ? p.Images.ToList()[1].Url : "/images/products/nophoto.png";
                }   
                else
                {
                    m.MainImageUrl = "/images/products/nophoto.png";
                }
                modelList.Add(m);
            }
            return modelList;
        }

        public static SummaryModel ToSummaryModel(this Product product)
        {
            SummaryModel pm = new SummaryModel { Id = product.Id, Name = product.Name, Price = product.Price };
            if (product.Images.Count > 0)
            {
                pm.MainImageUrl = product.Images.ToList()[0].Url;
                pm.AlternateImageUrl = (product.Images.Count > 1) ? product.Images.ToList()[1].Url : "/images/products/nophoto.png";
            }
            else
            {
                pm.MainImageUrl = "/images/products/nophoto.png";
            }
            return pm;
        }

        public static List<CategoryModel> ToCategoryModel(this IEnumerable<Category> categories)
        {
            List<CategoryModel> cm = new List<CategoryModel>();

            foreach(var c in categories)
            {
                CategoryModel m = new CategoryModel { Id=c.Id, Name=c.Name, DepartmentId=c.DepartmentId};

                cm.Add(m);
            }

            return cm;
        }

        public static Category ToCategoryEntity(this CategoryModel categoryModel)
        {
            Category c = new Category {
                Id =categoryModel.Id,
                Name = categoryModel.Name,
                DepartmentId= categoryModel.DepartmentId,
                Department =new GarmentsHandler().GetDepartment(categoryModel.DepartmentId)
            };
            return c;
        }

        public static CategoryModel ToCatModel(this Category category)
        {
            CategoryModel c = new CategoryModel();
            c.Id = category.Id;
            c.Name = category.Name;
            c.DepartmentId = category.Department.Id;
            return c;
        }

        public static SubCategoryModel ToSubCategoryModel(this SubCategory subCategory)
        {
            SubCategoryModel model = new SubCategoryModel();
            model.Id = subCategory.Id;
            model.Name = subCategory.Name;
            model.CategoryId = subCategory.CategoryId;

            return model;
        }

        public static List<SubCategoryModel> ToSubCategoryModelList(this IEnumerable<SubCategory> subCategory)
        {
            List<SubCategoryModel> model = new List<SubCategoryModel>();

            foreach(var m in subCategory)
            {
                model.Add(new SubCategoryModel { Id = m.Id, Name = m.Name, CategoryId = m.CategoryId });
            }

            return model;
        }

        public static SubCategory ToSubCategoryEntity(this SubCategoryModel model)
        {
            return new SubCategory
            {
                Id = model.Id,
                Name = model.Name,
                CategoryId = model.CategoryId,
                Category = new GarmentsHandler().GetCategoryByID(model.CategoryId)
            };

        }

        public static List<SubCategory> ToSubCategoryEntityList(this IEnumerable<SubCategoryModel> model)
        {
            List<SubCategory> entity = new List<SubCategory>();

            foreach (var m in model)
            {
                entity.Add(new SubCategory {
                    Id = m.Id,
                    CategoryId = m.CategoryId,
                    Category = new GarmentsHandler().GetCategoryByID(m.CategoryId)
                });
            }

            return entity;
        }


        public static User ToLoginEntity(this LoginModel model)
        {
            User m= new User();
            m.LoginId = model.LoginId;
            m.Password = model.Password;

            return m;
        }

        public static List<DepartmentModel> ToDepartmentModelList(this IEnumerable<Department> department)
        {
            List<DepartmentModel> modal = new List<DepartmentModel>();
            
            foreach(var d in department)
            {
                modal.Add(new DepartmentModel { Id = d.Id, Name = d.Name });
            }

            return modal;

        }

        public static List<Department> ToDepartmentEntityList(this IEnumerable<DepartmentModel> department)
        {
            List<Department> modalEntity = new List<Department>();

            foreach (var d in department)
            {
                modalEntity.Add(new Department { Id = d.Id, Name = d.Name });
            }

            return modalEntity;

        }

        public static DepartmentModel ToDepartmentModel (this Department department)
        {
            DepartmentModel modal = new DepartmentModel();
            modal.Id = department.Id;
            modal.Name = department.Name;

            return modal;
        }

        public static Department ToDepartmentEntity(this DepartmentModel department)
        {
            Department modalEntity = new Department();
            modalEntity.Id = department.Id;
            modalEntity.Name = department.Name;

            return modalEntity;
        }

        public static ProvinceModal ToProvinceModel(this Province p)
        {
            ProvinceModal model = new ProvinceModal { Id = p.Id, Name = p.Name, Country_Id = p.CountryId };
            
            return model;
        }

        public static List<ProvinceModal> ToProvinceModelList(this IEnumerable<Province> province)
        {
            List<ProvinceModal> model = new List<ProvinceModal>();
            foreach(var p in province)
            {
                model.Add(new ProvinceModal { Id = p.Id, Name = p.Name, Country_Id = p.CountryId });
            }

            return model;
        }

        public static Province ToProvinceEntity(this ProvinceModal p)
        {
            Province entity = new Province { Id = p.Id, Name = p.Name, CountryId = p.Country_Id, Country=new LocationsHandler().GetCountry(p.Country_Id) };

            return entity;
        }

        public static List<Province> ToProvinceEntitylList(this IEnumerable<ProvinceModal> province)
        {
            List<Province> entity = new List<Province>();
            foreach (var p in province)
            {
                entity.Add(new Province { Id = p.Id, Name = p.Name, CountryId = p.Country_Id , Country = new LocationsHandler().GetCountry(p.Country_Id) });
            }

            return entity;
        }

        public static City ToCityEntity(this CityModel cityModel)
        {
            City entity = new City {
                Id = cityModel.Id,
                Name = cityModel.Name,
                ProvinceId = cityModel.Province_Id,
                Province = new LocationsHandler().GetProvinceById(cityModel.Province_Id)
            };

            return entity;
        }

        public static List<City> ToCityEntityList(this IEnumerable<CityModel> city)
        {
            List<City> entity = new List<City>();
            foreach (var c in city)
            {
                entity.Add(new City {
                    Id = c.Id,
                    Name = c.Name,
                    ProvinceId = c.Province_Id,
                    Province = new LocationsHandler().GetProvinceById(c.Province_Id)
                });
            }

            return entity;
        }

        public static CityModel ToCityModel(this City city)
        {
            CityModel model = new CityModel { Id = city.Id, Name = city.Name, Province_Id = city.ProvinceId };

            return model;
        }

        public static List<CityModel> ToCityModelList(this IEnumerable<City> city)
        {
            List<CityModel> model = new List<CityModel>();
            foreach(var c in city)
            {
                model.Add(new CityModel { Id = c.Id, Name = c.Name, Province_Id = c.ProvinceId });
            }

            return model;
        }

    }
}