

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace BaseLibrary.Entities
{
    public class ItemRequest
    {
        [Required]
        [ReadOnly(true)]
        [Key]
        public int RequestId { get; private set; }
        [Required]
        public string? EmployeeName { get;  set; }
        public Item? Item { get; set; }
        [Required]
        public int ItemId { get; set; }
        [Required]
        public int Unit { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]
        public int Quantity { get; set; }
        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal UAH { get; set; }
        public string? Comment { get; set; }

        public int? Status { get; set; }



    }
}
