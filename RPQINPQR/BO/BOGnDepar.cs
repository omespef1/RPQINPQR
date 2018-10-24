using RPQINPQR.DAO;
using RPQINPQR.TO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RPQINPQR.BO
{
    public class BOGnDepar
    {
        public TOTransaction<List<GnDepar>> GetGnDepar(int pai_codi)
        {
            try
            {
                DAOGnDepar dao = new DAOGnDepar();
                List<GnDepar> result = new List<GnDepar>();
                result = dao.GetGnDepar(pai_codi);
                if (result == null || !result.Any())
                    throw new Exception("No se encontraron departamentos parametrizados en Seven-Erp");
                return new TOTransaction<List<GnDepar>>() { objTransaction = result, retorno = 0, txtRetorno = "" };
            }
            catch(Exception ex)
            {
                return new TOTransaction<List<GnDepar>>() { objTransaction = null, retorno = 1, txtRetorno = ex.Message };
            }
        }
    }
}