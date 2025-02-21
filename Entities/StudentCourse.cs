using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment02EfCore.Entities
{
    internal class StudentCourse
    {
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public double Grade { get; set; }

        public List<Student> Students { get; set; }
        public List<Course> Courses { get; set; }
        public double Grade { get; set; }
    }
}
