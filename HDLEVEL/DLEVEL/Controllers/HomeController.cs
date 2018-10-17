using DLEVEL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment.Controllers
{

    public class HomeController : Controller
    {
        private Entities1 db = new Entities1();
        public ActionResult Index()
        {
          
       
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "FAQ";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            List<int> ages = new List<int>();
            ages.Add(db.Staffs.Where(m => m.age < 19).Count());
            ages.Add(db.Staffs.Where(m => m.age > 20 && m.age <= 40).Count());
            ages.Add(db.Staffs.Where(m => m.age > 40 && m.age <= 60).Count());
            ages.Add(db.Staffs.Where(m => m.age > 60 && m.age <= 80).Count());
            ages.Add(db.Staffs.Where(m => m.age > 80).Count());
            ViewBag.ages = ages;

            return View();
        }

        public ActionResult Documentation()
        {
            return View();
        }

        public List<int> getStaffAges()
        {
          
            return null;
        }

        public int getStaff2()
        {
            return db.Staffs.Where(m => m.age > 20 && m.age <= 40).Count();
        }

        public int getStaff3()
        {
            return db.Staffs.Where(m => m.age > 40 && m.age <= 60).Count();
        }

        public int getStaff4()
        {
            return db.Staffs.Where(m => m.age > 60 && m.age <= 80).Count();
        }

        public int getStaff5()
        {
            return db.Staffs.Where(m => m.age > 80).Count();
        }
    }
}