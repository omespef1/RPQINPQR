using Digitalware.Seven.Utilidades;
using RPQINPQR.DAO;
using RPQINPQR.TO;
using RPQINPQR.tools;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace RPQINPQR.BO
{
    public class BOPqInpqr
    {
        DAOPqInpqr daoPqr = new DAOPqInpqr();
        mailHandler mailHandler = new mailHandler();
        DAOPqInpqr DAOPqr = new DAOPqInpqr();
        DAOGnInsta DAOInsta = new DAOGnInsta();
        DAOGnItems DAOItems = new DAOGnItems();
        DAOPqParam DAOPqParam = new DAOPqParam();
        DAOGnLogo DAOLogoEmpre = new DAOGnLogo();
        string emp_codi;
        public BOPqInpqr()
        {
           emp_codi = ConfigurationManager.AppSettings["emp_codi"];
         
        }
        public TOTransaction<PqrTransactionLoad> GetInitialDataWpqinqr(string cli_coda)
        {
            BOGnPaise boPaise = new BOGnPaise();
            BOGnDepar boDepar = new BOGnDepar();
            BOGnMunic boMunic = new BOGnMunic();
            BOGnItems boItems = new BOGnItems();
            BOGnDigfl boFlag = new BOGnDigfl();
            BOPqDpara boPerte = new BOPqDpara();
            BOGnArbol boArbol = new BOGnArbol();
            DAOGnParam daoParam = new DAOGnParam();
            DAOGnLogo daoLogo = new DAOGnLogo();
            DAOCtContr daoContr = new DAOCtContr();
            try
            {
                PqrTransactionLoad result = new PqrTransactionLoad();                      
                GnParam param = daoParam.GetGnParam(int.Parse(emp_codi));
                GnFlag spq000001 = boFlag.GetGnDigfl("SPQ000001");
                if (param == null)
                    throw new Exception(string.Format( "Parámetros de empresa no definidos para empresa {0}", emp_codi));
                List<GnPaise> countries = boPaise.GetGnPaise();            
                List<GnDepar> states = boDepar.GetGnDepar(param.pai_codi);                                 
                List<GnMunic> cities = boMunic.GetAllGnMunic(param.pai_codi);                
                GnFlag flag = boFlag.GetGnDigfl("SPQ000002");             
                List<GnItem> pqrType = boItems.GetGnItems(327);               
                List<GnItem> pqrSubject = boItems.GetGnItems(330);               
                List<GnItem> pqrInscription = boItems.GetGnItems(331);               
                List<TOGPerte> pqrGrpups = boPerte.GetPqDpara();                                                             
                result.countries = countries;
                result.states = states;
                result.cities = cities;
                result.pqrInscription = pqrInscription;
                result.pqrSubject = pqrSubject;
                result.pqrType = pqrType;
                result.pqrGroup = pqrGrpups;
                result.digiflag = flag;
                result.pqrImage = daoLogo.GetGnLogo(int.Parse(emp_codi)).emp_logs;
                if (emp_codi.Length > 0 && cli_coda!=null)
                {
                    FaClien client = DAOFaClien.GetFaClien(int.Parse(emp_codi), cli_coda);
                    if (client == null)
                        throw new Exception(string.Format("No se encontraron clientes con identificación {0} y empresa {1}", cli_coda, emp_codi));
                    result.client = client;
                    result.contracts = daoContr.GetCtContr(int.Parse(emp_codi),cli_coda);
                  
                }              
                if (spq000001 != null)
                    result.spq000001 = spq000001;
                return new TOTransaction<PqrTransactionLoad>() { objTransaction = result, txtRetorno = "", retorno = 0 };

            }
            catch (Exception ex)
            {
                return new TOTransaction<PqrTransactionLoad>() { objTransaction = null, retorno = 1, txtRetorno = ex.Message };
            }
        }

        public TOTransaction<PqInpqrSalida> PostPqr(PqInpqr pqr)
        {

            DAOGnArbol daoArbol = new DAOGnArbol();
            DAOGnItems daoItems = new DAOGnItems();
            BOGnRadju boRdju = new BOGnRadju();
            DAOGnParam daoParam = new DAOGnParam();            
            try
            {
                

                string emp_codi = ConfigurationManager.AppSettings["emp_codi"];
                GnParam param = daoParam.GetGnParam(int.Parse(emp_codi));
                string typeRequest = daoItems.GetGnItems(327, "").Where(i => i.ite_cont == pqr.ite_tpqr).FirstOrDefault().ite_nomb;
                if (string.IsNullOrEmpty(emp_codi))
                    throw new Exception("Código de empresa no parametrizado en api.");
                mailHandler.uploadFile();
                pqr.emp_codi = int.Parse(emp_codi);
                pqr.inp_cont = daoPqr.GetCont("PQ_INPQR", "INP_CONT");
                if (string.IsNullOrEmpty(pqr.arb_sucu))
                    pqr.arb_sucu = "0";
                pqr.arb_sucu = daoArbol.GetGnArbol("2", pqr.arb_sucu, int.Parse(emp_codi))[0].arb_cont.ToString();
                pqr.ite_frec = daoItems.GetGnItems(326, "3")[0].ite_cont.ToString();
                pqr.arb_cecr = daoArbol.GetGnArbol("3", "0", int.Parse(emp_codi))[0].arb_cont.ToString();
                if(pqr.pai_codi == param.pai_codi)
                {
                    //Cuando se selecciona el país de instalación el municipio contiene también la región separada por -
                    int mun_codi = int.Parse(pqr.mun_codi.Split('-')[0]);
                    pqr.reg_codi = int.Parse(pqr.mun_codi.Split('-')[1]);
                    pqr.mun_codi = mun_codi.ToString();
                }
                
                
                insertPqr(pqr, typeRequest);
              
                string cas_cont = mailHandler.createWorkFlow(pqr,typeRequest);
                if (cas_cont.AsInt() > 0)
                    daoPqr.updatePqr(pqr.inp_cont, cas_cont);
                string msg = mailHandler.GetDescriptionPqrMessage(pqr);

                return new TOTransaction<PqInpqrSalida>() { objTransaction = new PqInpqrSalida() { inp_cont = pqr.inp_cont, msg  = msg},retorno=0, txtRetorno=""};
              
            }
            catch (Exception ex)
            {
                if (pqr.inp_cont > 0)
                    daoPqr.deletePqr(pqr.inp_cont);
                return new TOTransaction<PqInpqrSalida>() { objTransaction = null, retorno = 1, txtRetorno = ex.Message };
            }
        }

        
    
    
        private void insertPqr(PqInpqr pqr, string typeRequest)
        {
            DAOPqParam daoPqParam = new DAOPqParam();
            DAOGnInsta daoInsta = new DAOGnInsta();
            DAOGnLogo daoLogo = new DAOGnLogo();
            if (string.IsNullOrEmpty(pqr.inp_nide))
                pqr.inp_nide = ".";
            int result = daoPqr.InserPqInpqr(pqr);
            pqr.genratedKey = mailHandler.GetPassword(pqr);
            // valida si tiene parametrizado Manejo Correo de Ingreso PQR en PQ_PARAM

            var MailPQParam = daoPqParam.GetMailParam();

            if (MailPQParam.par_mcin == "S") // Si tiene configurado envio de correo al ingresar un PQR
            {
                if (MailPQParam.par_tcin == null) // si no tiene una plantilla configurada para ingreso de PQR, continua con el metodo original para Defensa Civil
                {
                    pqr.genratedKey = mailHandler.GetPassword(pqr);
                    Mail mail = mailHandler.generateMail(pqr, MailPQParam, typeRequest);
                    Mail mailModulo = mailHandler.generateMail(pqr, MailPQParam, "", "Modulo");
                    GnInsta insta = daoInsta.GetGnInsta();
                    mailHandler.sendMail(mail, insta);
                    mailHandler.sendMail(mailModulo, insta);
                }
                else
                {


                    Gnlogo Logo = daoLogo.GetGnLogo(pqr.emp_codi);
                    MailImages ItemList = new MailImages();
                    ItemList.imageType = "Jpeg";
                    ItemList.sourceName = "logo";
                    ItemList.imgBytes = Logo.emp_logs;
                    List<MailImages> LstImg = new List<MailImages>();
                    LstImg.Add(ItemList);

                  
                    Mail mail = mailHandler.generateMailFromPQParam(pqr, MailPQParam, typeRequest);
                    Mail mailModulo = mailHandler.generateMailFromPQParam(pqr, MailPQParam, "", "Modulo");
                    GnInsta insta = daoInsta.GetGnInsta();
                   

                    mailHandler.sendMail(mail, insta, true, LstImg);
                    mailHandler.sendMail(mailModulo, insta, true, LstImg);
                }

            }

        }


       
     

        private string GetDescriptionPqrMessage(PqInpqr pqr)
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
        private static string EncodePassword(string originalPassword)
        {
            SHA1 sha1 = new SHA1CryptoServiceProvider();

            byte[] inputBytes = (new UnicodeEncoding()).GetBytes(originalPassword);
            byte[] hash = sha1.ComputeHash(inputBytes);

            return Convert.ToBase64String(hash);
        }
        private string GetPassword(PqInpqr pqr, string type = "")
        {
            string password = "";

            //Si es consulta   
            if (type == "C")
            {
                password = string.Format("{0}{1}", pqr.inp_cont, pqr.inp_fech.ToString("yyyyMMdd"));
            }
            //Si es generación de clave
            else
            {
                password = string.Format("{0}{1}", pqr.inp_cont, DateTime.Now.ToString("yyyyMMdd"));
            }
            return password;
        }
        private string createMessage()
        {
            StringBuilder text = new StringBuilder();
            text.AppendLine("Hemos recibido su PQR.");
            text.AppendLine("Su número de radicado es <b> @inp_cont.</b>");
            text.AppendLine("<b>Su contraseña es @password </b>");
            text.AppendLine("Con el número de radicado y clave puede consultar el estado de su PQR en cualquier momento.");
            text.AppendLine("Por favor conservar esta información.");
            return text.ToString();
        }
        private Mail generateMail(PqInpqr pqr, TOPqParam MailPQParam, string typeResquest = "", string typeMail = "")
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
                mail.mailTo = DAOPqr.GetRespModulo();

            }
            return mail;
        }

        public  TOTransaction<PqInpqr> GetInfoPqrGenerated(int inp_cont, string inp_pass)
        {
            DAOPqDinPq daoDinpq = new DAOPqDinPq();
            try
            {
                PqInpqr pqr = DAOPqr.getPqInPqr(inp_cont);
                if (pqr == null)
                    throw new Exception("No se encontraron datos");
                pqr.genratedKey = GetPassword(pqr, "C");
                if (inp_pass != pqr.genratedKey)
                    throw new Exception("La constraseña para este documento no es correcta");
                pqr.seguimientos = daoDinpq.getpqDinPq(inp_cont,int.Parse(emp_codi));
                return new TOTransaction<PqInpqr>() { objTransaction = pqr, retorno = 0, txtRetorno = "" };

            }
            catch (Exception ex)
            {
                return new TOTransaction<PqInpqr>() { objTransaction = null, retorno = 1, txtRetorno = ex.Message };
            }

            //Setear el key antes de devolverlo a la pagina principal, teniendo en  cuentas las condiciones

        }
        private bool sendMail(Mail mail, GnInsta insta, bool UseAltView = false, List<MailImages> LstImg = null)
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
            return send;
        }



        private Mail generateMailFromPQParam(PqInpqr pqr, TOPqParam MailPQParam, string typeResquest = "", string typeMail = "")
        {
            string codeSite = MailPQParam.par_urle; //ConfigurationManager.AppSettings["SearchSitePqr"].ToString();
            string codeUrl = string.Format("{0}?psw={1}&pqr={2}", codeSite, pqr.genratedKey, pqr.inp_cont);
            Mail mail = new Mail();
            if (typeMail == "")
            {
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
                mail.mailTo = DAOPqr.GetRespModulo();

            }
            return mail;
        }
    }
}