using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Flight_Booking_System.Models
{
    public class Schedule
    {
        [Key]
        public int ScheduleId { get; set; }

        [ForeignKey("Flight")]
        public int FlightId { get; set; }
        [ForeignKey("Airline")]
        public int AirlineId { get; set; }
        public string Source { get; set; }
        public string Destination { get; set; }

        [Display(Name = "Flight Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FlightDate { get; set; }

        public string DepartureTime { get; set; }
        public string ArrivalTime { get; set;}
       

        [Required(ErrorMessage = "Price Required")]
        [Display(Name = "Price")]
        public float Price { get; set; }
        public Flight Flight { get; set; }
        public Airline Airline { get; set; }
    }
}