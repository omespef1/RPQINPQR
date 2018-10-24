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
       public TOTransaction<List<GnPaise>> GetGnPaise()
        {
            try
            {
                DAOGnPaise dao = new DAOGnPaise();
                List<GnPaise> result = new List<GnPaise>();
                result = dao.GetGnPaise();
                if (result == null || !result.Any())
                    throw new Exception("No se encontraron países parametrizados en Seven-Erp");
                return new TOTransaction<List<GnPaise>>() { objTransaction = result, retorno = 0, txtRetorno = "" };
            }
            catch(Exception ex)
            {
                return new TOTransaction<List<GnPaise>>() { objTransaction = null, txtRetorno = ex.Message, retorno = 1 };
            }
        }
    }
}