using System;

namespace SimpleBookingWidget.Core.Models
{
    public class ScheduleModel
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public long Id { get; set; }
        public long ProductId { get; set; }
        public long ScheduleId { get; set; }
        public int Availability { get; set; }
        public bool Available { get; set; }
        public bool? Cta { get; set; }
        public bool? Ctd { get; set; }
        public int? MinStay { get; set; }
        public int? MaxStay { get; set; }
    }
}
