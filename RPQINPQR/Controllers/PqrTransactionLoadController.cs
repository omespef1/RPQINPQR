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
    public class PqrTransactionLoadController : ApiController
    {

        [EnableCors(origins: "*", headers: "*", methods: "*")]
        // GET: api/PqrTransactionLoad
        public TOTransaction<PqrTransactionLoad> Get(string cli_coda=null)
        {
            BOPqInpqr bo = new BOPqInpqr();
            return bo.GetInitialDataWpqinqr(cli_coda);
        }

        // GET: api/PqrTransactionLoad/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/PqrTransactionLoad
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/PqrTransactionLoad/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/PqrTransactionLoad/5
        public void Delete(int id)
        {
        }
    }
}
