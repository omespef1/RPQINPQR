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
    public class DAOGnInsta
    {
        public GnInsta GetGnInsta()
        {
                  
            List<Parameter> parametros = new List<Parameter>();
            StringBuilder sql = new StringBuilder();
            sql.Append(" SELECT PAR_NMIN,PAR_NMAX,PAR_CCLN,PAR_CCLG,PAR_HOST,PAR_MAIL,PAR_PCOR,PAR_USMT,PAR_USSL,PAR_UTLS,PAR_CLMA,PAR_INFA ,PAR_ADJU");
            sql.Append(" FROM GN_INSTA ");
            OTOContext pTOContext = new OTOContext();         
            var conection = DBFactory.GetDB(pTOContext);
            var data = conection.Read(pTOContext, sql.ToString(), Make);
            return data;
        }
        public Func<IDataReader, GnInsta> Make = reader => new GnInsta
        {
            par_mail = reader["PAR_MAIL"].AsString(),
            par_host = reader["PAR_HOST"].AsString(),
            par_smtp = reader["PAR_USMT"].AsString(),
            par_clma = reader["PAR_CLMA"].AsString(),
            par_pcor = reader["PAR_PCOR"].AsString(),
            par_ussl = reader["PAR_USSL"].AsString(),
            par_adju = reader["PAR_ADJU"].ToString()

        };
    }
}