using RPQINPQR.BO;
using RPQINPQR.TO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RPQINPQR.Controllers
{
    public class GnArbolController : ApiController
    {
        /// <summary>
        /// Consulta arboles en seven erp
        /// </summary>
        /// <param name="tar_codi"></param>
        /// <param name="arb_codi"></param>
        /// <returns></returns>
        public TOTransaction<List<GnArbol>>  Get(string tar_codi,string arb_codi)
        {
            BOGnArbol bo = new BOGnArbol();
            return bo.GetGbnArbol(tar_codi, arb_codi);
        }

       
    }
}
