using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PolaHotel.Models
{
   
    public class Room
    {
        //[Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        //[Remote(action: "CheckRoomNumber", controller: "Room", ErrorMessage = "Room Already Exist")]

        public int ID { get; set; }

        public string Image { get; set; }

        public string Description { get; set; }
        
        [Required]
        [Display(Name = "You should select room's availability?")]
        public bool isavailble { get; set; }

        //[Required(ErrorMessage = "enter number of Beeds")]
        //[Range(1, 4, ErrorMessage = "Number Of Beds Must Be Less Than 5 Beds")]
        //public int NumberOfBeeds { get; set; }  

        [ForeignKey("RoomCategory")]
        public int catID { set; get; }

        [ForeignKey("catID")]

        public virtual Room_Category RoomCategory { get; set; }

        //[ForeignKey("Reservation")]
        //public int ReservID { set; get; }

        //[ForeignKey("ReservID")]
        //public virtual Reservation Reservation { get; set; }
        

       public virtual ICollection<RoomReservation> RoomReservations { get; set; }

        


    }
}