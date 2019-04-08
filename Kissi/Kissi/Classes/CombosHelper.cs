using Kissi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
        public void Dispose()
        {
           db.Dispose();
        }
    }
}