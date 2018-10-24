using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using Ophelia.Comun;
using Ophelia.DataBase;
using Ophelia;
using RPQINPQR.TO;
using System.Data;
using System.Configuration;
namespace RPQINPQR.DAO
{
    public class DAOPqEncue
    {
        public int insertDAOPqEncue(PQEncue encue)
        {
            string emp_codi = ConfigurationManager.AppSettings["emp_codi"].ToString();
            StringBuilder sql = new StringBuilder();
            sql.Append("INSERT INTO PQ_ENCUE ");
            sql.Append("(EMP_CODI,AUD_ESTA, AUD_UFAC,AUD_USUA,ENC_CONT,INP_CONT,TIP_CODI,ENC_DOCU,ENC_NOMB,ENC_APEL,ENC_PREG, ");
            sql.Append("ENC_RESP,ENC_FECH )VALUES ");
            sql.Append("(@EMP_CODI,@AUD_ESTA,@AUD_UFAC, @AUD_USUA, @ENC_CONT,@INP_CONT,@TIP_CODI,@ENC_DOCU,@ENC_NOMB, @ENC_APEL, ");
            sql.Append(" @ENC_PREG,@ENC_RESP, @ENC_FECH) ");
            OTOContext ptoContext = new OTOContext();
            List<Parameter> parameters = new List<Parameter>();          
            parameters.Add(new Parameter("@EMP_CODI",emp_codi));
            parameters.Add(new Parameter("@AUD_ESTA","A"));
            parameters.Add(new Parameter("@AUD_UFAC",DateTime.Now));
            parameters.Add(new Parameter("@AUD_USUA","seven"));
            parameters.Add(new Parameter("@ENC_CONT",encue.enc_cont));
            parameters.Add(new Parameter("@INP_CONT",encue.inp_cont));
            parameters.Add(new Parameter("@TIP_CODI",encue.tip_codi));
            parameters.Add(new Parameter("@ENC_DOCU",encue.enc_docu));
            parameters.Add(new Parameter("@ENC_NOMB",encue.enc_nomb));
            parameters.Add(new Parameter("@ENC_APEL",encue.enc_apel));
            parameters.Add(new Parameter("@ENC_PREG",encue.enc_preg));
            parameters.Add(new Parameter("@ENC_RESP",encue.enc_resp));
            parameters.Add(new Parameter("@ENC_FECH",encue.enc_fech));
            var connection = DBFactory.GetDB(ptoContext);
            int result =  connection.Insert(ptoContext, sql.ToString(),parameters.ToArray());
            return result;

        }
        public int getPqEncue(int inp_cont)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" SELECT ENC_CONT  FROM PQ_ENCUE  WHERE INP_CONT=@INP_CONT ");
            OTOContext PToContext = new OTOContext();
            List<Parameter> parameters = new List<Parameter>();
            parameters.Add(new Parameter("@INP_CONT", inp_cont));
            var connection = DBFactory.GetDB(PToContext);
            var result = connection.GetScalar(PToContext, sql.ToString(), parameters.ToArray());
            if (result == null)
                return 0;
            return result.AsInt();
        }
       
    }
}