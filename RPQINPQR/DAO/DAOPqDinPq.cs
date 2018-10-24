using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Ophelia.Comun;
using Ophelia;
using Ophelia.DataBase;
using RPQINPQR.TO;
using System.Data;
using SevenFramework.DataBase;

namespace RPQINPQR.DAO
{
    public class DAOPqDinPq
    {
        public List<PqDinPq> getpqDinPq(int inp_cont,int emp_codi)
        {

            StringBuilder sql = new StringBuilder();
            sql.Append(" SELECT * FROM PQ_DINPQ  ");
            sql.Append(" WHERE INP_CONT=@INP_CONT AND EMP_CODI = @EMP_CODI");
            OTOContext PToContext = new OTOContext();
            List<SQLParams> parameters = new List<SQLParams>();
            parameters.Add(new SQLParams("INP_CONT", inp_cont));
            parameters.Add(new SQLParams("EMP_CODI", emp_codi));
            return new DbConnection().GetList<PqDinPq>(sql.ToString(), parameters);

        }
       
    }
}