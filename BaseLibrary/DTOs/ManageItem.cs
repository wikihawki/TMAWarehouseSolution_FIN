using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseLibrary.DTOs
{
    public class ManageItem
    {
        [Required]
        public int ItemId { get; set; }
        [Required]
        public string? ItemGroup { get; set; }
        [Required]
        public string? Unit { get; set; }

        [Required, Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]
        public int Quantity { get; set; }
        [Required, Column(TypeName = "decimal(10,2)")]
        public decimal UAH { get; set; }
        [Required, MaxLength(50)]
        public string? Status { get; set; }
        [MaxLength(50)]
        public string? StorageLocation { get; set; }

        public string? ContactPerson { get; set; }


    }
}
