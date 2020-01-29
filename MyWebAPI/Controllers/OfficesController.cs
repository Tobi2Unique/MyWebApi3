using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MyWebAPI.Models;
//using OfficeDataAccess;

namespace MyWebAPI.Controllers
{
    public class OfficesController : ApiController
    {
        public IEnumerable<OfficeClass> Get()
        {
            using (WaecDirDbEntities db = new WaecDirDbEntities())
            {
                return db.OfficeClasses.ToList();
            }
        }

        public OfficeClass Get(int Id)
        {
            using (WaecDirDbEntities db = new WaecDirDbEntities())
            {
                return db.OfficeClasses.FirstOrDefault(o=>o.Id== Id);
            }
        }
    }
}
