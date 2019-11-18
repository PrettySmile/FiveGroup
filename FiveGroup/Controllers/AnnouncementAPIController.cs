using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FiveGroup.Models;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;

namespace FiveGroup.Controllers
{
    public class AnnouncementAPIController : ApiController
    {
        private Project2Entities db = new Project2Entities();
        // GET api/<controller>
        public IEnumerable<announcement> GET()
        {
            return db.announcement.ToList();
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}