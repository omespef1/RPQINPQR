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
    public class GnItemsController : ApiController
    {
        /// <summary>
        /// Retorna una lista de items para el titem especificado
        /// </summary>
        /// <param name="tit_cont"></param>
        /// <param name="ite_codi"></param>
        /// <returns></returns>
        
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public TOTransaction<List<GnItem>> Get(int tit_cont)
        {
            BOGnItems bo = new BOGnItems();
            return bo.GetGnItems(tit_cont, "");
        }

       
    }
}
