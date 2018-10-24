using Ophelia;
using Ophelia.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Configuration;
namespace RPQINPQR.DAO
{
    public class DAOGnConse
    {
        public int emp_codi;
        public DAOGnConse()
        {
            emp_codi = ConfigurationManager.AppSettings["emp_codi"].AsInt();
        }
        public int getGnConse(double con_codi)
        {           
            StringBuilder sql = new StringBuilder();
            sql.Append(" SELECT MAX(RAD_CONT) ");
            sql.Append(" FROM   GN_RADJU ");
            sql.Append(" WHERE  EMP_CODI  = @EMP_CODI  ");
            List<Parameter> parametros = new List<Parameter>();
            parametros.Add(new Parameter("@EMP_CODI ", emp_codi));           
            OTOContext pTOContext = new OTOContext();
            var conection = DBFactory.GetDB(pTOContext);
            var retorno=  conection.GetScalar(pTOContext, sql.ToString(), parametros.ToArray());
           return int.Parse(retorno.ToString());
        }

        public int updateGnConse (int con_codi, double con_disp)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("  UPDATE GN_CONSE ");
            sql.Append("   SET   CON_DISP = @CON_DISP  	 ");
            sql.Append("   WHERE EMP_CODI = @EMP_CODI AND CON_CODI= 325 ");
            List<Parameter> parametros = new List<Parameter>();
            parametros.Add(new Parameter("@EMP_CODI ", emp_codi));
            parametros.Add(new Parameter("@CON_DISP", con_disp));
            parametros.Add(new Parameter("@CON_CODI", con_codi));
            OTOContext pTOContext = new OTOContext();
            var conection = DBFactory.GetDB(pTOContext);
            var retorno = conection.Update(pTOContext, sql.ToString(), parametros.ToArray());
            return retorno;
        }
    }
}