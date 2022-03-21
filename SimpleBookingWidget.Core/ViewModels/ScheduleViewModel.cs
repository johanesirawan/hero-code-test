using SimpleBookingWidget.Core.Models;
using System;
using System.Collections.Generic;

namespace SimpleBookingWidget.Core.ViewModels
{
    public class ScheduleViewModel
    {
        public long ProductId { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime? DateEnd { get; set; }
        public ProductDetailModel Product { get; set; }
    }
}
