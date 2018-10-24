using RPQINPQR.BO;
using RPQINPQR.DAO;
using RPQINPQR.TO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace RPQINPQR.Controllers
{
    public class uploadController : ApiController
    {
        [EnableCors(origins: "*", headers: "*", methods: "*")]
       
       
        public TOTransaction Post()
        {
            BOGnRadju boRadju = new BOGnRadju();
            var httpRequest = HttpContext.Current.Request;
            if (httpRequest.Files.Count > 0)
            {
                int inp_cont = 0;
                try
                {
                    List<HttpPostedFile> files = new List<HttpPostedFile>();
                    string emp_codi = ConfigurationManager.AppSettings["emp_codi"];
                    if (string.IsNullOrEmpty(emp_codi))
                        throw new Exception("Código de empresa no parametrizado en el api");
                    foreach (string file in httpRequest.Files)
                    {
                        var postedFile = httpRequest.Files[file];
                        files.Add(postedFile);
                        inp_cont =int.Parse( Path.GetFileNameWithoutExtension(postedFile.FileName));
                       // var filePath = HttpContext.Current.Server.MapPath("~/Upload/" + postedFile.FileName);
                        string key = string.Concat(emp_codi, inp_cont);
                     var saveAttchment =   boRadju.insertGnRadju(short.Parse(emp_codi), key, "PQ_INPQR", "SPQINPQR", files);
                        if (!saveAttchment.Item1)
                            throw new Exception(string.Format("Error insertando adjunto {0}", saveAttchment.Item2));
                    }
                }
                catch(Exception ex)
                {
                    DAOPqInpqr daoPqr = new DAOPqInpqr();
                    daoPqr.deletePqr(inp_cont);
                    return new TOTransaction() { retorno = 1, txtRetorno = ex.Message};
                }
           
            }
            return new TOTransaction() { retorno = 0, txtRetorno = "" };

        }


    }
}
