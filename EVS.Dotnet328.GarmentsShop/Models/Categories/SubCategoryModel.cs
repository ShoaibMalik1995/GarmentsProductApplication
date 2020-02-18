using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EVS.Dotnet328.GarmentsShop.Models.Categories
{
    public class SubCategoryModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int CategoryId { get; set; }
    }
}