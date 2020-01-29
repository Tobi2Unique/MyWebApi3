using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using MyWebAPI.Models;

namespace MyWebAPI.Controllers
{
    public class OfficeController : ApiController
    {
        private WaecDirDbEntities db = new WaecDirDbEntities();

        // GET: api/Office
        public IQueryable<OfficeClass> GetOfficeClasses()
        {
            return db.OfficeClasses;
        }

        //[Route("api/office/{searchText:alpha}")]
        [Route("api/office/search/{searchText}")]
        public IQueryable<OfficeClass> GetOfficeBySearch(string searchText)
        {
            //searchText =  searchText.ToLower().Substring(1);
            return  db.OfficeClasses.Where(c => c.OfficeName.ToLower().Contains(searchText) || c.Department.ToLower().Contains(searchText) || c.Division.ToLower().Contains(searchText) || c.Extension.ToLower().Contains(searchText));
        }

        // GET: api/Office/5
        [Route("api/office/{id:int}")]
        [ResponseType(typeof(OfficeClass))]
        public IHttpActionResult GetOfficeClass(int id)
        {
            OfficeClass officeClass = db.OfficeClasses.Find(id);
            if (officeClass == null)
            {
                return NotFound();
            }

            return Ok(officeClass);
        }

        

        // PUT: api/Office/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutOfficeClass(int id, OfficeClass officeClass)
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
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OfficeClassExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Office
        [ResponseType(typeof(OfficeClass))]
        public IHttpActionResult PostOfficeClass(OfficeClass officeClass)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.OfficeClasses.Add(officeClass);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = officeClass.Id }, officeClass);
        }

        // DELETE: api/Office/5
        [ResponseType(typeof(OfficeClass))]
        public IHttpActionResult DeleteOfficeClass(int id)
        {
            OfficeClass officeClass = db.OfficeClasses.Find(id);
            if (officeClass == null)
            {
                return NotFound();
            }

            db.OfficeClasses.Remove(officeClass);
            db.SaveChanges();

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