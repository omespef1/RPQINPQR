using RPQINPQR.DAO;
using RPQINPQR.TO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace RPQINPQR.BO
{
    public class BOGnArbol
    {
        string emp_codi = ConfigurationManager.AppSettings["emp_codi"];
        public TOTransaction<List<GnArbol>> GetGbnArbol(string tar_codi,string arb_codi)
        {
            try
            {
                if (string.IsNullOrEmpty(emp_codi))
                    throw new Exception("Código de empresa (emp_codi) no definido en configuración de Api");
                DAOGnArbol dao = new DAOGnArbol();
                List<GnArbol> result = new List<GnArbol>();
                result = dao.GetGnArbol(tar_codi, arb_codi, int.Parse(emp_codi));
                if (result == null || !result.Any())
                    throw new Exception(string.Format("No se encontraron arboles parametrizados para tar_codi {0} y arb_codi {1}",tar_codi,arb_codi));
                return new TOTransaction<List<GnArbol>>() { objTransaction = result, retorno = 0, txtRetorno = "" };
            }
            catch(Exception ex)
            {
                return new TOTransaction<List<GnArbol>>() { objTransaction = null, retorno = 1, txtRetorno = ex.Message };
            }
        }
    }
}