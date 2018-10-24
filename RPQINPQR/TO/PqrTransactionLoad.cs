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
        public TOTransaction<List<GnPaise>> countries { get; set; }
        /// <summary>
        /// Departmentos
        /// </summary>
        public TOTransaction<List<GnDepar>> states { get; set; }
        /// <summary>
        /// Ciudades
        /// </summary>
        public TOTransaction<List<GnMunic>> cities { get; set; }
        /// <summary>
        /// Digiflag para formulario de pqr
        /// </summary>
        public TOTransaction<GnFlag> digiflag { get; set; }
        /// <summary>
        /// Lista de items 
        /// </summary>
        public TOTransaction<List<GnItem>> pqrType { get; set; } 
        /// <summary>
        /// Lista de items
        /// </summary>
        public TOTransaction<List<GnItem>> pqrSubject { get; set; }
        /// <summary>
        /// Lista de items
        /// </summary>
        public TOTransaction<List<GnItem>> pqrInscription { get; set; }
        /// <summary>
        /// Grupo pertenece
        /// </summary>
        public TOTransaction<List<TOGPerte>> pqrGroup { get; set; }
        /// <summary>
        /// Imagen de empresa
        /// </summary>
        public byte[] pqrImage { get; set; }

        



    }
}