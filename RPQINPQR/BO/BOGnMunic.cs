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
        public List<GnMunic> GetAllGnMunic(int pai_codi)
        {
           
                DAOGnMunic dao = new DAOGnMunic();
                List<GnMunic> result = new List<GnMunic>();
                result = dao.GetGnMunic(pai_codi);
                if (result == null || !result.Any())
                    throw new Exception("No se encontraron municipios parametrizados en seven -erp");
            return result;
          
        }
    }
}