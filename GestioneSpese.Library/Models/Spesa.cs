using System;

namespace GestioneSpese.Library.Models
{
    public class Spesa
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public Categoria Categoria { get; set; }
        public string Descrizione { get; set; }
        public string Utente { get; set; }
        public decimal Importo { get; set; }
        public bool Approvato { get; set; }


        public Spesa()
        {
 
        }

        public Spesa(string desc, string user, decimal imp)
        {
            Descrizione = desc;
            Utente = user;
            Importo = imp;
            Data = DateTime.Now;
            Approvato = false;
        }

        public Spesa(string desc, string user, decimal imp, Categoria cat) : this(desc, user, imp)
        {
            Categoria = cat;
        }


        public string StampaApprovato()
        {
            if (Approvato)
            {
                return "Y";
            }
            else
            {
                return "N";
            }
        }

        public override string ToString()
        {
            string stampa = $"[ {Id + " ]", -10} {Data.ToShortDateString(), -15}[{Categoria.Descrizione + "]", -18}{Descrizione, -20}{Utente, 20}{Importo, 15}{StampaApprovato(), 11}";
            return stampa;
        }
    }
}
