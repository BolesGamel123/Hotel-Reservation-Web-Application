using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PolaHotel.Models
{
     public enum Gender
    {
        Male,
        Female
    }
    public class Customer
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "you should Enter Your Name")]
        [MinLength(3)]
        public string Name { get; set; }
        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage = "you should enter your phone Number")]
        public int phone_Number { get; set; }
        [Display(Name = "Enter your National ID ")]
        [Required(ErrorMessage = "you should Enter your Your National ID")]

        
        public long NationalID { get; set; }
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "you should Enter your Email address")]
        public string Email { get; set; }
        [Required(ErrorMessage = "you should Enter your nationality")]
        public string Nationality { get; set; }
        [Required(ErrorMessage = "Select your Gender")]
        public Gender Gender { get; set; }
        public virtual ICollection<Reservation> Reservations{get;set;}
    }
}

