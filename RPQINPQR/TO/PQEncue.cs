using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RPQINPQR.TO
{
    public class PQEncue
    {
     public int     enc_cont { get; set; }
        public int inp_cont { get; set; }
        public int tip_codi { get; set; }
        public string enc_docu { get; set; }
        public string enc_nomb { get; set; }
        public string enc_apel{ get; set; }
        public string enc_preg { get; set; }
        public string enc_resp { get; set; }
        public DateTime enc_fech { get; set; }
    }
}