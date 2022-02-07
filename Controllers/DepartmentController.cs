using _1263087.Data;
using _1263087.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _1263087.Controllers
{

    [Route("Department")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private ApplicationDbContext _ctx = null;
        public DepartmentController(ApplicationDbContext context)
        {
            _ctx = context;
        }

        // GET: api/Values/GetBooks
        [HttpGet, Route("GetDepartments")]
        public async Task<object> GetDepartment()
        {
            List<Department> department = null;
            try
            {
                using (_ctx)
                {
                    department = await _ctx.Departments.ToListAsync();
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return department;
        }


        // GET api/Values/GetBookByID/5
        [HttpGet, Route("GetDepartment/{id}")]
        public async Task<Department> GetDepartment(int id)
        {
            Department department = null;
            try
            {
                using (_ctx)
                {
                    department = await _ctx.Departments.FirstOrDefaultAsync(b => b.DepartmentID == id);
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return department;
        }

        // 
        // POST api/Values/PostBook
        [HttpPost, Route("AddDepartment")]
        public async Task<object> AddDepartment(Department department)
        {
            object result = null; string message = "";
            if (department == null)
            {
                return BadRequest();
            }
            using (_ctx)
            {
                _ctx.Departments.Add(department);
                await _ctx.SaveChangesAsync();
                result = new
                {
                    message
                };
            }
            return result;
        }

        // PUT api/Values/PutBook/5
        [HttpPut, Route("UpdateDepartment")]
        public async Task<object> UpdateDepartment(Department department)
        {
            object result = null; string message = "";
            if (department == null)
            {
                return BadRequest();
            }
            using (_ctx)
            {
                try
                {
                    var entityUpdate = _ctx.Departments.FirstOrDefault(x => x.DepartmentID == department.DepartmentID);
                    if (entityUpdate != null)
                    {
                        entityUpdate.DepartmentName = department.DepartmentName;
                        entityUpdate.AvailableSeate = department.AvailableSeate;
                        entityUpdate.IsActive = department.IsActive;

                        await _ctx.SaveChangesAsync();
                    }
                    message = "Entry Updated";
                }
                catch (Exception e)
                {
                    e.ToString();
                    message = "Entry Update Failed!!";
                }
                result = new
                {
                    message
                };
            }
            return result;
        }

        // DELETE api/Values/DeleteBookByID/5
        [HttpDelete, Route("DeleteDepartment")]
        public async Task<object> DeleteDepartment(Department department)
        {
            object result = null; string message = "";
            using (_ctx)
            {
                try
                {
                    var idToRemove = _ctx.Departments.SingleOrDefault(x => x.DepartmentID == department.DepartmentID);
                    if (idToRemove != null)
                    {
                        _ctx.Departments.Remove(idToRemove);
                        await _ctx.SaveChangesAsync();
                    }
                    message = "Deleted Successfully";
                }
                catch (Exception e)
                {
                    e.ToString();
                    message = "Error on Deleting!!";
                }
                result = new
                {
                    message
                };
            }
            return result;
        }
    }
}