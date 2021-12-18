using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Farmacia.Models
{
    public class Remedio
    {
        public string Nome { get; set; }
        public DateTime Validade { get; set; }
        public int FarmaciaID { get; set; }
        public string FarmaciaNome { get; set; }
        public int CategoriaID { get; set; }
        public string CategoriaNome { get; set; }
        public int ID { get; set; }

    }
}
