using System.ComponentModel.DataAnnotations;

namespace API_Metadata.Models_API
{
    public class BaseRequest
    {
        [Required]
        public required string RequestingSystem { get; set; }
    }
}
