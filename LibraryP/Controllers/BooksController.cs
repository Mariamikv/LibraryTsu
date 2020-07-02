using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using LibraryP.Models;


namespace LibraryP.Controllers
{
    public class BooksController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
       
        // GET: Books
        public ActionResult Index()
        {
            var books = db.Books.Include(h => h.BorrowHistories)
                .Select(b => new BookViewModel
                {
                    BookId = b.BookId,
                    Title = b.Title,
                    Author = b.Author,
                    PublishingHouse = b.PublishingHouse,
                    Condition = b.Condition,
                    Quantity = b.Quantity,
                    Image = b.Image,
                    IsAvailable = !b.BorrowHistories.Any(h => h.ReturnDate == null)
                }).ToList();
            return View(books);
        }

        // GET: Books/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }
        

        // GET: Books/Create
        [HttpGet]
        public ActionResult Create()
        {

            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Book book)
        {
            string fileName = Path.GetFileNameWithoutExtension(book.ImageFile.FileName);
            string extention = Path.GetExtension(book.ImageFile.FileName);
            fileName = fileName + DateTime.Now.ToString("yymmssfff") + extention;
            book.Image = "~/Image/" + fileName;
            fileName = Path.Combine(Server.MapPath("~/Image/"), fileName);
            book.ImageFile.SaveAs(fileName);
            db.Books.Add(book);
            db.SaveChanges();
            ModelState.Clear();

            return RedirectToAction("Index");
        }

        // GET: Books/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Book book)
        {
            if (ModelState.IsValid)
            {
                string fileName = Path.GetFileNameWithoutExtension(book.ImageFile.FileName);
                string extention = Path.GetExtension(book.ImageFile.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssfff") + extention;
                book.Image = "~/Image/" + fileName;
                fileName = Path.Combine(Server.MapPath("~/Image/"), fileName);
                book.ImageFile.SaveAs(fileName);
                
                

                
                db.Entry(book).State = EntityState.Modified;
                db.SaveChanges();
                ModelState.Clear();
            }
            return RedirectToAction("Index");
        }

        // GET: Books/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Book book = db.Books.Find(id);
            db.Books.Remove(book);
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
