using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPITesting.Models;

namespace WebAPITesting.Controllers
{
    // Attribute Routing
   [RoutePrefix("api/Students")]
    public class ValuesController : ApiController
    {
        static List<Student> students = new List<Student>()
        {
            new Student() { Id = 1, Name = "Pranaya" },
            new Student() { Id = 2, Name = "Priyanka" },
            new Student() { Id = 3, Name = "Anurag" },
            new Student() { Id = 4, Name = "Sambit" }
        };

        [Route("")]
        public IEnumerable<Student> GET()
        {
            return students;
        }

        [Route("{id}")]
        public Student Get(int id)
        {
            return students.FirstOrDefault(s => s.Id == id);
        }

        [Route("{id}/Courses")]
        public IEnumerable<string> GetStudentCourses(int id)
        {
            List<string> CourseList = new List<string>();
            if (id == 1)
                CourseList = new List<string>() { "ASP.NET", "C#.NET", "SQL Server" };
            else if (id == 2)
                CourseList = new List<string>() { "ASP.NET MVC", "C#.NET", "ADO.NET" };
            else if (id == 3)
                CourseList = new List<string>() { "ASP.NET WEB API", "C#.NET", "Entity Framework" };
            else
                CourseList = new List<string>() { "Bootstrap", "jQuery", "AngularJs" };
            return CourseList;
        }

    }
}
