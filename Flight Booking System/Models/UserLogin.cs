using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;

namespace Flight_Booking_System.Models
{
    [Table("UserLogin")]
    public class UserLogin
    {
        [Key]
        [Required] public int UserID { get; set; }

        [Required(ErrorMessage = "First Name Required")]
        [Display(Name = "First Name")]
        [MaxLength(40, ErrorMessage = "Max 20 characters allowed"), MinLength(3, ErrorMessage = "Minimum 5 characters required.")]
        [RegularExpression(@"^[a-zA-Z0-9\s]*$", ErrorMessage = "Special characters are not allowed.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name Requird")]
        [Display(Name = "Last Name")]
        [MaxLength(40, ErrorMessage = "Max 20 characters allowed"), MinLength(1, ErrorMessage = "Minimum 5 characters required.")]
        [RegularExpression(@"^[a-zA-Z0-9\s]*$", ErrorMessage = "Special characters are not allowed.")]
        public string LastName { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email ID required.")]
        [MaxLength(20, ErrorMessage = "Maximum 30 characters allowed"), MinLength(5, ErrorMessage = "Minimum 5 characters required.")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Invalid Email Format")]
        public string Email { get; set; }
        public string Role { get; set; }

        [Required(ErrorMessage = "Password Required")]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "Minimum 8 characters required."), MaxLength(16, ErrorMessage = "Maximum 16 characters allowed.")]
        public string Password { get; set; }

        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Incorrect Password")]
        [MinLength(8, ErrorMessage = "Minimum 8 characters required."), MaxLength(16, ErrorMessage = "Maximum 16 characters allowed.")]
        public string ConfirmPassword { get; set; }

         

        [Required(ErrorMessage = "Age Required.")]
        [Range(12, 120, ErrorMessage = "Minimum 12 years to book the flight.")]
        public int Age { get; set; }
        
        [Display(Name = "Phone Number")]
        [Required(ErrorMessage = "Phone Number Required")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Please enter a 10-digit phone number.")]
        public string PhoneNumber { get; set; }
        public ICollection<Booking> Bookings { get; set; }
        public ICollection<Payment> Payments { get; set; }



    }
}