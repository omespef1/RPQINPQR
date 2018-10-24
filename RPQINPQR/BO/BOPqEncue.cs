using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RPQINPQR.DAO;
using RPQINPQR.TO;

namespace RPQINPQR.BO
{
    public class BOPqEncue
    {
        public TOTransaction SetPqEncue(List<PQEncue> encuesta)
        {
            DAOPqInpqr DAOPqr = new DAOPqInpqr();
            DAOPqEncue DAOEncue = new DAOPqEncue();
            try
            {
                if (encuesta != null && encuesta.Count > 0)
                {
                    foreach (PQEncue encue in encuesta)
                    {

                        encue.enc_cont = DAOPqr.GetCont("PQ_ENCUE", "ENC_CONT");                                             
                        encue.enc_fech = DateTime.Now;
                        var insertion = DAOEncue.insertDAOPqEncue(encue);
                        if (insertion != 0)
                            throw new Exception("No se envió la encuesta.Contacte con su proveedor de servicios");
                    }
                }
                return new TOTransaction() { txtRetorno = "", retorno = 0 };
               
            }
            catch (Exception ex)
            {
                return new TOTransaction() { retorno = 1, txtRetorno = ex.Message };
            }
        }

        public TOTransaction GetPqncue (int inp_cont)
        {
            DAOPqEncue DAOEncue = new DAOPqEncue();
            try
            {
                if (DAOEncue.getPqEncue(inp_cont) > 0)
                    return new TOTransaction() { retorno = 1, txtRetorno = "" };
                return new TOTransaction() { retorno = 0, txtRetorno = "" };
            }
            catch (Exception ex)
            {
                return new TOTransaction() { retorno = 1, txtRetorno = ex.Message };
            }
        }
    }
}