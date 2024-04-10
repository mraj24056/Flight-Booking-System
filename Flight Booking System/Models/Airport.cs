using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Flight_Booking_System.Models
{
    [Table("Airport")]
    public class Airport
    {
        [Key]
        public int AirportId { get; set; }

        [Required, MaxLength(30)]
        public string AirportName { get; set; }

        [Required, MaxLength(30)]
        public string City { get; set; }

        [Required, MaxLength(30)] 

        public string Country { get; set; }
        public ICollection<Flight> Flights { get; set; }
    }
}