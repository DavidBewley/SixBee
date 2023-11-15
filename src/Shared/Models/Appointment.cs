using System.ComponentModel.DataAnnotations;

namespace AppointmentBooking.Shared.Models
{
    public class Appointment
    {
        public Guid AppointmentId { get; set; }

        [Required]
        [MinLength(1, ErrorMessage = "Please enter your name Max 30 characters")]
        [MaxLength(30, ErrorMessage = "Please enter your name Max 30 characters")]
        public string Name { get; set; } = null!;
        [Required]
        public DateTime AppointmentDateTime { get; set; }
        [Required]
        [MinLength(10,ErrorMessage = "Please describe your issue in 20 - 200 characters")]
        [MaxLength(200,ErrorMessage = "Please describe your issue in 20 - 200 characters")]
        public string Issue { get; set; } = null!;
        [Required]
        [Phone(ErrorMessage = "Please enter a valid contact number")]
        public string ContactNumber { get; set; } = null!;
        [Required]
        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        public string EmailAddress { get; set; } = null!;
        public bool IsApproved { get; set; }

    }
}
