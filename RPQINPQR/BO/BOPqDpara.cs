using RPQINPQR.DAO;
using RPQINPQR.TO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace RPQINPQR.BO
{
    public class BOPqDpara
    {
        string emp_codi = ConfigurationManager.AppSettings["emp_codi"];
        public List<TOGPerte> GetPqDpara()
        {
           
                if (string.IsNullOrEmpty(emp_codi))
                    throw new Exception("Código de empresa (emp_codi) no definido en api");
                DAOGPerte daoPerte = new DAOGPerte();
                List<TOGPerte> result = new List<TOGPerte>();
                result = daoPerte.GetGrupoPerteneciente(int.Parse(emp_codi));
                if (result == null || !result.Any())
                    throw new Exception("Grupos no definidos en pqdpara");
            return result;
          
        }
    }
}