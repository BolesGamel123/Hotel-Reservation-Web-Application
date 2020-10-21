using PolaHotel.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PolaHotel.Controllers
{
    public class RoomController : Controller
    {
        // GET: Room
        //RoomService service = new RoomService();
        ApplicationDbContext Context = new ApplicationDbContext();


        [Authorize]
        public ActionResult Index()
        {
            ViewBag.Reserve = Context.roomReservations.ToList();

            return View(Context.Room_Categories.ToList());
        }

        public ActionResult IndexCustomer()
        {
            ViewBag.Reserve = Context.roomReservations.ToList();

            return View(Context.Room_Categories.ToList());
        }

        [Authorize]

        public ActionResult Add()
        {
            ViewBag.RoomCategorys = Context.Room_Categories.ToList();
            return View();
        }


        [HttpPost]
        public ActionResult Add(Room room, HttpPostedFileBase upload)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.RoomCtegorys = Context.Room_Categories.ToList();
                return View(room);
            }
            else
            {
                try
                {
                    if (upload != null)
                    {
                        string path = Path.Combine(Server.MapPath("~/Content/Images"), upload.FileName);
                        upload.SaveAs(path);
                        room.Image = upload.FileName;
                    }
                    Context.Rooms.Add(room);


                    Context.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                    ViewBag.RoomCtegorys = Context.Room_Categories.ToList();
                    return View(room);
                }

            }

        }


        [Authorize]

        public ActionResult Edit(int roomNo)
        {
            ViewBag.RoomCtegorys = Context.Room_Categories.ToList();
            return View(Context.Rooms.FirstOrDefault(r => r.ID == roomNo));

        }


        [HttpPost]
        public ActionResult Edit(Room room, HttpPostedFileBase upload)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.RoomCtegorys = Context.Room_Categories.ToList();
                return View(room);
            }
            else
            {
                try
                {
                    if (upload != null)
                    {
                        string path = Path.Combine(Server.MapPath("~/Content/Images"), upload.FileName);
                        upload.SaveAs(path);
                        room.Image = upload.FileName;
                    }
                    Room Room = Context.Rooms.FirstOrDefault(r => r.ID == room.ID);
                    if (Room != null)
                    {
                       
                        Room.Image = room.Image;
                        Room.Description = room.Description;
                        Room.isavailble = room.isavailble;
                        //Room.NumberOfBeeds = room.NumberOfBeeds;
                        Room.catID = room.catID;
                    }
                    Context.SaveChanges();


                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                    ViewBag.RoomCtegorys = Context.Room_Categories.ToList();
                    return View(room);
                }

            }

        }

        [Authorize]


        public ActionResult Details(int id)
        {
            Room room = Context.Rooms.FirstOrDefault(r => r.ID == id);
            return View(room);
        }

        public ActionResult Delete(int id)
        {
            Room room = Context.Rooms.FirstOrDefault(r => r.ID == id);
            Context.Rooms.Remove(room);
            Context.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult CheckRoomNumber(int Room_Number)
        {
            Room room = Context.Rooms.FirstOrDefault(r => r.ID == Room_Number);
            if (room != null)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            else
                return Json(true, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetRoomForCategory(int id)
        {

            List<Room> rooms = Context.Rooms.Where(r => r.catID == id).ToList();

            return View(rooms);
        }
    }
}