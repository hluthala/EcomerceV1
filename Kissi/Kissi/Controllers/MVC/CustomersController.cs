﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Kissi.Models;
using Kissi.Classes;

namespace Kissi.Controllers
{
    [Authorize(Roles = "User")]
    public class CustomersController : Controller
    {
        private KissiContext db = new KissiContext();

        // GET: Customers
        public ActionResult Index()
        {
            var user = db.Users.Where(c => c.UserName == User.Identity.Name).FirstOrDefault();

            var qry = (from cu in db.Customers
                       join cc in db.CompanyCustomers on cu.CustomerId equals cc.CustomerId
                       join co in db.Companies on cc.CompanyId equals co.CompanyId
                       where co.CompanyId == user.CompanyId
                       select new { cu }).ToList();

            var customers = new List<Customer>();
            foreach (var item in qry)
            {
                customers.Add(item.cu);
            }

            return View(customers.ToList());
        }

        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            var user = db.Users.Where(c => c.UserName == User.Identity.Name).FirstOrDefault();

            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }
            //var customer = new Customer { CompanyId = user.CompanyId, };
            ViewBag.CityId = new SelectList(CombosHelper.GetCities(0), "CityId", "Name");
            //ViewBag.CompanyId = new SelectList(db.Companies, "CompanyId", "Name");
            ViewBag.DepartmentId = new SelectList(CombosHelper.GetDepartment(), "DepartmentId", "Name");
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
            
                using (var transaccion = db.Database.BeginTransaction())
                {
                    try
                    {
                        db.Customers.Add(customer);
                        db.SaveChanges();

                        var user = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
                        var companyCustomer = new CompanyCustomer
                        {
                            CompanyId = user.CompanyId,
                            CustomerId = customer.CustomerId,
                        };

                        db.CompanyCustomers.Add(companyCustomer);
                        db.SaveChanges();

                        UsersHelper.CreateUserASP(customer.UserName, "Customer");
                        transaccion.Commit();
                        return RedirectToAction("Index");
                    }
                    catch (Exception ex)
                    {
                        transaccion.Rollback();
                        ModelState.AddModelError(string.Empty, ex.Message);
                    }
                }

            }

            ViewBag.CityId = new SelectList(CombosHelper.GetCities(customer.DepartmentId), "CityId", "Name", customer.CityId);
            //ViewBag.CompanyId = new SelectList(db.Companies, "CompanyId", "Name", customer.CompanyId);
            ViewBag.DepartmentId = new SelectList(CombosHelper.GetDepartment(), "DepartmentId", "Name", customer.DepartmentId);
            return View(customer);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }

            ViewBag.CityId = new SelectList(CombosHelper.GetCities(customer.DepartmentId), "CityId", "Name", customer.CityId);
            //ViewBag.CompanyId = new SelectList(db.Companies, "CompanyId", "Name", customer.CompanyId);
            ViewBag.DepartmentId = new SelectList(CombosHelper.GetDepartment(), "DepartmentId", "Name", customer.DepartmentId);
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Customer customer)
        {
            if (ModelState.IsValid)
            {
                var db2 = new KissiContext();
                var currentUser = db2.Customers.Find(customer.CustomerId);
                if (currentUser.UserName != customer.UserName)
                {
                    UsersHelper.UpdateUserName(currentUser.UserName, customer.UserName);
                }
                db2.Dispose();
                db.Entry(customer).State = EntityState.Modified;
                var response = DBHelper.SaveChanges(db);
                if (response.Succeeded)
                {
                    //UsersHelper.CreateUserASP(customer.UserName, "Customer");
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError(string.Empty, response.Message);
                //return RedirectToAction("Index");
            }
            ViewBag.CityId = new SelectList(CombosHelper.GetCities(customer.DepartmentId), "CityId", "Name", customer.CityId);
            //ViewBag.CompanyId = new SelectList(db.Companies, "CompanyId", "Name", customer.CompanyId);
            ViewBag.DepartmentId = new SelectList(CombosHelper.GetDepartment(), "DepartmentId", "Name", customer.DepartmentId);
            return View(customer);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var user = db.Users.Where(c => c.UserName == User.Identity.Name).FirstOrDefault();

            Customer customer = db.Customers.Find(id);
          
            var companycustomer = db.CompanyCustomers.Where(c => c.CompanyId == user.CompanyId && c.CustomerId == customer.CustomerId).FirstOrDefault();
            using (var transaction =db.Database.BeginTransaction())
            {
                db.CompanyCustomers.Remove(companycustomer);
                db.Customers.Remove(customer);
                var response = DBHelper.SaveChanges(db);
                if (response.Succeeded)
                {
                    //UsersHelper.CreateUserASP(customer.UserName, "Customer");
                    transaction.Commit();
                    //UsersHelper.DeleteUser(customer.UserName, "Customer");
                    return RedirectToAction("Index");

                }
                ModelState.AddModelError(string.Empty, response.Message);
                return View(customer);
            }

          
        }
        //public JsonResult Getcities(int departmentId)
        //{
        //    db.Configuration.ProxyCreationEnabled = false;
        //    var cities = db.Cities.Where(m => m.DepartmentId == departmentId);
        //    return Json(cities);
        //}
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
