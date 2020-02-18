using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System;

namespace EVS.Dotnet328.GarmentsShop
{
    public class GarmentsHandler
    {
        //============================ Product Section ====================================//
        public void AddProduct(Product product)
        {
            using (TheGarmentsContext context = new TheGarmentsContext())
            {
                context.Entry<SubCategory>(product.SubCategory).State = EntityState.Unchanged;
                context.Entry<Fabric>(product.Fabric).State = EntityState.Unchanged;

                foreach (var c in product.ColorsOffered)
                {
                    context.Entry<Color>(c).State = EntityState.Unchanged;
                }
                foreach (var s in product.SizesOffered)
                {
                    context.Entry<Size>(s).State = EntityState.Unchanged;
                }
                context.Products.Add(product);
                context.SaveChanges();
            }
        }

        public void DeleteProduct(int pid)
        {
            using (TheGarmentsContext context = new TheGarmentsContext())
            {
                Product result = (from p in context.Products
                                  .Include(p => p.SubCategory.Category.Department)
                                  .Include(p => p.Images)
                                  .Include(p => p.SizesOffered)
                                  .Include(p => p.ColorsOffered)
                                  .Include(p => p.Fabric)
                                  where p.Id == pid
                                  select p).FirstOrDefault();

                context.Products.Remove(result);
                context.SaveChanges();
            }
        }

        public List<Product> GetProducts()
        {
            using (TheGarmentsContext context = new TheGarmentsContext())
            {
                return (from p in context.Products
                        .Include(p => p.SubCategory.Category.Department)
                        .Include(p => p.Fabric)
                        .Include(p => p.ColorsOffered)
                        .Include(p => p.SizesOffered)
                        .Include(p => p.Images)
                        select p).ToList();
            }
        }

        public Product GetProduct(int id)
        {
            using (TheGarmentsContext context = new TheGarmentsContext())
            {
                //context.Configuration.LazyLoadingEnabled = true;
                Product result = (from p in context.Products
                                  .Include(p => p.SubCategory.Category.Department)
                                  .Include(p => p.Images)
                                  .Include(p => p.SizesOffered)
                                  .Include(p => p.ColorsOffered)
                                  .Include(p => p.Fabric)
                                  where p.Id == id
                                  select p).FirstOrDefault();
                return result;
            }

        }

        public List<Product> GetProductsByDepartment(Department department)
        {
            using (TheGarmentsContext context = new TheGarmentsContext())
            {
                return (from p in context.Products
                        .Include(p => p.SubCategory.Category.Department)
                        .Include(p => p.Fabric)
                        .Include(p => p.ColorsOffered)
                        .Include(p => p.SizesOffered)
                        .Include(p => p.Images)
                        where p.SubCategory.Category.Department.Id == department.Id
                        select p).ToList();
            }
        }

        //============================ Product Section END ====================================//


        //============================ Department Section ====================================//

        public void AddDepartment(Department department)
        {
            using (TheGarmentsContext context = new TheGarmentsContext())
            {
                context.Departments.Add(department);
                context.SaveChanges();
            }
        }

        public List<Department> GetDepartments()
        {
            using (TheGarmentsContext context = new TheGarmentsContext())
            {
                return (from d in context.Departments select d).ToList();
            }
        }

        public Department GetDepartment(int id)
        {
            using (TheGarmentsContext context = new TheGarmentsContext())
            {
                return (from d in context.Departments where d.Id == id select d).FirstOrDefault();
            }
        }

        public void UpdateDepartment(int idToSearch, Department department)
        {
            using (TheGarmentsContext context = new TheGarmentsContext())
            {
                Department temp = context.Departments.Find(idToSearch);
                if (!string.IsNullOrWhiteSpace(department.Name))
                {
                    temp.Name = department.Name;
                }
                if (!string.IsNullOrWhiteSpace(department.ImageUrl))
                {
                    temp.ImageUrl = department.ImageUrl;
                }
                context.SaveChanges();
            }
        }

        public void DeleteDepartment(Department department)
        {
            using (TheGarmentsContext context = new TheGarmentsContext())
            {
                Department found = context.Departments.Find(department.Id);
                context.Departments.Remove(found);
                context.SaveChanges();
            }
        }

        //============================ Department Section END ====================================//


        //============================ Category Section ====================================//

        public void AddCategory(Category category)
        {
            using (TheGarmentsContext context = new TheGarmentsContext())
            {
                context.Entry<Department>(category.Department).State = EntityState.Unchanged;
                context.Categories.Add(category);
                context.SaveChanges();
            }
        }

        public void UpdateCategory(int id, Category category)
        {
            using (TheGarmentsContext context = new TheGarmentsContext())
            {
                Category c = context.Categories.Find(id);
                if (!String.IsNullOrEmpty(category.Name))
                {
                    c.Name = category.Name;
                }
                c.DepartmentId = category.DepartmentId;
                context.SaveChanges();
            }
        }

        public void DeleteCategoryById(int id)
        {
            using (TheGarmentsContext context = new TheGarmentsContext())
            {
                Category c = context.Categories.Find(id);
                context.Categories.Remove(c);
                context.SaveChanges();
            }
        }

        public Category GetCategoryByID(int id)
        {
            using (TheGarmentsContext context = new TheGarmentsContext())
            {
                return (from c in context.Categories
                        .Include(x => x.Department)
                        where c.Id == id
                        select c).FirstOrDefault();
            }
        }

        public List<Category> GetCategoriesList()
        {
            using (TheGarmentsContext context = new TheGarmentsContext())
            {
                return (from c in context.Categories
                        select c).ToList();
            }
        }

        public List<Category> GetCategories(Department dept)
        {
            using (TheGarmentsContext context = new TheGarmentsContext())
            {
                return (from c in context.Categories
                        where c.Department.Id == dept.Id
                        select c).ToList();
            }
        }

        //============================ Category Section END ====================================//


        //============================ Sub Category Section ====================================//

        public void AddSubCategory(SubCategory subCategory)
        {
            using (TheGarmentsContext context = new TheGarmentsContext())
            {
                context.Entry<Category>(subCategory.Category).State = EntityState.Unchanged;
                context.SubCategories.Add(subCategory);
                context.SaveChanges();
            }
        }

        public void UpdateSubCategory(SubCategory subCategory)
        {
            using (TheGarmentsContext context = new TheGarmentsContext())
            {
                SubCategory category = context.SubCategories.Find(subCategory.Id);

                if (!String.IsNullOrEmpty(subCategory.Name))
                {
                    category.Name = subCategory.Name;
                }
                category.CategoryId = subCategory.CategoryId;
                context.SaveChanges();
            }
        }

        public void DeleteSubCategoryById(int id)
        {
            using (TheGarmentsContext context = new TheGarmentsContext())
            {
                SubCategory c = context.SubCategories.Find(id);
                context.SubCategories.Remove(c);
                context.SaveChanges();
            }
        }

        public SubCategory GetSubCategoryByID(int id)
        {
            using (TheGarmentsContext context = new TheGarmentsContext())
            {
                return (from c in context.SubCategories
                        where c.Id == id
                        select c).FirstOrDefault();
            }
        }

        public List<SubCategory> GetSubCategories()
        {
            using (TheGarmentsContext context = new TheGarmentsContext())
            {
                return (from sc in context.SubCategories
                        select sc).ToList();
            }
        }

        public List<SubCategory> GetSubCategoriesByCategory(Category category)
        {
            using (TheGarmentsContext context = new TheGarmentsContext())
            {
                return (from sc in context.SubCategories
                        where sc.Category.Id == category.Id
                        select sc).ToList();
            }
        }


        //============================ Sub Category Section END ====================================//


        //============================ Colors Section ====================================//

        public void addColor(string name)
        {
            using (TheGarmentsContext context = new TheGarmentsContext())
            {
                Color c = new Color { Name = name };

                context.Colors.Add(c);
                context.SaveChanges();
            }
        }

        public void updateColor(int id, string name)
        {
            using (TheGarmentsContext context = new TheGarmentsContext())
            {
                Color c = context.Colors.Find(id);
                if (!String.IsNullOrEmpty(name))
                {
                    c.Name = name;
                }
                context.Colors.Add(c);
                context.SaveChanges();
            }
        }

        public void DeleteColor(int id)
        {
            using (TheGarmentsContext context = new TheGarmentsContext())
            {
                Color c = context.Colors.Find(id);
                context.Colors.Remove(c);
                context.SaveChanges();
            }
        }

        public List<Color> GetColors()
        {
            using (TheGarmentsContext context = new TheGarmentsContext())
            {
                return (from c in context.Colors select c).ToList();
            }
        }

        //============================ Colors Section END ====================================//

        //============================ Sizes Section ====================================//

        public void addSize(Size size)
        {
            using (TheGarmentsContext context = new TheGarmentsContext())
            {
                context.Sizes.Add(size);
                context.SaveChanges();
            }
        }

        public void updateSize(int id, string name)
        {
            using (TheGarmentsContext context = new TheGarmentsContext())
            {
                Size s = context.Sizes.Find(id);
                if (!String.IsNullOrEmpty(name))
                {
                    s.Name = name;
                }
                context.Sizes.Add(s);
                context.SaveChanges();
            }
        }

        public void DeleteSize(int id)
        {
            using (TheGarmentsContext context = new TheGarmentsContext())
            {
                Size s = context.Sizes.Find(id);
                context.Sizes.Remove(s);
                context.SaveChanges();
            }
        }

        public List<Size> GetSizes()
        {
            using (TheGarmentsContext context = new TheGarmentsContext())
            {
                return (from s in context.Sizes select s).ToList();
            }
        }

        //============================ Sizes Section END ====================================//

        //============================ Fabrics Section ====================================//

        public void addFabric(string name)
        {
            using (TheGarmentsContext context = new TheGarmentsContext())
            {
                Fabric f = new Fabric { Name = name };

                context.Fabrics.Add(f);
                context.SaveChanges();
            }
        }

        public void updateFabric(int id, string name)
        {
            using (TheGarmentsContext context = new TheGarmentsContext())
            {
                Fabric f = context.Fabrics.Find(id);
                if (!String.IsNullOrEmpty(name))
                {
                    f.Name = name;
                }
                context.Fabrics.Add(f);
                context.SaveChanges();
            }
        }

        public void DeleteFabric(int id)
        {
            using (TheGarmentsContext context = new TheGarmentsContext())
            {
                Fabric f = context.Fabrics.Find(id);
                context.Fabrics.Remove(f);
                context.SaveChanges();
            }
        }

        public List<Fabric> GetFabrics()
        {
            using (TheGarmentsContext context = new TheGarmentsContext())
            {
                return (from f in context.Fabrics select f).ToList();
            }
        }

        //============================ Fabrics Section END ====================================//

    }
}
