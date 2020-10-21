using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PolaHotel.Models
{
    public class Reservation
    {
        [Key]
        public int ReservID { get; set; }
        public DateTime ChickIn { get; set; }
        public DateTime choutOut { get; set; }
        public DateTime ReserveDate { get; set; }
        public int Adult { get; set; }
        public int Childern { get; set; } 
       // [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int Period { get; set; }
        [ForeignKey("Customer")]
        public int CustomerID { get; set; }
        
        [ForeignKey("CustomerID")]
        public virtual Customer Customer { get; set; }

        //public virtual ICollection<Room> Room { get; set; }

        public virtual ICollection<RoomReservation> RoomReservations { get; set; }

       

    }
}