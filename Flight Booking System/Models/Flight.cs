using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Flight_Booking_System.Models
{
    [Table("Flights")]
    public class Flight
    {
        [Key]

        public int FlightId { get; set; }

        [ForeignKey("Airline")]

        public int AirlineId { get; set; }

        [ForeignKey("Airport")]
        public int AirportId { get; set; }

        [Required]

        public string Source { get; set; }

        [Required]

        public string Destination { get; set; }
        public string SourceCode { get; set; }
        public string DestinationCode { get; set; }

        [Required]
        [Display(Name = "Arrival Time")]
        public string ArrivalTime { get; set; }

        [Required]
        [Display(Name = "Departure Time")]
        public string DepartureTime { get; set; }
         
        [Required]
        public int TotalSeatsCapacity { get; set; }
        public ICollection<Schedule> Schedules { get; set; }
        public Airline Airline { get; set; }   
        public Airport Airport { get; set; }
    }
}