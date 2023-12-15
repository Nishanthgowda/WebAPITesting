using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading;
using System.Web.Http;
using WebAPITesting.Data;
using WebAPITesting.Models;

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
        [BasicAuthenticationFilter]         //adding basic authentication at action level
        [CustomAuthorization(Roles = "Admin")]
        public HttpResponseMessage LoadEmployeesOnGenders()
        {
            var identity =(ClaimsIdentity) User.Identity;
            var email = identity.Claims.FirstOrDefault(i => i.Type == "Email").Value;
            var id = identity.Claims.FirstOrDefault(i => i.Type == "ID").Value;

            string UserName = identity.Name;
            //string UserName = Thread.CurrentPrincipal.Identity.Name;

            switch (UserName.ToLower())
            {
                case "maleusers":
                    return Request.CreateResponse(HttpStatusCode.OK, dbContext.Employees.Where(g => g.Gender == "Male"));
                    break;
                case "femaleusers":
                    return Request.CreateResponse(HttpStatusCode.OK, dbContext.Employees.Where(g => g.Gender == "Female"));
                    break;
                case "allusers":
                        return Request.CreateResponse(HttpStatusCode.OK, dbContext.Employees);
                    break;
                default: return Request.CreateResponse(HttpStatusCode.NoContent);
            }
        }


        [HttpGet]
        //[Route("LoadEmpbyId/{id}")]
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
                var response =  Request.CreateResponse(HttpStatusCode.Created);
                string uri = Url.Link("GetEmployeeById", new { EmployeeID = employee.ID });
                response.Headers.Location = new Uri(uri);
                return response;
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

        public HttpResponseMessage PATCH(int id, [FromBody] Employee employee)
        {
            try
            {
                var entity = dbContext.Employees.FirstOrDefault(i => i.ID == id);
                if (entity != null)
                {
                    entity.FirstName = employee.FirstName;
                    entity.LastName = employee.LastName;
                    entity.Salary = employee.Salary;
                    entity.Gender = employee.Gender;
                    dbContext.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "The data is not available to update");
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

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
