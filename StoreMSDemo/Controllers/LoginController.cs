using StoreMSDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace StoreMSDemo.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Autherize(StoreMSDemo.Models.User userModel)
        {
            var hashed = "";
            using (StoreMSEntities db = new StoreMSEntities())
            {
                if (userModel.Password != null)
                {
                    hashed = Crypto.Hash(userModel.Password, "MD5");
                    var userDetails = db.Users.Where(x => x.Username == userModel.Username && x.Password == hashed && x.isActive == true).FirstOrDefault();
                    if (userDetails != null)
                    {
                        Session["UserID"] = userDetails.UserID;
                        Session["UserName"] = userDetails.Username;
                        TempData["UserID"] = userDetails.UserID;
                        return RedirectToAction("DisplayItems", "Items");
                    }

                    TempData["Message"] = " Wrong username & password.";
                }

                return View("Login", userModel);
            }
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Login", "Login");
        }
    }
}