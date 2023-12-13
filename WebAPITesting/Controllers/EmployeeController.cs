using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.ModelBinding;
using System.Web.UI.WebControls;
using WebAPITesting.Data;

namespace WebAPITesting.Controllers
{
    public class EmployeeController : ApiController
    {
        WebApi_DbContext dbContext = new WebApi_DbContext();

        [HttpGet]
        public IEnumerable<Employee> LoadAllEmployees()
        {
            return dbContext.Employees.ToList();
        }

        [HttpGet]
        public Employee LoadEmployeebyId(int id) 
        {
            
            return dbContext.Employees.FirstOrDefault(i=>i.ID == id);
        }

        public HttpResponseMessage POST([FromBody]Employee employee) 
        {
            try
            {
                dbContext.Employees.Add(employee);
                dbContext.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, employee);
            }
            catch (Exception ex) 
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
            
        }
        public HttpResponseMessage PUT(int id, [FromBody] Employee employee)
        {
            var entity = dbContext.Employees.FirstOrDefault(i => i.ID == id);
            if(entity!=null)
            {
                entity.FirstName = employee.FirstName;
                entity.LastName = employee.LastName;
                entity.Salary = employee.Salary;
                entity.Gender = employee.Gender;
                dbContext.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, entity);
            }
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest,"The data is not available to update");
        }
        public HttpResponseMessage DELETE(int id)
        {
            try
            {
                var entity = dbContext.Employees.FirstOrDefault(i => i.ID == id);
                dbContext.Employees.Remove(entity);
                dbContext.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK,entity);
            }
            catch(Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK, ex);
            }
            
        }

        //public HttpResponseMessage PATCH(int id,[FromBody] Employee employee)
        //{
        //    try
        //    {
        //        var entity = dbContext.Employees.FirstOrDefault(i => i.ID == id);
        //        if (entity != null)
        //        {
        //            entity.FirstName = employee.FirstName;
        //            entity.LastName = employee.LastName;
        //            entity.Salary = employee.Salary;
        //            entity.Gender = employee.Gender;
        //            dbContext.SaveChanges();
        //            return Request.CreateResponse(HttpStatusCode.OK, entity);
        //        }
        //        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "The data is not available to update");
        //    }
        //    catch (Exception ex) 
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
        //    }
        //}

        public IEnumerable<string> Get()
        {
            IList<string> forMatterList = new List<string>();
            foreach(var item in GlobalConfiguration.Configuration.Formatters)
            {
                forMatterList.Add(item.ToString());
            }
            return forMatterList;
        }

    }
}
