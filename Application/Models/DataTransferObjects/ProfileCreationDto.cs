using System.ComponentModel.DataAnnotations;

namespace Application.Models.DataTransferObjects
{
    public class ProfileCreationDto
    {
        [Required]
        public string ProjectId { get; set; }

        [Required]
        public string FreelancerId { get; set; }

        public string Message { get; set; }

        [Required]
        public decimal Amount { get; set; }
    }
}