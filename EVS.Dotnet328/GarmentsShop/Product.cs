using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVS.Dotnet328.GarmentsShop
{
    public class Product : IListable
    {

        public Product()
        {
            Images = new List<ProductImage>();
            ColorsOffered = new List<Color>();
            SizesOffered = new List<Size>();
        }
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "varchar")]
        [MaxLength(100)]
        public string Name { get; set; }

        public float Price { get; set; }


        [Column(TypeName = "varchar")]
        [MaxLength(100)]
        public string Description { get; set; }

        public Nullable<DateTime> LaunchingDate { get; set; }

        [Required]
        public virtual SubCategory SubCategory { get; set; }

        [Required]
        public virtual ICollection<ProductImage> Images { get; set; }

        public virtual ICollection<Color> ColorsOffered { get; set; }

        public virtual ICollection<Size> SizesOffered { get; set; }

        public virtual Fabric Fabric { get; set; }


    }
}
