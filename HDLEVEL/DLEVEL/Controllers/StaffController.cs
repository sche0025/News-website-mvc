using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DLEVEL.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;
using System.Net.Mail;


namespace DLEVEL.Controllers
{



    public class StaffController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private Entities1 db = new Entities1();
        //
        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public string option()
        {

            string option= "{color: ['#3398DB'],tooltip:{trigger: 'axis',axisPointer:{type: 'shadow'}},grid:{left: '3%',right: '4%',bottom: '3%',containLabel: true},xAxis: [{type: 'category',data: ['0-20', '21-40', '41-60', '61-80', '80+'],axisTick:{alignWithLabel: true}}],yAxis: [{type: 'value'}],series: [{name: 'Numbers',type: 'bar',barWidth: '60%',data:["+ getStaff1() + ", "+ getStaff2() + ", "+ getStaff3() + ", "+getStaff4()+", "+ getStaff5() + "]}]}";
            return option;
        }

        public int getStaff1()
        {
            return db.Staffs.Where(m => m.age <= 20).Count();
        }

        public int getStaff2()
        {
            return db.Staffs.Where(m => m.age >20 && m.age<=40).Count();
        }

        public int getStaff3()
        {
            return db.Staffs.Where(m => m.age >40 && m.age <= 60).Count();
        }

        public int getStaff4()
        {
            return db.Staffs.Where(m => m.age > 60 && m.age <= 80).Count();
        }

        public int getStaff5()
        {
            return db.Staffs.Where(m => m.age > 80).Count();
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        // GET: Staff
        [Authorize(Roles = "Administrator")]
        public ActionResult Index()
        {
            //return View(db.Staffs.ToList());
            string currentUserId = User.Identity.GetUserId();
            return View(db.Staffs.Where(m => m.user_id != currentUserId).ToList());
      
        }

        // GET: Staff/Details/5
        [Authorize]
        public ActionResult Details(String id)
        {
            if (id == "")
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Staff staff = db.Staffs.Find(id);
            if (staff == null)
            {
                return HttpNotFound();
            }
            return View(staff);
        }


        public ActionResult Email()
        {
            return View();
        }

        public ActionResult ageDemo()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Email(FormCollection collection)
        {
            ViewBag.Error = "";
            ViewBag.sendInfo = "";
            String msg = collection["message"];
            String title = collection["title"];
            var useremails = collection["userEmail"];

            {
                // var body = "<p>Email From: {0} ({1})</p><p>Message:</p><p>{2}</p>";
                var message = new MailMessage();
                if (useremails != null)
                {
                    foreach (string item in useremails.Split(','))
                    {
                        message.To.Add(new MailAddress(item));
                    }
                }
                else
                {
                    TempData["Message"] = "Sorry,Please select at least one user";
                    return RedirectToAction("Index");
                }

                message.From = new MailAddress("sche0025@student.monash.edu");
                if (title.Trim() != "" && title != null)
                {
                    message.Subject = title;
                }
                else
                {
                    TempData["Message"] = "Sorry,Please input a subject";
                    return RedirectToAction("Index");
                }

                if (msg.Trim() != "" || msg != null)
                {
                    message.Body = string.Format(msg);
                }
                else
                {
                    msg = "Hello";
                    message.Body = string.Format(msg);
                }

                message.IsBodyHtml = true;
                using (var smtp = new SmtpClient())
                {
                    var credential = new NetworkCredential
                    {
                        UserName = "",
                        Password = ""
                    };

                    smtp.Credentials = credential;
                    smtp.Host = "smtp.monash.edu.au";
                    await smtp.SendMailAsync(message);
                }
            }

            TempData["Message"] = "Sent Successfully";
            return RedirectToAction("Index");
        }




      
        // GET: Staff/Create
        public ActionResult Create()
        {
            var staff = new MyModel();
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(MyModel model)
        {
            if (ModelState.IsValid)
            {
                var newUser = db.AspNetUsers.Where(m => m.Email == model.Email).ToList();
                if (newUser.Count != 0)
                {
                    return RedirectToAction("Error", "Staff");
                }
                else
                {
                    var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                    var result = await UserManager.CreateAsync(user, model.Password);
                    if (result.Succeeded)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        var staff = new Staff
                        {
                            user_id = user.Id,
                            staff_Name = model.staff_Name,
                            Address = model.address,
                            company_Name = model.company_Name,
                            phone = model.phone,
                            email = model.Email,
                            age = model.age
                        };
                        db.Staffs.Add(staff);
                        db.SaveChanges();

                        return RedirectToAction("Index", "News");
                    }

                }

            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        public ActionResult Error()
        {
            return View();
        }
        [Authorize]
        // GET: Staff/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == "")
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Staff staff = db.Staffs.Find(id);
            if (staff == null)
            {
                return HttpNotFound();
            }
            return View(staff);
        }
        //edit my profile

        [Authorize]
        public ActionResult EditMyProfile()
        {
            var id = User.Identity.GetUserId();
            Staff staff = db.Staffs.Find(id);
            if (staff == null)
            {
                return HttpNotFound();
            }
            return View(staff);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditMyProfile([Bind(Include = "staff_ID,staff_Name,company_Name,Address,phone,email,age,user_id")] Staff staff)
        {
            if (ModelState.IsValid)
            {
                db.Entry(staff).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index","Manage");
            }
            return View(staff);
        }

        // POST: Staff/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "staff_ID,staff_Name,company_Name,Address,phone,email,age,user_id")] Staff staff)
        {
            if (ModelState.IsValid)
            {
                db.Entry(staff).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(staff);
        }
        [Authorize]
        // GET: Staff/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == "")
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            AspNetUser user = db.AspNetUsers.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Staff/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            AspNetUser user = db.AspNetUsers.Find(id);
            db.AspNetUsers.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
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
