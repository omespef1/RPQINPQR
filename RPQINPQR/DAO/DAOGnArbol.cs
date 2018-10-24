using Ophelia;
using Ophelia.DataBase;
using Ophelia.Comun;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using RPQINPQR.TO;

namespace RPQINPQR.DAO
{
    public class DAOGnArbol
    {
        public List<GnArbol> GetGnArbol(string tar_codi,string arb_codi,int emp_codi)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" SELECT * FROM GN_ARBOL  ");
            sql.Append(" WHERE EMP_CODI =@EMP_CODI AND TAR_CODI = @TAR_CODI AND ARB_CODI = @ARB_CODI ");
            List<Parameter> parameters = new List<Parameter>();
            parameters.Add(new Parameter("@EMP_CODI", emp_codi));
            parameters.Add(new Parameter("@TAR_CODI", tar_codi));
            parameters.Add(new Parameter("@ARB_CODI", arb_codi));
            OTOContext pTOContext = new OTOContext();
            var conection = DBFactory.GetDB(pTOContext);
            List<GnArbol> data = conection.ReadList(pTOContext, sql.ToString(), Make, parameters.ToArray());
            return data;
        }
        private static Func<IDataReader, GnArbol> Make = reader => new GnArbol
        {
            arb_codi   = reader["ARB_CODI"].AsString(),
            arb_nomb = reader["ARB_NOMB"].AsString(),
            arb_cont = reader["ARB_CONT"].AsInt()
        };
    }
}