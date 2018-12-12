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
        public List<GnDepar> GetGnDepar(int pai_codi)
        {
           
                DAOGnDepar dao = new DAOGnDepar();
                List<GnDepar> result = new List<GnDepar>();
                result = dao.GetGnDepar(pai_codi);
                if (result == null || !result.Any())
                    throw new Exception("No se encontraron departamentos parametrizados en Seven-Erp");
            return result;
         
          
        }
    }
}