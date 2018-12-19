using RPQINPQR.BO;
using RPQINPQR.DAO;
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
    public class GnArbolController : ApiController
    {
        /// <summary>
        /// Consulta arboles en seven erp
        /// </summary>
        /// <param name="tar_codi"></param>
        /// <param name="arb_codi"></param>
        /// <returns></returns>
         [EnableCors(origins: "*", headers: "*", methods: "*")]
        public TOTransaction<List<GnArbol>>  Get(string tar_codi,string arb_codi)
        {
            BOGnArbol bo = new BOGnArbol();
            return bo.GetGbnArbol(tar_codi, arb_codi);
        }

        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public TOTransaction<GnArbol> Get(int con_cont)
        {
            DAOCtContr dao = new DAOCtContr();
            try
            {
               var retorno =  dao.GetCtDcont(con_cont);
                return new TOTransaction<GnArbol>() { objTransaction = retorno, retorno = 0, txtRetorno = "" };
            }
            catch(Exception ex)
            {
                return new TOTransaction<GnArbol>() { objTransaction = null, retorno = 1, txtRetorno = ex.Message };
            }
           
        }
       
    }
}
