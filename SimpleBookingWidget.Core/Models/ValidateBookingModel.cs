using System.Collections.Generic;

namespace SimpleBookingWidget.Core.Models
{
    public class ValidateBookingModel
    {
        public bool IsValid { get; set; }
        public ValidateBookingErrorModel Errors { get; set; }
    }

    public class ValidateBookingErrorModel
    {
        public string Product { get; set; }
        public string Quote { get; set; }
    }
}
