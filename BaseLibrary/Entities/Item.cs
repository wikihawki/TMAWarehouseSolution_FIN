
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;


namespace BaseLibrary.Entities
{


    public class Item
    {
        [Required]
        [ReadOnly(true)]
        [Key]
        public int ItemId { get; private set; }
        [Required]
        public int Unit { get; set; }
        [Required]
        public int ItemGroup { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]
        public int Quantity { get; set; }
        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal UAH { get; set; }
        [Required]
        [MaxLength(50)]
        public string? Status { get; set; }
        [MaxLength(50)]
        public string? StorageLocation { get; set; }

        public string? ContactPerson { get; set; }
        [JsonIgnore]
        public List<ItemRequest>? ItemRequests { get; set; }


    }
}
