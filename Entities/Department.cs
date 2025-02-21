using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment02EfCore.Entities
{
    internal class Department
    {
        public int Id { get; set; }
        public string DeptName { get; set; }
        //[ForeignKey("Manager")] 
        //[Foreignkey(nameof (Department.Manager))]
        public Employee Manager { get; set; } // Navigational Property
        public List<Employee> Employees { get; set; }
    }
}
