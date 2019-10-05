using System.ComponentModel.DataAnnotations;

namespace Application.Models.DataTransferObjects
{
    public class BiddingCreationDto
    {
                [Required]
                public string OwnerId { get; set; }
                public string FreelancerId { get; set; }
        
                [Required]
                public BiddingContentDto BiddingContent { get; set; }
    }
}