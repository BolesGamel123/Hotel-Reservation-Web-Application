using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using PolaHotel.Models;

namespace PolaHotel.Controllers
{
    public class ReservationsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Search(string Name)
        {
            ViewBag.name = Name;

           List<Reservation> Reservations = db.Reservations
                .Where(r => r.Customer.Name.Contains(Name)).ToList();

            return View("Index", Reservations);
           
        }

        [HttpPost]
        public JsonResult SearchPost(string Name)
        {

           var customers = db.customers
                .Where(c => c.Name.StartsWith(Name)).Select(c => c.Name);

            return Json(customers, JsonRequestBehavior.AllowGet);

        }

        



        [Authorize]
        // GET: Reservations
        public ActionResult Index()
        {
            var reservations = db.Reservations.Include(r => r.Customer);
            return View(reservations.ToList());
        }

        [Authorize]

        // GET: Reservations/Details/5
        public ActionResult Details(int? id)
        {
            //ViewBag.RoomCategory = db.room_CategoryReservations.ToList().Where(r=>r.ReservID==id);
            ViewBag.Room = db.Room_Categories.ToList();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = db.Reservations.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            return View(reservation);
        }
      

        // GET: Reservations/Create
        public ActionResult Create()
        {
            ViewBag.Room_Categories = db.Room_Categories.ToList();
            return View();
        }

        // POST: Reservations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Reservation reservation, int catID, int NumberofRooms)
        {
            ViewBag.Room_Categories = db.Room_Categories.ToList();
            //Customer customer = db.customers.Where(c => c.ID == 1).FirstOrDefault();
            var availableRooms = db.Rooms
                .Where(m => m.catID == catID && m.RoomReservations.All
               (r => r.Reservation.choutOut <= reservation.ChickIn || r.Reservation.ChickIn >= reservation.choutOut))
                .Take(NumberofRooms);

            if (availableRooms.Count() != 0 && availableRooms.Count() >= NumberofRooms)
            {
                Reservation reservation1 = reservation;
                reservation1.ChickIn = (DateTime)reservation.ChickIn;
                reservation1.choutOut = (DateTime)reservation.choutOut;
                reservation1.Adult = reservation.Adult;
                reservation1.Childern = reservation.Childern;
                reservation1.ReserveDate = DateTime.Now;
                reservation1.CustomerID = (int)Session["id"];
                reservation1.Period = (int)(reservation.choutOut - reservation.ChickIn).TotalDays;
                foreach (var item in availableRooms)
                {
                    db.roomReservations.Add(
                        new RoomReservation()
                        {
                            RoomID = item.ID,
                            RserveID = reservation1.ReservID
                        }
                        );

                }
                db.Reservations.Add(reservation1);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }


            TempData["Message"] = "Room is Not Available Now";
            return View();

        }

        
        [Authorize]
        // GET: Reservations/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.Room_Categories = db.Room_Categories.ToList();
            Reservation reservation = db.Reservations
                .FirstOrDefault(r => r.ReservID == id);
            return View(reservation);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Reservation reservation, int catID, int NumberofRooms)
        {
            
            ViewBag.Room_Categories = db.Room_Categories.ToList();

            var availableRooms = db.Rooms
                .Where(m => m.catID == catID && m.RoomReservations.All
               (r => r.Reservation.choutOut <= reservation.ChickIn || r.Reservation.ChickIn >= reservation.choutOut))
                .Take(NumberofRooms);

            Reservation reservation1 = db.Reservations
                .FirstOrDefault(r => r.ReservID == reservation.ReservID);
            if (availableRooms.Count() != 0 && availableRooms.Count() >= NumberofRooms)
            {
                List<RoomReservation> roomReservations = db.roomReservations
                .Where(r => r.RserveID == reservation.ReservID).ToList();
                db.roomReservations.RemoveRange(roomReservations);
                db.SaveChanges();

                reservation1.ChickIn = reservation.ChickIn;
                reservation1.choutOut = reservation.choutOut;
                reservation1.Period = (int)(reservation.choutOut - reservation.ChickIn).TotalDays;
                reservation1.Adult = reservation.Adult;
                reservation1.Childern = reservation.Childern;
                reservation1.CustomerID = reservation.CustomerID;
                reservation1.ReserveDate = DateTime.Now;
                foreach (var item in availableRooms)
                {
                    db.roomReservations.Add(
                        new RoomReservation()
                        {
                            RoomID = item.ID,
                            RserveID = reservation1.ReservID
                        }
                        );

                }
              
                db.SaveChanges();
                return RedirectToAction("Index", "Reservations");


            }

            TempData["Message"] = "Room is Not Available Now";
            return View();
        }



        // GET: Reservations/Delete/5
        //public ActionResult Delete(int? id)
        //    {
        //        if (id == null)
        //        {
        //            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //        }
        //        Reservation reservation = db.Reservations.Find(id);
        //        if (reservation == null)
        //        {
        //            return HttpNotFound();
        //        }
        //        return View(reservation);
        //    }

        //    // POST: Reservations/Delete/5
        //    [HttpPost, ActionName("Delete")]
        //    [ValidateAntiForgeryToken]
        //    public ActionResult DeleteConfirmed(int id)
        //    {
        //        Reservation reservation = db.Reservations.Find(id);
        //        db.Reservations.Remove(reservation);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        [Authorize]

        public ActionResult Delete(int id)
        {
            Reservation reserve = db.Reservations.FirstOrDefault(r => r.ReservID == id);
            db.Reservations.Remove(reserve);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult New()
        {

            ViewBag.Room_Categories = db.Room_Categories.ToList();
           
            return View();
        }

        




        [HttpPost]
        public ActionResult New(Reservation reservation, int catID, int NumberofRooms)
        {
            ViewBag.Room_Categories = db.Room_Categories.ToList();
            //Customer customer = db.customers.Where(c => c.ID == 1).FirstOrDefault();
            var availableRooms = db.Rooms
                .Where( m => m.catID == catID &&  m.RoomReservations.All
                (r => r.Reservation.choutOut <= reservation.ChickIn || r.Reservation.ChickIn >= reservation.choutOut))
                .Take(NumberofRooms);

            if (availableRooms.Count() != 0 && availableRooms.Count()>= NumberofRooms)
            {
                Reservation reservation1 = reservation;
                reservation1.ChickIn = (DateTime)reservation.ChickIn;
                reservation1.choutOut = (DateTime)reservation.choutOut;
                reservation1.Adult = reservation.Adult;
                reservation1.Childern = reservation.Childern;
                reservation1.ReserveDate = DateTime.Now;
                reservation1.CustomerID = (int)Session["id"];
                reservation1.Period = (int)(reservation.choutOut - reservation.ChickIn).TotalDays;
                foreach (var item in availableRooms)
                {
                    db.roomReservations.Add(
                        new RoomReservation()
                        {
                            RoomID = item.ID,
                            RserveID = reservation1.ReservID
                        }
                        );

                }
                db.Reservations.Add(reservation1);
                TempData["reservation1"] = reservation1;
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }


            TempData["Message"] = "Room is Not Available Now";
            return View();
        }
        public ActionResult Thanks()
        {
            Reservation reservation = TempData["reservation1"] as Reservation;
            return View(reservation);
        }

        [HttpPost]
        public JsonResult getPrice(int CatID)
        {
            var price = db.Room_Categories.Where(c=>c.catID==CatID).Select(c => c.Price);
            return Json(price,JsonRequestBehavior.AllowGet);
        }
         protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
