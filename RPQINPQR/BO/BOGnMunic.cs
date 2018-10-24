using RPQINPQR.DAO;
using RPQINPQR.TO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RPQINPQR.BO
{
    public class BOGnMunic
    {
        public TOTransaction<List<GnMunic>> GetAllGnMunic(int pai_codi)
        {
            try
            {
                DAOGnMunic dao = new DAOGnMunic();
                List<GnMunic> result = new List<GnMunic>();
                result = dao.GetGnMunic(pai_codi);
                if (result == null || !result.Any())
                    throw new Exception("No se encontraron municipios parametrizados en seven -erp");
                return new TOTransaction<List<GnMunic>>() { objTransaction = result, txtRetorno = "", retorno = 0 };
            }
            catch(Exception ex)
            {
                return new TOTransaction<List<GnMunic>>() { objTransaction = null, retorno = 1, txtRetorno = ex.Message };
            }
        }
    }
}