using Ophelia;
using Ophelia.DataBase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using RPQINPQR.TO;
using Ophelia.Comun;
namespace RPQINPQR.DAO
{
    public class DAOGnFlag
    {
       public GnFlag GetGnFlag(string dig_codi)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" SELECT * FROM GN_DIGFL  WHERE DIG_CODI=@DIG_CODI ");
            List<Parameter> parameters = new List<Parameter>();
            parameters.Add(new Parameter("@DIG_CODI", dig_codi));
            OTOContext pTOContext = new OTOContext();
            var conection = DBFactory.GetDB(pTOContext);
            var data = conection.Read(pTOContext, sql.ToString(), Make, parameters.ToArray());
            return data;
        }
        public Func<IDataReader, GnFlag> Make = reader => new GnFlag
        {
            dig_codi = reader["DIG_CODI"].AsString(),
            dig_nomb = reader["DIG_NOMB"].AsString(),
            dig_valo = reader["DIG_VALO"].AsString()
        };
    }
}