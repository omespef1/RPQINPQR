using Ophelia;
using Ophelia.Comun;
using Ophelia.DataBase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Configuration;

namespace RPQINPQR.DAO
{
    public class DAOGnAdjFi
    {
        public string usu_codi { get; set; }
        public int emp_codi { get; set; }
        public DAOGnAdjFi()
        {
            emp_codi = ConfigurationManager.AppSettings["emp_codi"].AsInt();
            usu_codi = ConfigurationManager.AppSettings["usu_codi"].AsString();
        }
        public List<TO.TOGnAdjFi> GetAdjFi(int emp_codi, int rad_cont)
        {
            DateTime date = DateTime.Now;
            StringBuilder sql = new StringBuilder();
            sql.Append(" SELECT * ");
            sql.Append(" FROM   GN_ADJFI ");
            sql.Append("WHERE  EMP_CODI = @EMP_CODI AND RAD_CONT  = @RAD_CONT ");
            List<Parameter> parametros = new List<Parameter>();
            parametros.Add(new Parameter("@EMP_CODI ", emp_codi));
            parametros.Add(new Parameter("@RAD_CONT", rad_cont));
            OTOContext pTOContext = new OTOContext();
            var conection = DBFactory.GetDB(pTOContext);
            return conection.ReadList(pTOContext, sql.ToString(), Make, parametros.ToArray());
        }
        public int insertGnAdjfi(TO.TOGnAdjFi adjfi)
        {
            DateTime date = DateTime.Now;
            StringBuilder sql = new StringBuilder();
            sql.Append(" INSERT INTO GN_ADJFI( ");
            sql.Append(" EMP_CODI,RAD_CONT,ADJ_CONT,ADJ_FILE,AUD_ESTA,AUD_USUA,AUD_UFAC)");
            sql.Append(" VALUES( ");
            sql.Append(" @EMP_CODI,@RAD_CONT,@ADJ_CONT,@ADJ_FILE,@AUD_ESTA,@AUD_USUA,@AUD_UFAC) ");
            List<Parameter> parametros = new List<Parameter>();
            parametros.Add(new Parameter("@EMP_CODI",emp_codi));
            parametros.Add(new Parameter("@RAD_CONT", adjfi.rad_cont));
            parametros.Add(new Parameter("@ADJ_CONT", adjfi.adj_cont));
            parametros.Add(new Parameter("@ADJ_FILE",  adjfi.adj_file)); //pendiente          
            parametros.Add(new Parameter("@AUD_ESTA", "A"));
            parametros.Add(new Parameter("@AUD_USUA", usu_codi));
            parametros.Add(new Parameter("@AUD_UFAC", DateTime.Now));
            OTOContext pTOContext = new OTOContext();
            var conection = DBFactory.GetDB(pTOContext);
            return conection.Insert(pTOContext, sql.ToString(), parametros.ToArray());
        }
        private static Func<IDataReader, TO.TOGnAdjFi> Make = reader => new TO.TOGnAdjFi
        {
            adj_cont = reader["ADJ_CONT"].AsInt(),
            rad_cont = reader["RAD_CONT"].AsInt(),               
        };
    }
}
