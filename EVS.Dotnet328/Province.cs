using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVS.Dotnet328
{
    public class Province : IListable
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        [Column(TypeName = "varchar")]
        public string Name { get; set; }

        [Column("Country_Id")]
        public int CountryId { get; set; }
        [Required]
        public virtual Country Country { get; set; }

    }
}
