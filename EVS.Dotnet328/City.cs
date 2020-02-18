using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EVS.Dotnet328
{
    public class City : IListable
    {
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "varchar")]
        [MaxLength(50)]
        public string Name { get; set; }

        [Column("Province_Id")]
        public int ProvinceId { get; set; }
        [Required]
        public virtual Province Province { get; set; }
    }
}
