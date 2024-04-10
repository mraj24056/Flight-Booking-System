using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace Flight_Booking_System.Models
{
    [Table("Airline")]
    public class Airline
    {
        [Key]

        public int AirlineId { get; set; }

        [Display(Name ="Airline Name")]
        [Required(ErrorMessage ="Airline Name Required.")]
        [MaxLength(30, ErrorMessage ="Maximum 30 characters"),MinLength(3, ErrorMessage ="Minimum 3 characters allowed")]
        public string AirlineName { get; set; }

        [Required(ErrorMessage ="Capacity Required")]
        [Display(Name ="Seating Capacity")]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a value greater than 0")]

        public int SeatingCapacity { get; set; }
        public ICollection<Flight> Flights { get; set; }
        

    }
}