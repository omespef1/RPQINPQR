using Ophelia;
using Ophelia.Comun;
using Ophelia.DataBase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using RPQINPQR.TO;


namespace RPQINPQR.DAO
{
    public class DAOGnLogo
    {
        public Gnlogo GetGnLogo(int emp_codi)
        {
            List<Parameter> parametros = new List<Parameter>();
            StringBuilder sql = new StringBuilder();
            sql.Append(" SELECT EMP_LOGS ");
            sql.Append(" FROM GN_EMPRE ");
            sql.Append(" WHERE EMP_CODI= @emp_codi");
            parametros.Add(new Parameter("@emp_codi", emp_codi));
            
            OTOContext pTOContext = new OTOContext();
            var conection = DBFactory.GetDB(pTOContext);
            Gnlogo data = conection.Read(pTOContext, sql.ToString(), Make, parametros.ToArray());            
            return data;

        }
        public Func<IDataReader, Gnlogo> Make = reader => new Gnlogo
        {
            emp_logs = reader["EMP_LOGS"] as byte[]
            
        };
    }
}