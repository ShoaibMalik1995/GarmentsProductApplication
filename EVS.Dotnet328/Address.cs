using EVS.Dotnet328.UsersMgt;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EVS.Dotnet328
{
    public class Address
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [MaxLength(255)]
        [Column(TypeName = "varchar")]
        public string StreetAddress { get; set; }
        
        [Column("City_Id")]
        public int CityId { get; set; }
        [Required]
        public virtual City City { get; set; }

    }
}
