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
    public class DAOGnDepar
    {
        public List<GnDepar> GetGnDepar(int pai_codi)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" SELECT DEP_NOMB, DEP_CODI, PAI_CODI ");
            sql.Append(" FROM GN_DEPAR ");
            sql.Append(" WHERE PAI_CODI = @PAI_CODI ");
            List<Parameter> parameters = new List<Parameter>();
            parameters.Add(new Parameter("PAI_CODI", pai_codi));
            OTOContext pTOContext = new OTOContext();
            var conection = DBFactory.GetDB(pTOContext);
            List<GnDepar> data = conection.ReadList(pTOContext, sql.ToString(), Make,parameters.ToArray());
            return data;
        }
        private static Func<IDataReader, GnDepar> Make = reader => new GnDepar
        {
          dep_codi = reader["DEP_CODI"].AsInt(),
          dep_nomb = reader["DEP_NOMB"].AsString()
        };
    }
}