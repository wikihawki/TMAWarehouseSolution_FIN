

using System.ComponentModel.DataAnnotations;

namespace BaseLibrary.Entities
{
    public class OtherEntityBase
    {

        public int Id { get; set; }
        [Required]
        public string CiviliId { get; set; } = string.Empty;
        [Required]
        public string FileNumber { get; set; } = string.Empty;

        public string? Other {  get; set; } 
    }
}
