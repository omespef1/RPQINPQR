using Digitalware.Seven.Utilidades;
using RPQINPQR.DAO;
using RPQINPQR.TO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace RPQINPQR.Controllers
{
    public class DownloadController : ApiController
    {
        // GET: api/Download
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public TOTransaction<List<TOGnAdjun>> Get(int consecutivo,string pro_codi,string tableName)
        {

           
            DAOGnRadju daoRadju = new DAOGnRadju();
            DAOGnAdju daoAdju = new DAOGnAdju();
            List<TOGnAdjun> adjun = new List<TOGnAdjun>();
            string download = HttpContext.Current.Server.MapPath("../download/");
            string emp_codi = ConfigurationManager.AppSettings["emp_codi"];
            string alias = ConfigurationManager.AppSettings["alias"];
            string ftp = ConfigurationManager.AppSettings["servidorFTP"];
            List<TOGnAdjun> ficherosDescargados = new List<TOGnAdjun>();
            try
            {
                             
                if (emp_codi == null)
                    throw new Exception("Parámetro empresa no definido");
                if (alias == null)
                    throw new Exception("Parámetro alias no definido");
                if (ftp == null)
                    throw new Exception("Parámetro servidorFTP no definido");
                string userftp = FTPManager.UserFtp;
                string passFtp = FTPManager.PassFtp;
                List<TOGnRadju> radju = daoRadju.GetRadju(int.Parse(emp_codi), pro_codi, String.Concat(emp_codi, consecutivo));
                if (radju == null || !radju.Any())
                    throw new Exception("No hay adjuntos");
                int rad_cont = radju.FirstOrDefault().rad_cont;
                adjun = daoAdju.GetAdjun(int.Parse(emp_codi), rad_cont);
                if (adjun.Count == 0)
                    throw new Exception("No hay adjuntos");
                foreach(TOGnAdjun adjunto in adjun)
                {
                    string directoryDefault = string.Format("ftp://{0}/Seven/docs/{1}/{1}_{2}_{3}_{4}", ftp.ToString(), tableName, emp_codi.ToString(), rad_cont,adjunto.adj_cont);
                    List<DirectoryItem> filesExist = FTPManager.GetDirectoryInformation(directoryDefault);
                    if (filesExist!=null && filesExist.Count> 0)
                    {
                        foreach (DirectoryItem directories in filesExist)
                        {
                           
                            byte[] file =FTPManager.DownloadFtp(download, "", adjunto.adj_nomb, directoryDefault);
                            ficherosDescargados.Add(adjunto);
                        }
                    }
                }
                return new TOTransaction<List<TOGnAdjun>>() { objTransaction=ficherosDescargados, retorno = 0, txtRetorno = "" };
            }
            catch(Exception ex)
            {
                return new TOTransaction<List<TOGnAdjun>>() { objTransaction = null, retorno = 1, txtRetorno = ex.Message};
            }
               
            }


   


        // POST: api/Download
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Download/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Download/5
        public void Delete(int id)
        {
        }
    }
}
