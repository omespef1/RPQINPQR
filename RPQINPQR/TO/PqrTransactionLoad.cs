using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RPQINPQR.TO
{
    public class PqrTransactionLoad
    {
        /// <summary>
        /// Países
        /// </summary>
        public List<GnPaise> countries { get; set; }
        /// <summary>
        /// Departmentos
        /// </summary>
        public List<GnDepar> states { get; set; }
        /// <summary>
        /// Ciudades
        /// </summary>
        public List<GnMunic> cities { get; set; }
        /// <summary>
        /// Digiflag para formulario de pqr
        /// </summary>
        public GnFlag digiflag { get; set; }
        /// <summary>
        /// Lista de items 
        /// </summary>
        public List<GnItem> pqrType { get; set; } 
        /// <summary>
        /// Lista de items
        /// </summary>
        public List<GnItem> pqrSubject { get; set; }
        /// <summary>
        /// Lista de items
        /// </summary>
        public List<GnItem> pqrInscription { get; set; }
        /// <summary>
        /// Grupo pertenece
        /// </summary>
        public List<TOGPerte> pqrGroup { get; set; }
        /// <summary>
        /// Imagen de empresa
        /// </summary>
        ///           
        public byte[] pqrImage { get; set; }
        /// <summary>
        /// Cliente , si se ingresa desde el selfservices
        /// </summary>
        public FaClien client { get; set; }

        public GnFlag spq000001 { get; set; }







    }
}