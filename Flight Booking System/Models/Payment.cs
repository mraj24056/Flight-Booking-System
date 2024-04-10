using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Flight_Booking_System.Models
{
    public class Payment
    {
        [Key]
        public int Id { get;set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime PaymentDate { get; set; }
        public string PaymentMode { get; set; }
        public string UserName { get; set; }
        public string CardType { get; set; }

        [RegularExpression(@"^\d{16}$", ErrorMessage = "Invalid Card Number")]
        public long CardNo { get; set; }

        [RegularExpression(@"^\d{3}$", ErrorMessage = "Invalid CVV")]
        public int CVV { get; set; }
        public decimal Amount { get; set; }
        public int BookingId { get; set; }
        public Booking  Booking { get; set; }
        public int UserID {get; set; }
        public UserLogin UserLogin { get; set; }
        public int ScheduleId { get; set; }
        public Schedule Schedule { get; set; }
    }
}