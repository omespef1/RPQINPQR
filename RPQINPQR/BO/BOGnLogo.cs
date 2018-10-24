using RPQINPQR.DAO;
using RPQINPQR.TO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace RPQINPQR.BO
{
    public class BOGnLogo
    {

        public string emp_codi;
        public BOGnLogo()
        {
            emp_codi = ConfigurationManager.AppSettings["emp_codi"];
        }

       public TOTransaction<Gnlogo> GetGnLogo()
        {
            DAOGnLogo dao = new DAOGnLogo();
            try
            {
                if (string.IsNullOrEmpty(emp_codi))
                    throw new Exception("Código de empresa no parametrizado en api");
                var result = dao.GetGnLogo(int.Parse(emp_codi));
                if (result == null)
                    throw new Exception("No se encontraron datos de la empresa");
                return new TOTransaction<Gnlogo>() { objTransaction = result, retorno = 0, txtRetorno = "" };
            }
            catch(Exception ex)
            {
                return new TOTransaction<Gnlogo>() { retorno = 1, txtRetorno = ex.Message,objTransaction=null };
            }
           
        }
    }
}