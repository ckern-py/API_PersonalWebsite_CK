using System.ComponentModel.DataAnnotations;

namespace API_Metadata.Models_API
{
    public class InsertPageVisitRequest : BaseRequest
    {
        [Required]
        public required string PageName { get; set; }
    }
}
