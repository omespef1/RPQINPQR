using Ophelia;
using Ophelia.DataBase;
using Ophelia.Comun;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using RPQINPQR.TO;
using System.Configuration;
namespace RPQINPQR.DAO
{
    public class DAOWfCdocu
    {

        public string usu_codi;
        public int emp_codi;
        public DAOWfCdocu()
        {
            emp_codi = ConfigurationManager.AppSettings["emp_codi"].AsInt();
            usu_codi = ConfigurationManager.AppSettings["usuario"].AsString();
        }
        public List<TOWfCdocu> getWfCdocu(int cas_cont)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" SELECT  * FROM WF_CDOCU WHERE CAS_CONT=@CAS_CONT ");
            List<Parameter> parameters = new List<Parameter>();
            parameters.Add(new Parameter("@CAS_CONT", cas_cont));
            OTOContext pTOContext = new OTOContext();
            var conection = DBFactory.GetDB(pTOContext);
            var data = conection.ReadList(pTOContext, sql.ToString(), Make, parameters.ToArray());
            return data;
        }
        public int insertWfCodcu(TOWfCdocu wfcdocu)
        {
            DateTime date = DateTime.Now;
            StringBuilder sql = new StringBuilder();
            sql.Append(" INSERT INTO WF_CDOCU ");
            sql.Append(" (EMP_CODI,CAS_CONT,DOC_CONT,DOC_DESC,DOC_BLOB,AUD_ESTA,AUD_USUA,AUD_UFAC) ");
            sql.Append(" VALUES(@EMP_CODI,@CAS_CONT,@DOC_CONT,@DOC_DESC,@DOC_BLOB,@AUD_ESTA,@AUD_USUA,@AUD_UFAC) ");
            List<Parameter> parametros = new List<Parameter>();
            parametros.Add(new Parameter("@EMP_CODI ", emp_codi));
            parametros.Add(new Parameter("@CAS_CONT", wfcdocu.cas_cont));
            parametros.Add(new Parameter("@DOC_CONT", wfcdocu.doc_cont));
            parametros.Add(new Parameter("@DOC_DESC", wfcdocu.doc_desc));       
            parametros.Add(new Parameter("@DOC_BLOB", wfcdocu.doc_blob));
            parametros.Add(new Parameter("@AUD_ESTA", "A"));
            parametros.Add(new Parameter("@AUD_USUA", usu_codi));
            parametros.Add(new Parameter("@AUD_UFAC", DateTime.Now));
            OTOContext pTOContext = new OTOContext();
            var conection = DBFactory.GetDB(pTOContext);
            return conection.Insert(pTOContext, sql.ToString(), parametros.ToArray());
        }
        public Func<IDataReader, TOWfCdocu> Make = reader => new TOWfCdocu
        {
            doc_blob = reader["DOC_BLOB"].AsString(),
            doc_cont = reader["DOC_CONT"].AsInt(),
            cas_cont = reader["CAS_CONT"].AsInt()  ,
            doc_desc = reader["DOC_DESC"].AsString()        
        };
    }
}