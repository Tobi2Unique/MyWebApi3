using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using MyWebAPI.Models;

namespace MyWebAPI.Controllers
{
    [Authorize]
    public class OfficeClassesController : ApiController
    {
        private WaecDirDbEntities db = new WaecDirDbEntities();

        // GET: api/OfficeClasses
        public IQueryable<OfficeClass> GetOfficeClasses()
        {
            return db.OfficeClasses;
        }

        //[Route("api/OfficeClasses/{searchText:alpha}")]
        [Route("api/OfficeClasses/search/{searchText}")]
        public IQueryable<OfficeClass> GetOfficeClasses( string searchText)
        {
            return db.OfficeClasses.Where(c => c.OfficeName.ToLower().Contains(searchText) || c.Department.ToLower().Contains(searchText) || c.Division.ToLower().Contains(searchText) || c.Extension.ToLower().Contains(searchText));
        }

        // GET: api/OfficeClasses/5
        [Route("api/OfficeClasses/{id:int}")]
        [ResponseType(typeof(OfficeClass))]
        public async Task<IHttpActionResult> GetOfficeClass(int id)
        //public async Task<HttpResponseMessage> GetOfficeClass(int id)
        {
            OfficeClass officeClass = await db.OfficeClasses.FindAsync(id);
            if (officeClass == null)
            {
                //return Request.CreateErrorResponse(HttpStatusCode.NotFound, "The Office with Id " + id + " does not exist ");

                return Content(HttpStatusCode.NotFound, "The Office with Id " + id + " does not exist ");
                //return NotFound();
            }

            //return Request.CreateResponse(HttpStatusCode.OK, officeClass); 
            return Ok(officeClass);
        }

        // PUT: api/OfficeClasses/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutOfficeClass(int id, OfficeClass officeClass)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != officeClass.Id)
            {
                return BadRequest();
            }

            db.Entry(officeClass).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OfficeClassExists(id))
                {
                    return Content(HttpStatusCode.NotFound, "The Office with Id " + id + " does not exist ");
                    //return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/OfficeClasses
        [ResponseType(typeof(OfficeClass))]
        public async Task<IHttpActionResult> PostOfficeClass(OfficeClass officeClass)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.OfficeClasses.Add(officeClass);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = officeClass.Id }, officeClass);
        }

        // DELETE: api/OfficeClasses/5
        [ResponseType(typeof(OfficeClass))]
        public async Task<IHttpActionResult> DeleteOfficeClass(int id)
        {
            OfficeClass officeClass = await db.OfficeClasses.FindAsync(id);
            if (officeClass == null)
            {                
                return NotFound();
            }

            db.OfficeClasses.Remove(officeClass);
            await db.SaveChangesAsync();

            return Ok(officeClass);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OfficeClassExists(int id)
        {
            return db.OfficeClasses.Count(e => e.Id == id) > 0;
        }
    }
}