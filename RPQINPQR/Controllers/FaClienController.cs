using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RPQINPQR.Controllers
{
    public class FaClienController : ApiController
    {
        // GET: api/FaClien
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/FaClien/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/FaClien
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/FaClien/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/FaClien/5
        public void Delete(int id)
        {
        }
    }
}
