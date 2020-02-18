using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EVS.Dotnet328.GarmentsShop.Models
{
    public class SummaryModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public float Price { get; set; }

        public string MainImageUrl { get; set; }

        public string AlternateImageUrl { get; set; }
    }
}