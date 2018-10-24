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
    public class DAOGnTipdo
    {
        public List<GnTipdo> getListGnTipdo()
        {           
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("  SELECT * FROM GN_TIPDO  ");
                OTOContext pTOContext = new OTOContext();
                var conection = DBFactory.GetDB(pTOContext);
                List<GnTipdo> data = conection.ReadList(pTOContext, sql.ToString(), Make);
                return data;                       
        }
        private static Func<IDataReader, GnTipdo> Make = reader => new GnTipdo
        {
            TIP_ABRE = reader["TIP_ABRE"].AsString(),
            TIP_CLAS = reader["TIP_CLAS"].AsString(),
            TIP_CODI = reader["TIP_CODI"].AsInt16(),
            TIP_NOMB = reader["TIP_NOMB"].AsString()
        };
    }
}