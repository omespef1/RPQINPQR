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
    public class GnMunicController : ApiController
    {
        /// <summary>
        /// Retorna todos los municipios existentes
        /// </summary>
        /// <returns></returns>
        public TOTransaction<List<GnMunic>> Get(int pai_codi)
        {
            BOGnMunic bo = new BOGnMunic();
            return bo.GetAllGnMunic(pai_codi);
        }

      
     

       

       
    }
}
