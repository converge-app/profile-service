using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Application.Models.DataTransferObjects
{
    public class BiddingContentDto
    {
        
        [Required]
        public string Title { get; set; }
        
        [Required]
        public string Description { get; set; }
        
        [Required]
        public string Category { get; set; }
        
        [Required]
        public string SubCategory { get; set; }
        
        [Required]
        public decimal Amount { get; set; }
        public List<string> Files { get; set; } = new List<string>();
    }
}