using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BaseLibrary.DTOs
{
    public class ManageItemRequest
    {
        [Required]
        public int RequestId { get; set; }
        [Required]
        public int ItemId { get; set; }
        [Required]
        public string? EmployeeName { get; set; }
        [Required]
        public string? ItemName { get; set; }
        [Required]
        public string?  Unit { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]
        public int Quantity { get; set; }
        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal UAH { get; set; }
        public string? Comment { get; set; }
        public string? Status { get; set; }

    }
}
