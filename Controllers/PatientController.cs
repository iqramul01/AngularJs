using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _1263087.Data;
using _1263087.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace _1263087.Controllers
{
    [Route("Patient")]
    [ApiController]

    public class PatientController : ControllerBase
    {
        //private ApplicationDbContext db = new ApplicationDbContext();
        private ApplicationDbContext db = null;
        public PatientController(ApplicationDbContext context)
        {
            db = context;
        }

        // GET: api/Patient
        //public IQueryable<Patient> GetPatients()
        //{
        //    return db.Patients;
        //}

        // GET: api/Values/GetBooks
        [HttpGet, Route("GetPatients")]
        public async Task<object> GetPatient()
        {
            List<Patient> patient = null;
            try
            {
                using (db)
                {
                    patient = await db.Patients.ToListAsync();
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return patient;
        }

        // GET api/Values/GetBookByID/5
        [HttpGet, Route("GetPatient/{id}")]
        public async Task<Patient> GetPatient(int id)
        {
            Patient patient = null;
            try
            {
                using (db)
                {
                    patient = await db.Patients.FirstOrDefaultAsync(b => b.ID == id);
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return patient;
        }

        // POST api/Values/PostBook
        [HttpPost, Route("GetPatients")]
        public async Task<object> AddPatient(Patient patient)
        {
            object result = null; string message = "";
            if (patient == null)
            {
                return BadRequest();
            }
            using (db)
            {
                db.Patients.Add(patient);
                await db.SaveChangesAsync();
                result = new
                {
                    message
                };
            }
            return result;
        }

        // PUT api/Values/PutBook/5
        [HttpPut, Route("GetPatients")]
        public async Task<object> UpdatePatient(Patient patient)
        {
            object result = null; string message = "";
            if (patient == null)
            {
                return BadRequest();
            }
            using (db)
            {
                try
                {
                    var entityUpdate = db.Patients.FirstOrDefault(x => x.ID == patient.ID);
                    if (entityUpdate != null)
                    {
                        entityUpdate.Name = patient.Name;
                        entityUpdate.Address = patient.Address;
                        entityUpdate.Date = patient.Date;

                        entityUpdate.CellPhone = patient.CellPhone;


                        await db.SaveChangesAsync();
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
        [HttpDelete, Route("/{id}")]
        public async Task<object> DeletePatient(Patient patient)
        {
            object result = null; string message = "";
            using (db)
            {
                try
                {
                    var idToRemove = db.Patients.SingleOrDefault(x => x.ID == patient.ID);
                    if (idToRemove != null)
                    {
                        db.Patients.Remove(idToRemove);
                        await db.SaveChangesAsync();
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