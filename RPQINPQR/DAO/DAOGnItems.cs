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
    public class DAOGnItems
    {
        public List<GnItem>GetGnItems(int tit_cont,string ite_codi)
        {
            List<Parameter> parametros = new List<Parameter>();
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT * FROM GN_ITEMS WHERE TIT_CONT=@tit_cont AND ITE_ACTI='S' ");          
            OTOContext pTOContext = new OTOContext();
            List<Parameter> paramters = new List<Parameter>();
            paramters.Add(new Parameter("@tit_cont", tit_cont));
            if (ite_codi != "")
            {
                sql.Append(" and ITE_CODI = @ITE_CODI");
                paramters.Add(new Parameter("@ITE_CODI", ite_codi));
            }
            var conection = DBFactory.GetDB(pTOContext);
            List<GnItem> data = conection.ReadList(pTOContext, sql.ToString(), Make,paramters.ToArray());
            return data;

        }
        public Func<IDataReader, GnItem> Make = reader => new GnItem
        {
            ite_codi = reader["ITE_CODI"].AsString(),
            ite_nomb = reader["ITE_NOMB"].AsString(),
            ite_cont = reader["ITE_CONT"].AsInt()
        };
    }
}