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
    public class DAOGPerte
    {
        public List<TOGPerte> GetGrupoPerteneciente(int emp_codi)
        {
            List<Parameter> parametros = new List<Parameter>();
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT * FROM  PQ_DPARA WHERE EMP_CODI=@EMP_CODI");
            OTOContext pTOContext = new OTOContext();
            List<Parameter> paramters = new List<Parameter>();
            paramters.Add(new Parameter("@EMP_CODI", emp_codi));           
            var conection = DBFactory.GetDB(pTOContext);
            List<TOGPerte> data = conection.ReadList(pTOContext, sql.ToString(), Make, paramters.ToArray());
            return data;

        }
        public Func<IDataReader, TOGPerte> Make = reader => new TOGPerte
        {
            dpa_codi = reader["DPA_CODI"].AsInt(),
            dpa_grup = reader["DPA_GRUP"].AsString(),
            
        };

    }
    
}