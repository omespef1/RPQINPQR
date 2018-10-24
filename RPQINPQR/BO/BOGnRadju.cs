using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using RPQINPQR.DAO;
using Digitalware.Seven.Utilidades;
using RPQINPQR.TO;
using System.IO;
using RPQINPQR.tools;

namespace RPQINPQR.BO
{
    public class BOGnRadju
    {
        DAOGnRadju DAORadju = new DAOGnRadju();
        DAOGnAdju DAOAdju = new DAOGnAdju();
        DAOGnInsta DAOInsta = new DAOGnInsta();
        DAOGnAdjFi DAOAdjfi = new DAOGnAdjFi();
        DAOGnConse DAOConse = new DAOGnConse();
        public int emp_codi { get; set; }        
        public string usu_codi { get; set; }
        public BOGnRadju()
        {
            emp_codi = int.Parse(ConfigurationManager.AppSettings["emp_codi"].ToString());
            usu_codi = ConfigurationManager.AppSettings["usu_codi"].ToString();           
        }
        public Tuple<bool, string> insertGnRadju(short emp_codi,
            string key,
            string table,
            string pro_codi,          
           List< HttpPostedFile> files)
        {          
            try
            {              
               var insta =  DAOInsta.GetGnInsta();
                /*consultar si ya existe algún registro para sacar el consecutivo, sino se busca consecutivo*/
                int rad_cont = 0;
                List<TO.TOGnRadju> rad = DAORadju.GetRadju(emp_codi,key, pro_codi);
                if (rad != null && rad.Any())
                    rad_cont = rad.FirstOrDefault().rad_cont.AsInt();
                else
                {
                   
                  var consec=  DAOConse.getGnConse(325);
                    if (consec == 0)
                        throw new Exception(string.Format(
                                "No se encontró consecutivo para la empresa {0} y código consecutivo {1}",
                                emp_codi, "325"));
                    rad_cont = consec + 1;
                }              
              
               
                int? r = null;

                if (rad == null || !rad.Any())
                {
                    //Primero guardamos en la tabla de adjuntos
                    r = DAORadju.InsertarGnRadju(new TO.TOGnRadju()
                    {
                        rad_cont = rad_cont,
                        rad_tabl = table,
                        pro_codi = pro_codi,
                        rad_llav = key,
                        emp_codi = emp_codi
                    });
                    if (r == null)
                        throw new Exception("Ha ocurrido un error al guardar el archivo adjunto");
                }

                //Actualiza el gn conse
                DAOConse.updateGnConse(325,rad_cont);
                //buscamos el consecutivo a asignar
                List<TO.TOGnAdjun> adjunts = DAOAdju.GetAdjun(emp_codi, rad_cont);
                int adj_cont = adjunts == null || !adjunts.Any() ? 1 : adjunts.Max(o => o.adj_cont).AsInt() + 1;
                for (int i=0;i<=files.Count-1;i++)
                {
                    adj_cont = i == 0 ? adj_cont : adj_cont + 1;                  
                    TOGnAdjun adjun = new TOGnAdjun();
                    adjun.rad_cont = rad_cont;
                    adjun.adj_cont = adj_cont;
                    adjun.adj_nomb = files[i].FileName;
                    adjun.adj_tipo = "A";
                    adjun.aud_usua = usu_codi;
                    adjun.emp_codi = emp_codi;
                    r = DAOAdju.insertGnAdju(adjun);
                    if (r == null)
                        throw new Exception("Ha ocurrido un error al guardar el archivo adjunto");
                    string fileName = string.Format("{0}_{1}_{2}_{3}", table,
                   emp_codi, rad_cont, adj_cont);
                    if (insta.par_adju == "F")
                    {                     
                        Digitalware.Seven.SQLUtilidades.ActionResult result = FTPManager
                            .FileUpload(files[i].InputStream, table + "/" + fileName);
                        if (!result.State)
                            throw new Exception(result.Message);
                    }
                    if (insta.par_adju == "B")
                    {
                        //Consultamos si hay adjuntos para obtener el adj_cont
                        List<TOGnAdjFi> adjfi = DAOAdjfi.GetAdjFi(emp_codi, rad_cont);                      
                        adj_cont = adjfi == null || !adjfi.Any() ? 1 : adjfi.Max(j => j.adj_cont + 1);
                      //  string hex = Helpers.ByteArrayToString(files[i].data);
                        DAOAdjfi.insertGnAdjfi(new TOGnAdjFi { adj_cont = adj_cont, adj_file = fileHandler.ReadFully( files[i].InputStream), rad_cont = rad_cont });
                    }

                }
                             
                return new Tuple<bool, string>(true, "");
            }
            catch (Exception ex)
            {
                return new Tuple<bool, string>(false, ex.Message);
            }


        }
    }
}