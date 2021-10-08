using GestioneSpese.Library.Models;
using GestioneSpese.Library.Utilities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestioneSpese.EF
{
    public class EFCodeFirst
    {
        #region STAMPE
        public static void MostraSpeseApprovate()
        {
            Console.Clear();

            Console.WriteLine("========== SPESE APPROVATE ==========");
            Console.WriteLine();

            IEnumerable<Spesa> speseApprovate = ElencaSpese().Where(s => s.Approvato);

            if(speseApprovate.Count() > 0)
            {
                foreach (Spesa s in speseApprovate)
                {
                    Console.WriteLine(s);
                }
            }
            else
                Console.WriteLine("Nessuna spesa inserita");
        }

        public static void MostraSpeseUtente()
        {
            Console.WriteLine();
            string utente = Forms.InserisciUtente();

            Console.Clear();

            Console.WriteLine($"========== SPESE DELL'UTENTE \"{utente}\" ==========");

            IEnumerable<Spesa> speseUtente = ElencaSpese().Where(s => s.Utente == utente);

            if (speseUtente.Count() > 0)
            {
                foreach (Spesa s in speseUtente)
                {
                    Console.WriteLine(s);
                }
            }
            else
                Console.WriteLine("Nessuna spesa inserita per l'utente indicato");
        }


        private static IEnumerable<Spesa> ElencaSpese()
        {
            GestioneSpeseContext ctx = new GestioneSpeseContext();
            Console.WriteLine();
            string titolo = $"{"Id",-12} {"Data",-15}{"Categoria",-19}{"Descrizione", -20}{"Utente",20}{"Importo",15}{"Approvato",15}";
            Console.WriteLine(titolo);
            Console.WriteLine(new string('-', 120));

            IEnumerable<Spesa> spese = ctx.Spese.Include(s => s.Categoria).OrderByDescending(s => s.Data);

            return spese;
        }

        private static IEnumerable<Spesa> StampaSpese()
        {
            Console.Clear();

            Console.WriteLine("========== ELENCO SPESE ==========");
            Console.WriteLine();

            IEnumerable<Spesa> spese = ElencaSpese();

            if (spese.Count() > 0)
            {
                foreach (Spesa s in spese)
                {
                    Console.WriteLine(s);
                }
            }
            else
                Console.WriteLine("Nessuna spesa inserita");

            return spese;
        }

        public static void MostraTotaleSpesePerCategoria()
        {
            Console.Clear();

            Console.WriteLine("========== TOTALE SPESE PER CATEGORIA ==========");
            Console.WriteLine();

            GestioneSpeseContext ctx = new GestioneSpeseContext();
            Console.WriteLine();

            IEnumerable<Categoria> categorie = ctx.Categorie.Include(c => c.Spese);

            foreach(Categoria c in categorie)
            {
                decimal totale = c.Spese.Sum(s => s.Importo);

                Console.WriteLine($"[ {c.Descrizione + " ]", -17}{"--->", -10}{totale + " euro", -20}");
            }
        }

        #endregion

        public static void InserisciNuovaSpesa()
        {
            using (GestioneSpeseContext ctx = new GestioneSpeseContext())
            {
                Spesa nuovaSpesa = Forms.CreaNuovaSpesa();

                int idCategoria = Forms.ScegliCategoria();

                Categoria cat = ctx.Categorie.Find(idCategoria);

                Spesa spesaDaAggiuntere = new Spesa(nuovaSpesa.Descrizione, nuovaSpesa.Utente, nuovaSpesa.Importo, cat);

                ctx.Spese.Add(spesaDaAggiuntere);
                ctx.SaveChanges();

                Console.WriteLine("Spesa inserita con successo");
            }
        }

        public static void ApprovaSpesa()
        {
            IEnumerable<Spesa> spese = StampaSpese();
            if (spese.Count() == 0)
                return;

            using (GestioneSpeseContext ctx = new GestioneSpeseContext())
            {
                Console.WriteLine();
                int idSpesa = Forms.ApprovaSpesaDaId();

                Spesa spesaDaEliminare = ctx.Spese.Find(idSpesa);

                if (spesaDaEliminare != null)
                {
                    if (!spesaDaEliminare.Approvato)
                    {
                        spesaDaEliminare.Approvato = true;
                        ctx.SaveChanges();
                        Console.WriteLine("Spesa approvata con successo");
                    }
                    else
                        Console.WriteLine("Errore: questa spesa è già approvata");
                }
                else
                    Console.WriteLine("Id della spesa inserito non trovato");
            }
        }

        public static void EliminaSpesa()
        {
            IEnumerable<Spesa> spese = StampaSpese();
            if (spese.Count() == 0)
                return;

            using (GestioneSpeseContext ctx = new GestioneSpeseContext())
            {
                Console.WriteLine();
                int idSpesa = Forms.RimuoviSpesa();

                Spesa spesaDaEliminare = ctx.Spese.Find(idSpesa);

                if(spesaDaEliminare != null)
                {
                    ctx.Spese.Remove(spesaDaEliminare);
                    ctx.SaveChanges();
                    Console.WriteLine("Spesa eliminata con successo");
                }
                else
                    Console.WriteLine("Id della spesa inserito non trovato");
            }
        }
    }
}
