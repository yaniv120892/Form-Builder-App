using Form_Builder_App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Form_Builder_App.Controllers
{
    public class HomeController : Controller
    {
        public formBuilderDBEntities1 db = new formBuilderDBEntities1();

        public ActionResult Index(string errorMsg = null)
        {
            ViewBag.Error = errorMsg;
            return View();
        }
        [HttpPost]
        public ActionResult Login(string userName, string password)
        {
            int userID = -1;
            if (this.HttpContext.Request.Form["Login_Or_Register"] == "Register")
            {
                userID = DoRegister(userName, password);
                if (userID == -1)
                {
                    ViewBag.Error = "Error, user name already exists";
                    return View("Index");
                }
            }
            else
            {
                string hashPass = password.GetHashCode() + "";
                var loggedInUser = db.tblUsers.Where(m => m.userName == userName & m.password == hashPass).Select(m => m).SingleOrDefault();
                if (loggedInUser == null)
                {
                    ViewBag.Error = "Error, password or user name are incorrect";
                    return View("Index");
                }
                //update useragent and ip
                userID = loggedInUser.userID;
            }
            Session["logged_in_user"] = new tblUser(userID,userName,null);
            return RedirectToAction("Index", "forms", new { area = "" });
        }



        private int DoRegister(string userName, string password)
        {
            int userID = -1;
            string hashPass = password.GetHashCode() + "";
            var listUsersWithSameUserName = db.tblUsers.Where(m => m.userName == userName).Select(m => m).ToList();
            if (listUsersWithSameUserName.Count > 0)
            {
                return -1;
            }
            try
            {
                userID = db.tblUsers.Max(m => m.userID) + 1;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                userID = 1;
            }

            db.tblUsers.Add(new tblUser(userID, userName, hashPass));
            db.SaveChanges();
            return userID;
        }
        public ActionResult Logout()
        {
            Session["logged_in_user"] = null;
            return View("Index");

        }

    }
}