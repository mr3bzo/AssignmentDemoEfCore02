using System.Collections.Generic;
using System.Reflection.Emit;

namespace Assignment02Ef
{
    class Program
    {
        #region 1.Do CRUD operations on all tables (Use Previous Assignment)

        public interface IRepository<T> where T : class
        {
            IEnumerable<T> GetAll();
            T GetById(int id);
            void Add(T entity);
            void Update(T entity);
            void Delete(T entity);
            void SaveChanges();
        }

        public class Repository<T> : IRepository<T> where T : class
        {
            private readonly ITIContext _context;
            private readonly DbSet<T> _dbSet;

            public Repository(ITIContext context)
            {
                _context = context;
                _dbSet = context.Set<T>();
            }

            public IEnumerable<T> GetAll()
            {
                return _dbSet.ToList();
            }

            public T GetById(int id)
            {
                return _dbSet.Find(id);
            }

            public void Add(T entity)
            {
                _dbSet.Add(entity);
            }

            public void Update(T entity)
            {
                _context.Entry(entity).State = EntityState.Modified;
            }

            public void Delete(T entity)
            {
                _dbSet.Remove(entity);
            }

            public void SaveChanges()
            {
                _context.SaveChanges();
            }
        }



        public class StudentRepository : Repository<Student>
        {
            public StudentRepository(ITIContext context) : base(context) { }

        }

        public class DepartmentRepository : Repository<Department>
        {
            public DepartmentRepository(ITIContext context) : base(context) { }

        }


        public void CreateStudent(string fname, string lname, string address, int age, int depId)
        {
            using (var context = new ITIContext())
            {
                var studentRepo = new StudentRepository(context);

                var newStudent = new Student
                {
                    FName = fname,
                    LName = lname,
                    Address = address,
                    Age = age,
                    Dep_Id = depId
                };

                studentRepo.Add(newStudent);
                studentRepo.SaveChanges();
            }
        }

        public List<Student> GetAllStudents()
        {
            using (var context = new ITIContext())
            {
                var studentRepo = new StudentRepository(context);
                return studentRepo.GetAll().ToList();
            }
        }

        public void UpdateStudent(int id, string newAddress, int newAge)
        {
            using (var context = new ITIContext())
            {
                var studentRepo = new StudentRepository(context);
                var student = studentRepo.GetById(id);

                if (student != null)
                {
                    student.Address = newAddress;
                    student.Age = newAge;
                    studentRepo.Update(student);
                    studentRepo.SaveChanges();
                }
            }
        }

        public void DeleteStudent(int id)
        {
            using (var context = new ITIContext())
            {
                var studentRepo = new StudentRepository(context);
                var student = studentRepo.GetById(id);

                if (student != null)
                {
                    studentRepo.Delete(student);
                    studentRepo.SaveChanges();
                }
            }
        }
        #endregion

        #region 2.Create Relations Between Tables ( Use Previous Assignment )

        // Student class with relationships
        public class Student
        {
            public int ID { get; set; }
            public string FName { get; set; }
            public string LName { get; set; }
            public string Address { get; set; }
            public int Age { get; set; }
            public int Dep_Id { get; set; }

            public Department Department { get; set; }
            public ICollection<StudentCourse> StudentCourses { get; set; } = new List<StudentCourse>();
        }

        // Department class with relationships
        public class Department
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public int Ins_ID { get; set; }
            public DateTime HiringDate { get; set; }

            public Instructor Manager { get; set; }
            public ICollection<Student> Students { get; set; } = new List<Student>();
            public ICollection<Instructor> Instructors { get; set; } = new List<Instructor>();
        }

        // Course class with relationships
        public class Course
        {
            public int ID { get; set; }
            public int Duration { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public int Top_ID { get; set; }

            public Topic Topic { get; set; }
            public ICollection<StudentCourse> StudentCourses { get; set; } = new List<StudentCourse>();
            public ICollection<CourseInstructor> CourseInstructors { get; set; } = new List<CourseInstructor>();
        }

        // Instructor class with relationships
        public class Instructor
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public decimal Salary { get; set; }
            public string Address { get; set; }
            public decimal HourRate { get; set; }
            public decimal Bonus { get; set; }
            public int Dept_ID { get; set; }

            public Department Department { get; set; }
            public ICollection<Department> ManagedDepartments { get; set; } = new List<Department>();
            public ICollection<CourseInstructor> CourseInstructors { get; set; } = new List<CourseInstructor>();
        }

        // Topic class with relationships
        public class Topic
        {
            public int ID { get; set; }
            public string Name { get; set; }

            public ICollection<Course> Courses { get; set; } = new List<Course>();
        }

        // StudentCourse class with relationships
        public class StudentCourse
        {
            public int Stud_ID { get; set; }
            public int Course_ID { get; set; }
            public decimal Grade { get; set; }

            public Student Student { get; set; }
            public Course Course { get; set; }
        }

        // CourseInstructor class with relationships
        public class CourseInstructor
        {
            public int Inst_ID { get; set; }
            public int Course_ID { get; set; }
            public string Evaluate { get; set; }

            public Instructor Instructor { get; set; }
            public Course Course { get; set; }
        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Student relationships
            modelBuilder.Entity<Student>()
                .HasOne(s => s.Department)
                .WithMany(d => d.Students)
                .HasForeignKey(s => s.Dep_Id);

            // Department relationships
            modelBuilder.Entity<Department>()
                .HasOne(d => d.Manager)
                .WithMany(i => i.ManagedDepartments)
                .HasForeignKey(d => d.Ins_ID);

            // Course relationships
            modelBuilder.Entity<Course>()
                .HasOne(c => c.Topic)
                .WithMany(t => t.Courses)
                .HasForeignKey(c => c.Top_ID);

            // Instructor relationships
            modelBuilder.Entity<Instructor>()
                .HasOne(i => i.Department)
                .WithMany(d => d.Instructors)
                .HasForeignKey(i => i.Dept_ID);

            // StudentCourse relationships (many-to-many junction)
            modelBuilder.Entity<StudentCourse>()
                .HasKey(sc => new { sc.Stud_ID, sc.Course_ID });  // Composite primary key

            modelBuilder.Entity<StudentCourse>()
                .HasOne(sc => sc.Student)
                .WithMany(s => s.StudentCourses)
                .HasForeignKey(sc => sc.Stud_ID);

            modelBuilder.Entity<StudentCourse>()
                .HasOne(sc => sc.Course)
                .WithMany(c => c.StudentCourses)
                .HasForeignKey(sc => sc.Course_ID);

            // CourseInstructor relationships (many-to-many junction)
            modelBuilder.Entity<CourseInstructor>()
                .HasKey(ci => new { ci.Inst_ID, ci.Course_ID });  // Composite primary key

            modelBuilder.Entity<CourseInstructor>()
                .HasOne(ci => ci.Instructor)
                .WithMany(i => i.CourseInstructors)
                .HasForeignKey(ci => ci.Inst_ID);

            modelBuilder.Entity<CourseInstructor>()
                .HasOne(ci => ci.Course)
                .WithMany(c => c.CourseInstructors)
                .HasForeignKey(ci => ci.Course_ID);
        }

        #endregion
    }
}