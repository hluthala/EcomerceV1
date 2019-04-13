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
        public static List<City> GetCities()
        {
            var cities = db.Cities.ToList();
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

        public static List<Customer> GetCustomers(int companyId)
        {
            var customer = db.Customers.Where(c=>c.CompanyId==companyId).ToList();
            customer.Add(new Customer
            {
                CustomerId = 0,
                FirstName = "[Select a customer ...]",
            });
            return customer.OrderBy(c => c.FirstName).ThenBy(c=>c.LastName).ToList();
        }
    }
}