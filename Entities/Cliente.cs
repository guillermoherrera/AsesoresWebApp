using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsesoresAPI.Entities
{
    public class Cliente
    {
        public int sistema { get; set; }
        public int usuarioRegistra { get; set; }
        public string userID { get; set; }
        public string solicitudID { get; set; }
        public string nombre { get; set; }
        public string apellidoPat { get; set; }
        public string apellidoMat { get; set; }
        public string TelCel { get; set; }
        public string rfc { get; set; }
        public string fechaNac { get; set; }
        public string nombreVialidad { get; set; }
        public string curp { get; set; }
        public string cp { get; set; }
        public string nombreAdicional { get; set; }
        public string estado { get; set; }
        public string municipio { get; set; }
        public string colonia { get; set; }
        public string ciudad { get; set; }

    }
}
