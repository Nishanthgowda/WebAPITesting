using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.UI.WebControls.WebParts;
using WebAPITesting.Models;

namespace WebAPITesting.Controllers
{
    // Attribute Routing
   [RoutePrefix("api/Students")]
    public class ValuesController : ApiController
    {
        static List<string> CourseList = new List<string>();
        static int count = 0;
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

        //making as optional parameter using [Route("id=1")]
        [Route("{id?}")]
        public Student Get(int id)
        {
            return students.FirstOrDefault(s => s.Id == id);
        }

        //adding routing constraints
        [Route("{id:int}/Courses",Name = "GetCourses")]
        public IEnumerable<string> GetStudentCourses(int id)
        {            
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

        public HttpResponseMessage PostCourses(IEnumerable<string> courses)
        {
            try
            {
                count++;
                CourseList.AddRange(courses);
                var response = Request.CreateResponse(HttpStatusCode.OK, courses);
                var res = Url.Link("GetCourses", new { CourseId = count});
                response.Headers.Location = new Uri(res);
                return response;
            }
            catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotModified, ex);
            }
        }

    }
}
