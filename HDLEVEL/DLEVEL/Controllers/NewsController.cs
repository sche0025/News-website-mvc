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
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.xml;
using System.Web.Helpers;
using System.IO;

namespace DLEVEL.Controllers
{
    [Authorize]
    public class NewsController : Controller
    {
        private Entities1 db = new Entities1();

        // GET: News
        public ActionResult Index()
        {
            string currentUserId = User.Identity.GetUserId();
            if (User.IsInRole("Administrator"))
            {
                var news = db.News.Include(n => n.AspNetUser);
                return View(news.ToList());
               // return View(news.OrderBy(t => t.datePub).ToList());
            }
            else
            {
                return View(db.News.Where(m => m.user_id == currentUserId).ToList());
                // return View(db.News.OrderBy(t => t.datePub).Where(m => m.user_id == currentUserId).ToList());
            }
       

            // var news = db.News.Include(n => n.AspNetUser);
            //return View(news.ToList());
        }

        // GET: News/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = db.News.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        // GET: News/Create
        public ActionResult Create()
        {
            //ViewBag.user_id = new SelectList(db.AspNetUsers, "Id", "Email");
            return View();
        }

        // POST: News/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "news_ID,user_id,title,datePub,comment,Create_Date")] News news)
        {
        
            news.datePub = DateTime.Now;
            var myNews = new News
            {
              
                user_id = User.Identity.GetUserId(),
                title = news.title,
                datePub = news.datePub,
                comment = news.comment
            };
            if (ModelState.IsValid)
            {
                db.News.Add(myNews);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.user_id = new SelectList(db.AspNetUsers, "Id", "Email", news.user_id);
            return View(news);
        }

        // GET: News/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = db.News.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            ViewBag.user_id = new SelectList(db.AspNetUsers, "Id", "Email", news.user_id);
            return View(news);
        }

        // POST: News/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "news_ID,user_id,title,datePub,comment")] News news)
        {
            if (ModelState.IsValid)
            {
                db.Entry(news).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.user_id = new SelectList(db.AspNetUsers, "Id", "Email", news.user_id);
            return View(news);
        }

        // GET: News/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = db.News.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        // POST: News/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            News news = db.News.Find(id);
            db.News.Remove(news);
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
