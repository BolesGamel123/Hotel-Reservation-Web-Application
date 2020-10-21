using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PolaHotel.Models
{
    
    public class Room_Category
    {
       
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int catID { get; set; }

        [Required]
        [Display(Name = "Entert Room Price ?")]
        public double Price { get; set; }
        public string Image { get; set; }

        public string View { set; get; }

       
        public virtual ICollection<Room> Rooms { get; set; }


       


    }
}