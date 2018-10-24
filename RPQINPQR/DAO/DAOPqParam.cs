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
using System.Configuration;

namespace RPQINPQR.DAO
{
    public class DAOPqParam
    {
        public string emp_codi = ConfigurationManager.AppSettings["emp_codi"].ToString();

        public TOPqParam GetMailParam()
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("  SELECT  PAR_URLE,PAR_MCIN,PAR_TCIN,PAR_MCTR,PAR_TCTR,PAR_MCCR,PAR_TCCR FROM PQ_PARAM,GN_TERCE ");
            sql.Append("  WHERE PQ_PARAM.EMP_CODI = GN_TERCE.EMP_CODI AND PQ_PARAM.TER_CODI = GN_TERCE.TER_CODI ");
            sql.Append("  AND PQ_PARAM.EMP_CODI= @EMP_CODI ");
            List<Parameter> parameters = new List<Parameter>();
            parameters.Add(new Parameter("@EMP_CODI", emp_codi));
            OTOContext pTOContext = new OTOContext();
            var conection = DBFactory.GetDB(pTOContext);
            var data = conection.Read(pTOContext, sql.ToString(), Make, parameters.ToArray());
            
            return data;
        }

        public Func<IDataReader, TOPqParam> Make = reader => new TOPqParam
        {            
            par_urle = reader["PAR_URLE"].AsString(),
            par_mcin = reader["PAR_MCIN"].AsString(),
            par_tcin = reader["PAR_TCIN"].AsString(),
            par_mctr = reader["PAR_MCTR"].AsString(),
            par_tctr = reader["PAR_TCTR"].AsString(),
            par_mccr = reader["PAR_MCCR"].AsString(),
            par_tccr = reader["PAR_TCCR"].AsString(),
            
        };
    }
}