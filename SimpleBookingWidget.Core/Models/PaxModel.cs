using SimpleBookingWidget.Commons;
using System.ComponentModel.DataAnnotations;

namespace SimpleBookingWidget.Core.Models
{
    public class PaxModel
    {
        public string Id { get; set; }
        [Required]
        public string First { get; set; }
        [Required]
        public string Last { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Mobile { get; set; }
        public int? Age { get; set; }
        public string Notes { get; set; }
        public Gender? Gender { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }

    }
}
