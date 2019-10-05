using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Application.Models.DataTransferObjects
{
    public class BiddingUpdateDto
    {
        [Required] public string Id { get; set; }

        [Required] public string OwnerId { get; set; }
        public string FreelancerId { get; set; }

        [Required] public BiddingContentDto BiddingContent { get; set; }
    }
}