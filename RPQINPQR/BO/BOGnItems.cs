﻿using RPQINPQR.DAO;
using RPQINPQR.TO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RPQINPQR.BO
{
    public class BOGnItems
    {
        public List<GnItem>  GetGnItems(int tit_cont, string ite_codi="")
        {
           
                DAOGnItems dao = new DAOGnItems();
                List<GnItem> result = new List<GnItem>();
                result = dao.GetGnItems(tit_cont, ite_codi);
                if (result == null || !result.Any())
                    throw new Exception(string.Format("No se encontraron items parametrizados para el titem {0}",tit_cont));
            return result;
           
        }
    }
}