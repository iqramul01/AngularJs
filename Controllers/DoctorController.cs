using _1263087.Data;
using _1263087.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace 
    _1263087.Controllers
{
    [Route("Doctor")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private ApplicationDbContext _ctx = null;
        public DoctorController(ApplicationDbContext context)
        {
            _ctx = context;
        }

        // GET: api/Doctor
        [HttpGet, Route("GetDoctors")]
        public async Task<object> GetDoctor()
        {
            List<Doctor> doctor = null;
            try
            {
                using (_ctx)
                {
                    doctor = await _ctx.Doctors.ToListAsync();
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return doctor;
        }

        // GET: api/Doctor/5
        [HttpGet, Route("GetDoctor/{id}")]
        public async Task<Doctor> GetDoctor(int id)
        {
            Doctor doctor = null;
            try
            {
                using (_ctx)
                {
                    doctor = await _ctx.Doctors.FirstOrDefaultAsync(b => b.DoctorID == id);
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return doctor;
        }

        // PUT: api/Doctor/5
        [HttpPut, Route("UpdateDoctor")]
        public async Task<object> PutDoctor(Doctor doctor)
        {
            object result = null; string message = "";
            if (doctor == null)
            {
                return BadRequest();
            }
            using (_ctx)
            {
                try
                {
                    var entityUpdate = _ctx.Doctors.FirstOrDefault(x => x.DoctorID == doctor.DoctorID);
                    if (entityUpdate != null)
                    {
                        entityUpdate.DoctorName = doctor.DoctorName;
                        entityUpdate.ContactAddress = doctor.ContactAddress;
                        entityUpdate.EmailAddress = doctor.EmailAddress;
                        entityUpdate.JoiningDate = doctor.JoiningDate;
                        entityUpdate.Salary = doctor.Salary;
                        entityUpdate.IsActive = doctor.IsActive;
                        entityUpdate.DepartmentID = doctor.DepartmentID;


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

        // POST: api/Doctor
        [HttpPost, Route("AddDoctor")]
        public async Task<object> PostDoctor(Doctor doctor)
        {
            object result = null; string message = "";
            if (doctor == null)
            {
                return BadRequest();
            }
            using (_ctx)
            {
                _ctx.Doctors.Add(doctor);
                await _ctx.SaveChangesAsync();
                result = new
                {
                    message
                };
            }
            return result;
        }

        // DELETE: api/Doctor/5
        [HttpDelete, Route("DeleteDoctor")]
        public async Task<object> DeleteDoctor(Doctor doctor)
        {
            object result = null; string message = "";
            using (_ctx)
            {
                try
                {
                    var idToRemove = _ctx.Doctors.SingleOrDefault(x => x.DoctorID == doctor.DoctorID);
                    if (idToRemove != null)
                    {
                        _ctx.Doctors.Remove(idToRemove);
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

        private bool OrderBookExists(int? id)
        {
            return _ctx.Doctors.Any(e => e.DoctorID == id);
        }
    }
}