using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PolaHotel.Models
{
    public class RoomReservation
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column(Order =0)]
        [ForeignKey("Room")]
        public int RoomID { get; set; }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column(Order = 1)]
        [ForeignKey("Reservation")]
        public int RserveID { get; set; }


        public virtual Room Room { get; set; }

        public virtual Reservation Reservation { get; set; }
    }
}