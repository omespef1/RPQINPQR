using RPQINPQR.DAO;
using RPQINPQR.TO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RPQINPQR.BO
{
    public class BOGnPaise
    {
       public List<GnPaise> GetGnPaise()
        {
           
                DAOGnPaise dao = new DAOGnPaise();
                List<GnPaise> result = new List<GnPaise>();
                result = dao.GetGnPaise();
                if (result == null || !result.Any())
                    throw new Exception("No se encontraron países parametrizados en Seven-Erp");
            return result;
            
            
        }
    }
}