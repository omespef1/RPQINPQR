using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Xml.Serialization;

namespace RPQINPQR.TO
{
    public class PqInpqr {

        public int emp_codi { get; set; }
        public int inp_cont { get; set; }
        public string inp_nide { get; set; }
        public string inp_nomb { get; set; }
        public string inp_apel { get; set; }
        public string inp_ntel { get; set; }
        public string inp_mail { get; set; }
        public int ite_tpqr { get; set; }
        public string inp_mpqr { get; set; }
        public int ite_tipi { get; set; }
        public int ite_stip { get; set; }
        public string inp_tido { get; set; }
        public string inp_dire { get; set; }
        public string inp_ncel { get; set; }
        public int pai_codi { get; set; }
        public int dep_codi { get; set; }
        public string mun_codi { get; set; }
        public int reg_codi { get; set; }
        public string inp_mres { get; set; }
        public string ite_frec { get; set; }
        public string genratedKey { get; set; }
        public DateTime inp_fech { get; set; }
        public string inp_esta { get; set; }
        public string arb_sucu { get; set; }
        public string arb_cecr { get; set; }
        public string  inp_gper { get; set; }
        public int cas_cont { get; set; }
        public string Path { get; set; }      
        public List<PqDinPq> seguimientos { get; set; }
        public HttpResponseMessage adj_file { get; set; }
        
    }
}