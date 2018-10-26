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
    public class PqEncueController : ApiController
    {
      
        BOPqEncue boEncue = new BOPqEncue();
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public TOTransaction Post(List<PQEncue> encuesta)
        {
           
             return boEncue.SetPqEncue(encuesta);
        }
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public TOTransaction Get(int inp_cont)
        {
            return boEncue.GetPqncue(inp_cont);
        }
    }
}
