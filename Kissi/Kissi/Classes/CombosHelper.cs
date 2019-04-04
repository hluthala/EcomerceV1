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
                Name="[Select department ...]",
            });
            return departments.OrderBy(c => c.Name).ToList();
        }

        public void Dispose()
        {
           db.Dispose();
        }
    }
}