using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PolaHotel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PolaHotel.Controllers
{
    public class RoleController : Controller
    {
        public ActionResult New()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> New(string RoleName)
        {
            if (RoleName != null)
            {
                ApplicationDbContext context = new ApplicationDbContext();
                RoleStore<IdentityRole> store = new RoleStore<IdentityRole>(context);
                RoleManager<IdentityRole> manager = new RoleManager<IdentityRole>(store);

                IdentityRole role = new IdentityRole();
                role.Name = RoleName;
                IdentityResult result = await manager.CreateAsync(role);
                if (result.Succeeded)
                    return RedirectToAction("Index", "Home");
                else
                {
                    ViewBag.Error = result.Errors;
                    ViewBag.RoleName = RoleName;
                    return View();
                }
            }
            return View();
        }
    }
}