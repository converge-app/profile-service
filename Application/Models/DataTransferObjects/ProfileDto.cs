using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Application.Models.DataTransferObjects
{
    public class ProfileDto
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public string UserId { get; set; }
        public string ProfilePictureUrl { get; set; }
        [Required]
        public string Title { get; set; }
        public decimal Rating { get; set; }
        public string Description { get; set; }
        public List<string> Skills { get; set; } = new List<string>();
        public List<string> Experience { get; set; } = new List<string>();
    }
}