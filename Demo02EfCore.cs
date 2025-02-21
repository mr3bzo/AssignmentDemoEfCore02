using System.Net;
using System.Xml.Linq;
using Assignment02EfCore.Entities;

namespace demo02EfCore
{
    class Program
    {

        // CRUD Operation 
        // Create Read - Update Delete 

        //        AppDbContext context = new AppDbContext(); 
        //        try 
        //        { 
        //        // Code 
        //        } 
        //        finally 
        //        { 
        //        context.Dispose(); 
        //        }


        //    //context.Dispose(); 

        ////
        ////using (AppDbContext context = new AppDbContext()) 
        ////
        //// CRUD 
        //}

        //using AppDbContext context = new AppDbContext();

        #region CRUD Operation
        #region Insert Ctreate
        //        var employee = new Employee()
        //        {
        //            Name "Ahmed Ali",
        //            Salary 12000,
        //            Address = "Cairo",
        //            Age = 25
        //        };


        //        Console.WriteLine(context.Entry (employee). State); // Detached 
        //        employee.Name = "Khaled"; 

        //        Console.WriteLine(context.Entry(employee). State); // Detached 
        //        //context.Add(employee); 
        // context.Employees.Add(employee); 

        //Console.WriteLine(context.Entry(employee). State); // Added 

        //var Result = context.SaveChanges();

        //        Console.WriteLine(context.Entry(employee). State); // Unchanged 

        //employee.Name = "Omar"; 

        //Console.WriteLine(context.Entry(employee). State); // Modified
        //            //Console.WriteLine(Result); 
        ////context.Employees.Add(employee); 
        ////context.SaveChanges(); 

        //Console.WriteLine(context.Entry(employee).State); //
        //        context.Entry(employee). State EntityState.Added;
        //        Console.WriteLine(context.Entry (employee).State); //
        //context.SaveChanges(); 
        #endregion

        #region Read Select 

        // Read Select 
        //var Result = context.Employees. Where (EE.Id=48).FirstOrDefault(
        //); 
        var Result = context.Employees.FirstOrDefault(E => E.Id == 48);

        //var Result = context. Employees.Select(E => E.Name); 

        Console.WriteLine(context.Entry(Result). State); // Unchanged 

            Result.Name = "Ali"; 

            Console.WriteLine(context.Entry (Result). State); // Modified

//foreach (var item in Result) 
//{
//Console.WriteLine(item);
//}

//Console.WriteLine(Result?.Name); 
	#endregion

           #region Update
		 var Result = context.Employees.FirstOrDefault(E => E.Id 40);

        Console.WriteLine(context.Entry (Result).State); 
            
            Result.Name = "Omar Mohamed"; 
            //Console.WriteLine(context.Entry(Result). State); 
            
            //context.Update (Result); 
            Console.WriteLine(context.Entry (Result).State); 
            
            context.SaveChanges(); 
            Console.WriteLine(context.Entry (Result).State); 
	#endregion

            #region Delete
		// Delete 
var Result context.Employees.FirstOrDefault(EE.Id = 30); 

Console.WriteLine(context.Entry (Result). State); // Unchanged 

context.Employees.Remove(Result); 
Console.WriteLine(context.Entry (Result). State); // Deleted 

context.SaveChanges(); 
Console.WriteLine(context.Entry (Result). State); // Deleted 
	#endregion

	#endregion
            Employee employee new Employee();
        Department department = new Department();
        //employee.WorkFor 
        //department.Manager.



    }
}