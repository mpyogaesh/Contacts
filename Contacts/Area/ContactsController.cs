using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using Contacts.Controllers;
using Contacts.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Contacts.Area
{
    public class ContactsController : Controller
    {
        private ContactEntities db = new ContactEntities();

        // GET: Contacts
        public ActionResult Index()
        {
            return View(db.Contacts.ToList().OrderByDescending(c=>c.Id));
        }

        // GET: Contacts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = db.Contacts.Find(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        // GET: Contacts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Contacts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName,Email,PhoneNumber,Address,City,State,Country,PostalCode")] Contact contact)
        {
            if (ModelState.IsValid)
            {
                db.Contacts.Add(contact);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(contact);
        }

        // GET: Contacts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = db.Contacts.Find(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        // POST: Contacts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,Email,PhoneNumber,Address,City,State,Country,PostalCode")] Contact contact)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contact).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(contact);
        }

        // GET: Contacts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = db.Contacts.Find(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        // POST: Contacts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Contact contact = db.Contacts.Find(id);
            db.Contacts.Remove(contact);
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


        // GET: Contacts/Details/5
        public ActionResult MapDetails()
        {
            return View(db.Contacts.ToList());

        }


        public ActionResult DisplayMaps()
        {
            string url = "https://nominatim.openstreetmap.org/search?postalcode={0}&country={1}&format=json";

            List<MapViewModel> maps = (from c in db.Contacts.ToList()
                                       select new MapViewModel
                                       {
                                           FirstName = c.FirstName,
                                           LastName = c.LastName,
                                           Address = c.Address,
                                           City = c.City,
                                           State = c.State,
                                           Country = c.Country,
                                           PostalCode = c.PostalCode,
                                           Email = c.Email,
                                           PhoneNumber = c.PhoneNumber,
                                           PinCode = c.PostalCode,
                                           URL = string.Format(url, c.PostalCode, c.Country),
        }).ToList();

            return View(maps);
        }
    }
}
