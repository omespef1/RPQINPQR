using Ophelia;
using Ophelia.DataBase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;

namespace RPQINPQR.DAO
{
    public class DAOGnAdju
    {        
        public int? insertGnAdju(TO.TOGnAdjun adjun)
        {
            DateTime date = DateTime.Now;
            StringBuilder sql = new StringBuilder();
            sql.Append("INSERT INTO GN_ADJUN ");
            sql.Append(" (EMP_CODI,RAD_CONT,ADJ_CONT,ADJ_NOMB,AUD_ESTA,AUD_USUA,AUD_UFAC,ADJ_TIPO) ");
            sql.Append(" VALUES (@EMP_CODI,@RAD_CONT,@ADJ_CONT,@ADJ_NOMB,@AUD_ESTA,@AUD_USUA,@AUD_UFAC,@ADJ_TIPO) ");
            List<Parameter> parametros = new List<Parameter>();
            parametros.Add(new Parameter("@EMP_CODI ", adjun.emp_codi));
            parametros.Add(new Parameter("@RAD_CONT", adjun.rad_cont));
            parametros.Add(new Parameter("@ADJ_CONT", adjun.adj_cont));
            parametros.Add(new Parameter("@ADJ_NOMB", adjun.adj_nomb)); //pendiente          
            parametros.Add(new Parameter("@AUD_ESTA", "A"));
            parametros.Add(new Parameter("@AUD_USUA", adjun.aud_usua));
            parametros.Add(new Parameter("@AUD_UFAC", DateTime.Now));
            parametros.Add(new Parameter("@ADJ_TIPO", adjun.adj_tipo));

            OTOContext pTOContext = new OTOContext();
            var conection = DBFactory.GetDB(pTOContext);
            return conection.Insert(pTOContext, sql.ToString(), parametros.ToArray());
        }
        public List<TO.TOGnAdjun> GetAdjun(int emp_codi, int rad_cont)
        {
            DateTime date = DateTime.Now;
            StringBuilder sql = new StringBuilder();
            sql.Append(" SELECT * ");
            sql.Append(" FROM   GN_ADJUN ");
            sql.Append(" WHERE  EMP_CODI  = @EMP_CODI  AND RAD_CONT     = @RAD_CONT");
            List<Parameter> parametros = new List<Parameter>();
            parametros.Add(new Parameter("@EMP_CODI ", emp_codi));
            parametros.Add(new Parameter("@RAD_CONT", rad_cont));          
            OTOContext pTOContext = new OTOContext();
            var conection = DBFactory.GetDB(pTOContext);
            return conection.ReadList(pTOContext, sql.ToString(), Make, parametros.ToArray());
        }
        private static Func<IDataReader, TO.TOGnAdjun> Make = reader => new TO.TOGnAdjun
        {
            emp_codi = reader["EMP_CODI"].AsInt(),         
            rad_cont = reader["RAD_CONT"].AsInt(),
            adj_cont =reader["ADJ_CONT"].AsInt(),
            adj_nomb= reader["ADJ_NOMB"].ToString()
        };
    }
}