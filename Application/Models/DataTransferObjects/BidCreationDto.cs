using System.ComponentModel.DataAnnotations;

namespace Application.Models.DataTransferObjects
{
    public class BidCreationDto
    {
                [Required]
                public string OwnerId { get; set; }
                public string FreelancerId { get; set; }
    }
}