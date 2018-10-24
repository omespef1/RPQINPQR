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
    public class PqInpqrController : ApiController
    {
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        // GET: api/PqInpqr
        public TOTransaction<PqInpqr> Get(int inp_cont, string inp_pass)
        {
            BOPqInpqr boPqr = new BOPqInpqr();
            return boPqr.GetInfoPqrGenerated(inp_cont, inp_pass);
        }

        // GET: api/PqInpqr/5
        public string Get(int id)
        {
            return "value";
        }

        /// <summary>
        /// Inserta una nueva PQR en seven - erp
        /// </summary>
        /// <param name="pqr"></param>
        /// <returns></returns>
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public TOTransaction<PqInpqrSalida> Post(PqInpqr pqr)
        {
            BOPqInpqr bo = new BOPqInpqr();
            return bo.PostPqr(pqr);
        }

        // PUT: api/PqInpqr/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/PqInpqr/5
        public void Delete(int id)
        {
        }
    }
}
