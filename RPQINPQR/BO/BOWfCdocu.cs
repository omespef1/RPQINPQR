using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RPQINPQR.DAO;
using RPQINPQR.TO;
namespace RPQINPQR.BO
{
    public class BOWfCdocu
    {
        DAOWfCdocu DAODocu = new DAOWfCdocu();
      public int insertWfCdocu(TOWfCdocu wfdocu)
        {
            //Consulta si ya hay adjuntos
            List<TOWfCdocu> wfcdocu = DAODocu.getWfCdocu(wfdocu.cas_cont);
            int doc_cont = wfcdocu == null || !wfcdocu.Any() ? 1 : wfcdocu.Max(d => d.doc_cont + 1);
            wfdocu.doc_cont = doc_cont;
            int retorno = DAODocu.insertWfCodcu(wfdocu);
            return retorno;
        }
    }
}