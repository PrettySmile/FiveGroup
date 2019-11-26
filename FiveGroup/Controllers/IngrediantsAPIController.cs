using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FiveGroup.Models;

namespace FiveGroup.Controllers
{
    public class IngrediantsAPIController : ApiController
    {
        private Project2Entities db = new Project2Entities();
        // GET api/<controller>
        public IEnumerable<ingrediant> GET()
        {
            return db.ingrediant.ToList();
        }

    }
}