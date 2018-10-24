using Digitalware.Seven.Utilidades;
using RPQINPQR.DAO;
using RPQINPQR.TO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Data;

namespace RPQINPQR.tools
{
    public class mailHandler
    {
        DAOPqInpqr daoPqr = new DAOPqInpqr();
        public string GetPassword(PqInpqr pqr, string type = "")
        {
            string password = "";

            //Si es consulta   
            if (type == "C")
            {
                password = string.Format("{0}{1}", pqr.inp_cont, pqr.inp_fech.ToString("yyyyMMdd"));
            }
            else
            {
                password = string.Format("{0}{1}", pqr.inp_cont, DateTime.Now.ToString("yyyyMMdd"));
            }
            return password;
        }
        public Mail generateMail(PqInpqr pqr, TOPqParam MailPQParam, string typeResquest = "", string typeMail = "")
        {
            string codeSite = MailPQParam.par_urle; //ConfigurationManager.AppSettings["SearchSitePqr"].ToString();
            string codeUrl = string.Format("{0}?psw={1}&pqr={2}", codeSite, pqr.genratedKey, pqr.inp_cont);
            Mail mail = new Mail();
            if (typeMail == "")
            {
                mail.subject = string.Format("Derecho de petición No. {0} ({1}) con código {2}", pqr.inp_cont, typeResquest, pqr.genratedKey.ToUpper());


                StringBuilder body = new StringBuilder();
                body.AppendLine(string.Format("Apreciado(a) <b>{0} {1}.</b><br>", pqr.inp_nomb, pqr.inp_apel));
                body.AppendLine("<br>");
                body.AppendLine("Cordial saludo de Defensa Civil Colombiana. <br>");
                body.AppendLine(string.Format("Informamos que hemos recibido su <b>{0}</b> el {1} y está siendo trámitado.<br>", typeResquest, DateTime.Now.ToString()));
                body.AppendLine("<br>");
                body.AppendLine(string.Format("Su código de radicado es: <b>{0}.</b><br>", pqr.inp_cont));
                body.AppendLine(string.Format("Su contraseña es: <b>{0}</b><br>", pqr.genratedKey));
                body.AppendLine("<br>");
                body.AppendLine(string.Format("<a href='{0}'>Consulte el estado de su solicitud aquí</a><br>", codeUrl));
                body.AppendLine("<br>");
                body.AppendLine("Agradecemos su retroalimentación, la cual contribuye al mejoramiento de nuestro servicio y");
                body.Append("al cumplimiento de nuestro objetivo más importante, la satisfacción de las necesidades de personas que como usted han confiado ");
                body.Append("en nuestra Institución.<br> <b> POR FAVOR NO RESPONDA ESTE MAIL. </b>");
                mail.body = body.ToString();
                mail.mailTo = pqr.inp_mail;
            }
            else
            {
                StringBuilder body = new StringBuilder();
                mail.subject = string.Format("Derecho de petición No. {0}.", pqr.inp_cont);
                body.AppendLine(" Cordial Saludo de la Defensa Civil Colombiana.<br> ");
                body.AppendLine(string.Format(" Informamos que ha sido creada una solicitud de {0} {1}, en el sistema de Atención al Ciudadano.<br>", pqr.inp_nomb, pqr.inp_apel));
                body.AppendLine("POR FAVOR NO RESPONDA ESTE MAIL.");
                mail.body = body.ToString();
                mail.mailTo = daoPqr.GetRespModulo();

            }
            return mail;
        }
        public  Mail generateMailFromPQParam(PqInpqr pqr, TOPqParam MailPQParam, string typeResquest = "", string typeMail = "")
        {
            mailHandler mailHandler = new mailHandler();
            string codeSite = MailPQParam.par_urle; //ConfigurationManager.AppSettings["SearchSitePqr"].ToString();
            string codeUrl = string.Format("{0}?psw={1}&pqr={2}", codeSite, pqr.genratedKey, pqr.inp_cont);
            Mail mail = new Mail();
            if (typeMail == "")
            {
                MailPQParam.par_tcin = mailHandler.BuildDinamicMail(MailPQParam, pqr.emp_codi, pqr.inp_cont);
                mail.subject = string.Format("Derecho de petición No. {0} ({1}) con código {2}", pqr.inp_cont, typeResquest, pqr.genratedKey.ToUpper());
                StringBuilder body = new StringBuilder();
                body.AppendLine("<!DOCTYPE html><html><head></head><body>");
                body.AppendLine("<div>" + MailPQParam.par_tcin.Replace("\r\n", "<br>") + "</div>");
                body.AppendLine("<div style='margin-top:50px;'>");
                body.AppendLine("<img src='cid:logo'/>");
                body.AppendLine("</div>");
                body.Append("</body>");
                body.Append("</html>");

                mail.body = body.ToString();
                mail.mailTo = pqr.inp_mail;

            }
            else
            {
                StringBuilder body = new StringBuilder();
                mail.subject = string.Format("Derecho de petición No. {0}.", pqr.inp_cont);

                body.AppendLine(" Cordial Saludo.<br> ");
                body.AppendLine(string.Format(" Informamos que ha sido creada una solicitud de {0} {1}, en el sistema de Atención al Ciudadano.<br>", pqr.inp_nomb, pqr.inp_apel));
                body.AppendLine("POR FAVOR NO RESPONDA ESTE MAIL.");
                body.AppendLine("<div style='margin-top:50px;'>");
                body.AppendLine("<img src='cid:logo'/>");
                body.AppendLine("</div>");
                mail.body = body.ToString();
                mail.mailTo = daoPqr.GetRespModulo();

            }
            return mail;
        }

        public async void sendMail(Mail mail, GnInsta insta, bool UseAltView = false, List<MailImages> LstImg = null)
        {
            bool send = false;
            try
            {
                 send = MailHelper.SendMail(new string[] { mail.mailTo }, mail.subject, mail.body, insta.par_mail,
                insta.par_host, insta.par_smtp, insta.par_clma, insta.par_pcor.AsInt(), insta.par_ussl == "S", true, UseAltView, LstImg);
            }
            catch (Exception ex)
            {
                send = false;
            }
           
        }

        public string GetDescriptionPqrMessage(PqInpqr pqr)
        {
            string description = "";
            string texto = "";
            try
            {
                string password = pqr.genratedKey;
                texto = createMessage();
                texto = texto.Replace("@inp_cont", pqr.inp_cont.ToString());
                texto = texto.Replace("@password", password);
                description = texto;
            }
            catch (Exception ex)
            {
                description = ex.Message;
            }
            return description;
        }
        public string createMessage()
        {
            StringBuilder text = new StringBuilder();
            text.AppendLine("Hemos recibido su PQR.");
            text.AppendLine("Su número de radicado es <b> @inp_cont.</b>");
            text.AppendLine("<b>Su contraseña es @password </b>");
            text.AppendLine("Con el número de radicado y clave puede consultar el estado de su PQR en cualquier momento.");
            text.AppendLine("Por favor conservar esta información.");
            return text.ToString();
        }
        public void uploadFile()
        {
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                var httpRequest = HttpContext.Current.Request;
                if (httpRequest.Files.Count > 0)
                {
                    foreach (string file in httpRequest.Files)
                    {
                        var postedFile = httpRequest.Files[file];
                        var filePath = HttpContext.Current.Server.MapPath("~/Upload/" + postedFile.FileName);
                        postedFile.SaveAs(filePath);
                    }
                }
                //return response;
            }
            catch (Exception ex)
            {

            }

        }
        public string createWorkFlow(PqInpqr pqr, string TypeRequest)
        {

            swflup.SWFRFLUP ws = new swflup.SWFRFLUP();
            swflup.TOWfRflup to = new swflup.TOWfRflup();
            to.cam_name = "INP_CONT";
            to.cas_desc = String.Format("Creación de PQR # {0} , Tipo de Solicitud : {1}", pqr.inp_cont, TypeRequest);
            to.cas_narc = "";
            to.emp_codi = pqr.emp_codi;
            to.frm_codi = "SPQINPQR";
            to.num_cont = pqr.inp_cont.ToString();
            to.pro_codi = "SPQINPQR";
            to.tbl_name = "PQ_INPQR";
            to.usu_codi = ConfigurationManager.AppSettings["usu_codi"].ToString();
            var response = ws.EnviarWF(to);
            if (response.Retorno != "0")
                throw new Exception(string.Format("Error creando flujo :{0}", response.TxtError));

            return response.Tra_cont;
        }

        public string BuildDinamicMail (TOPqParam pqparam,int emp_codi,int inp_cont)
        {
            string body = pqparam.par_tcin;
            try
            {
               
                List<string> filedNames = new List<string>();
                var sourceFields = pqparam.par_tcin.Split('#');
                foreach(string field in sourceFields)
                {
                   string fieldName = field.Substring(0, 8);
                    filedNames.Add(fieldName);
                }
                filedNames.Remove(filedNames.FirstOrDefault());
                string query = string.Join( ",", filedNames.ToArray());
                var resultQuery = daoPqr.GetMailInformationDinamic(query, emp_codi, inp_cont);
                if (resultQuery.Tables[0].Rows.Count > 0)
                {
                  foreach(DataRow fila in resultQuery.Tables[0].Rows)
                    {
                        for(int i=0; i< filedNames.Count;i++)
                        {
                          body =  body.Replace(filedNames[i], fila[i].ToString()) ;
                        }
                    }
                }

              body =  body.Replace("#", "");
            }
            catch(Exception ex)
            {

            }
            return body;
        }
    }
}