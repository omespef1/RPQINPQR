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
    public class DAOGnPaise
    {
        public List<GnPaise> GetGnPaise()
        {
            List<Parameter> parametros = new List<Parameter>();
            StringBuilder sql = new StringBuilder();
            sql.Append(" SELECT PAI_NOMB, PAI_CODI ");
            sql.Append(" FROM GN_PAISE ");
            sql.Append(" ORDER BY PAI_NOMB ");
            OTOContext pTOContext = new OTOContext();
            var conection = DBFactory.GetDB(pTOContext);
            List<GnPaise> data = conection.ReadList(pTOContext, sql.ToString(), Make);
            return data;

        }
        public Func<IDataReader, GnPaise> Make = reader => new GnPaise
        {
            pai_codi = reader["PAI_CODI"].AsInt(),
            pai_nomb = reader["PAI_NOMB"].AsString()
        };
    }
}