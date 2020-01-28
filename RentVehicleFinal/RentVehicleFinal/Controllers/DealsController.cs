using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RentVehicleFinal.Models;
using Stripe;

namespace RentVehicleFinal.Controllers
{
    
    public class DealsController : Controller
    {
        private RentVehicleFinalContext db = new RentVehicleFinalContext();

        // GET: Deals
        [Authorize(Roles = "Employee, Admin")]
        public ActionResult Index()
        {
            var stripePublishKey = StripeConfiguration.ApiKey = "pk_test_5kfWz9M6LAxJBS8Sy0hKPr9V004M2OCtYo";
            var stripeSecretKey = StripeConfiguration.ApiKey = "sk_test_uOveI2SFAXbU4XnnSunDN8kN00Q2y172o5";
            ViewBag.StripeSecretKey = stripeSecretKey;
            ViewBag.StripePublishableKey = stripePublishKey;

            //var deals = db.Deals.Include(d => d.Customer).Include(d => d.User).Include(d => d.Vehicle);
            var deals = db.Deals;
            List<Vehicle> vehicles = db.Vehicles.ToList(); 
            foreach(var d in deals)
            {
                var v = d.VehicleId;
                Vehicle vozilo = vehicles.Where(ve => ve.Id == v).First(); //izbereno vozilo
                var total = vozilo.Price * (d.DateTo.DayOfYear - d.DateFrom.DayOfYear);
                d.TotalPrice = total;
               
                //if (DateTime.Now >= d.DateTo)
                //{
                //    vozilo.Available = true;
                //    db.SaveChanges();
                //}
                //var ivona = "icona";
            }
            return View(deals.ToList());
        }

        public ActionResult Charge(string stripeEmail, string stripeToken, int ?Id)
        {
            StripeConfiguration.ApiKey = "sk_test_uOveI2SFAXbU4XnnSunDN8kN00Q2y172o5";
            var deals = db.Deals;
            List<Vehicle> vehicles = db.Vehicles.ToList();
            foreach (var d in deals)
            {
                var v = d.VehicleId;
                Vehicle vozilo = vehicles.Where(ve => ve.Id == v).First(); //izbereno vozilo
                var total = vozilo.Price * (d.DateTo.DayOfYear - d.DateFrom.DayOfYear);
            }

            var customers = new Stripe.CustomerService();
            var charges = new Stripe.ChargeService();

            var customer = customers.Create(new Stripe.CustomerCreateOptions
            {
                Email = stripeEmail,
                Source = stripeToken

            });
            Deal deal = new Deal();

            int ivona = Convert.ToInt32(Id);
            Deal deal1 = db.Deals.Where(de => de.Id == Id).First();
            Session["price"] = deal1.TotalPrice;
            long totalStripes = Convert.ToInt64(Session["price"]);
            deal1.plateno = true;
            db.SaveChanges();
            deal.plateno = true;
            
            var charge = charges.Create(new Stripe.ChargeCreateOptions
            {
                Amount = totalStripes*100,
                Description = "Description for Stripe implmentation",
                Currency = "mkd",
                Customer = customer.Id,
                ReceiptEmail=stripeEmail,
                Metadata=new Dictionary<string, string>
                {
                    {"OrderID",deal.Id.ToString()},
                    {"PostCode","2300"}
                }
               
            });
            
            return View();
        }

        // GET: Deals/Details/5
        [Authorize(Roles = "Employee, Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Deal deal = db.Deals.Find(id);
            if (deal == null)
            {
                return HttpNotFound();
            }
            return View(deal);
        }

        // GET: Deals/Create
        [Authorize(Roles = "Employee, Admin")]
        public ActionResult Create(int ?VehicleId)
        {
            int jovana = (int) VehicleId;
            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "Email");
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email");

            List<Models.Customer> klienti = db.Customers.ToList();
            ViewBag.AllCustomers = klienti;

            //  ViewBag.VehicleId = new SelectList(db.Vehicles, "Id", "RegistrationNum");
            Vehicle temp = db.Vehicles.Where(k => k.Id == VehicleId).First();

            String user = System.Web.HttpContext.Current.User.Identity.Name;
            User us = db.Users.Where(u => u.Email == user).First();
            ViewBag.korisnik = us.Email;
            //.Available = false;
            //db.SaveChanges();
            ViewBag.VehicleId = temp.RegistrationNum;
            ViewBag.cena = temp.Price;
            ViewBag.registracija = db.Vehicles.Where(v => v.Id == VehicleId);
            return View();
        }

        // POST: Deals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,VehicleId,CustomerId,UserId,DateFrom,DateTo,TotalPrice")] Deal deal)
        {
            String user = System.Web.HttpContext.Current.User.Identity.Name;
            User us = db.Users.Where(u => u.Email == user).First();
            deal.UserId = us.Id;
            Vehicle temp = db.Vehicles.Where(k => k.Id == deal.VehicleId).First();
            Deal deal1 = new Deal();
            deal.TotalPrice = temp.Price * (deal.DateTo.DayOfYear - deal.DateFrom.DayOfYear);
            deal1 = db.Deals.Where(d => d.VehicleId == temp.Id).FirstOrDefault();
            //var ivona = "ivona";
            if (ModelState.IsValid)
            {
                if(deal1 != null)
                {
                    int najdenoId = db.Deals.Where(v => v.VehicleId == temp.Id).First().VehicleId;
                    Vehicle najdenoVozilo = db.Vehicles.Where(ve => ve.Id == najdenoId).First();

                    //proverka dali datumite se sovpagaat

                    List<Deal> najdeniDeals = db.Deals.Where(de => de.VehicleId == najdenoId).ToList(); //najdeni deal

                    foreach(Deal d in najdeniDeals)
                    {
                        if(d.DateTo.DayOfYear > deal.DateFrom.DayOfYear)
                        {
                            Session["uspeh"] = "Резервацијата е неуспешна! Веќе постои резервација во овој период.";
                            Session["pecatiPoraka"] = "da";
                            ViewBag.uspesnost = "Резервацијата е неуспешна! Веќе постои резервација во овој период.";
                            return RedirectToAction("../Vehicles/Index");
                        }
                        else if (d.DateFrom.DayOfYear < DateTime.Now.DayOfYear)
                        {
                           
                            Session["pecatiPoraka"] = "da";
                            Session["uspeh"] = "Резервацијата е неуспешна! Не може да се креира резервација со датум помал од денешниот.";
                            return RedirectToAction("../Vehicle/Index");
                        }
                    }
                    

                }
                Session["pecatiPoraka"] = "ne";
                db.Deals.Add(deal);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "Email", deal.CustomerId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "UserId", deal.UserId);
            ViewBag.VehicleId = new SelectList(db.Vehicles, "Id", "VehicleId", deal.VehicleId);
        
            return View(deal);
        }

        // GET: Deals/Edit/5
        [Authorize(Roles = "Employee, Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Deal deal = db.Deals.Find(id);
            if (deal == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "Name", deal.CustomerId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", deal.UserId);
            ViewBag.VehicleId = new SelectList(db.Vehicles, "Id", "ImageUrl", deal.VehicleId);
            return View(deal);
        }

        // POST: Deals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,VehicleId,CustomerId,UserId,DateFrom,DateTo,TotalPrice")] Deal deal)
        {
            if (ModelState.IsValid)
            {
                db.Entry(deal).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "Name", deal.CustomerId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", deal.UserId);
            ViewBag.VehicleId = new SelectList(db.Vehicles, "Id", "ImageUrl", deal.VehicleId);
            return View(deal);
        }

        // GET: Deals/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Deal deal = db.Deals.Find(id);
            if (deal == null)
            {
                return HttpNotFound();
            }
            return View(deal);
        }

        // POST: Deals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Deal deal = db.Deals.Find(id);
            db.Deals.Remove(deal);
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



        public ActionResult Payment(string reg)
        {
            List<Vehicle> vehicles = db.Vehicles.ToList(); //site vozila
            Vehicle vehicle = vehicles.Where(v => v.RegistrationNum == reg).First(); //izbereno vozilo

            //Customer client = db.Customers.Where(u => u.Email == email).First(); //klient

            ViewBag.vozilo = vehicle;
           // ViewBag.klient = client;
            return View();
        }

        public ActionResult getDealsByMonth()
        {
            List<Deal> deals = db.Deals.ToList();
            List<Deal> jan = deals.Where(j => j.DateFrom.Month == 1).ToList();
            ViewBag.Jan = jan;
            return View(deals);
        }
    }
}
