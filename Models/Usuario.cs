using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Farmacia.Models
{
    public class Usuario
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public int FarmaciaID { get; set; }
        public string FarmaciaNome { get; set; }
        public int ID { get; set; }

    }
}
