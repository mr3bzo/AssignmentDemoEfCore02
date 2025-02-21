using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment02EfCore.Entities
{
    internal class Emplyees
    {
        public int Id { get; set; } // PK -> EmployeeId Identity(1,1) 
        public string Name { get; set; } // nvarchar(max) 
        public double? Salary { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }

        //public Department? Department { get; set;}

        //[InverseProperty (nameof(Department. Manager))] 
        //public Department Department { get; set; } 

        public int WorkForId { get; set; }
        public Department? WorkFor { get; set; }


    }
}
