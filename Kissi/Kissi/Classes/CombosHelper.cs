using Kissi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;

namespace Kissi.Classes
{
    public class CombosHelper:IDisposable
    {
        private static KissiContext db = new KissiContext();
        public static List<Department> GetDepartment()
        {
            var departments = db.Departments.ToList();
            departments.Add(new Department
            {
                DepartmentId=0,
                Name="[Select a department ...]",
            });
            return departments.OrderBy(c => c.Name).ToList();
        }
        public static List<City> GetCities(int departmentid)
        {
            var cities = db.Cities.Where(c=>c.DepartmentId==departmentid).ToList();
            cities.Add(new City
            {
                CityId = 0,
                Name = "[Select a city ...]",
            });
            return cities.OrderBy(c => c.Name).ToList();
        }
        public static List<Company> GetCompanies()
        {
            var companies = db.Companies.ToList();
            companies.Add(new Company
            {
                CompanyId = 0,
                Name = "[Select a company ...]",
            });
            return companies.OrderBy(c => c.Name).ToList();
        }

        public static List<Product> GetProducts(int companyId)
        {
            var product = db.Products.Where(c => c.CompanyId == companyId).ToList();
            product.Add(new Product
            {
                ProductId = 0,
                Description = "[Select a Product ...]",
            });
            return product.OrderBy(c => c.Description).ToList();
        }

        public void Dispose()
        {
           db.Dispose();
        }
        public static List<Product> GetProducts(int companyId, bool sw)
        {
            var products = db.Products.Where(p => p.CompanyId == companyId).ToList();
            return products.OrderBy(p => p.Description).ToList();
        }

        public static List<Customer> GetCustomers(int companyId)
        {
            var qry = (from cu in db.Customers
                       join cc in db.CompanyCustomers on cu.CustomerId equals cc.CustomerId
                       join co in db.Companies on cc.CompanyId equals co.CompanyId
                       where co.CompanyId == companyId
                       select new { cu }).ToList();
            var customers = new List<Customer>();
            foreach (var item in qry)
            {
                customers.Add(item.cu);
            }

            customers.Add(new Customer
            {
                CustomerId = 0,
                FirstName = "[Select a customer ...]",
            });
            return customers.OrderBy(c => c.FirstName).ThenBy(c=>c.LastName).ToList();
        }
    }
}