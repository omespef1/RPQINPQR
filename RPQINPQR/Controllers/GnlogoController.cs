using RPQINPQR.BO;
using RPQINPQR.TO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace RPQINPQR.Controllers
{
    public class GnlogoController : ApiController
    {
        // GET: api/Gnlogo
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public TOTransaction<Gnlogo> Get()
        {
            BOGnLogo bo = new BOGnLogo();
            return bo.GetGnLogo();
        }

        // GET: api/Gnlogo/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Gnlogo
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Gnlogo/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Gnlogo/5
        public void Delete(int id)
        {
        }
    }
}
