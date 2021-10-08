using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestioneSpese.Library.Models
{
    public class Categoria
    {
        public int CategoriaID { get; set; }
        public string Descrizione { get; set; }

        public IList<Spesa> Spese { get; set; }
    }
}
