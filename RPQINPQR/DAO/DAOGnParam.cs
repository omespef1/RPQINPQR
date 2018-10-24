using RPQINPQR.TO;
using SevenFramework.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace RPQINPQR.DAO
{
    public class DAOGnParam
    {
        public GnParam GetGnParam(int emp_codi)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT * FROM GN_PARAM WHERE EMP_CODI = @EMP_CODI");
            List<SQLParams> sqlParams = new List<SQLParams>();
            sqlParams.Add(new SQLParams("EMP_CODI", emp_codi));
            return new DbConnection().Get<GnParam>(sql.ToString(), sqlParams);

        }
    }
}