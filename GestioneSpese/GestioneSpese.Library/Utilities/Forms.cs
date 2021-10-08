using GestioneSpese.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestioneSpese.Library.Utilities
{
    public class Forms
    {
        public static Spesa CreaNuovaSpesa()
        {
            Console.WriteLine("========== INSERIMENTO NUOVA SPESA ==========");
            Console.WriteLine();

            Console.WriteLine("Inserisci una descrizione della spesa");
            string desc = Helpers.CheckNullString();
            Console.WriteLine();
            Console.WriteLine("Inserisci il nome utente");
            string user = Helpers.CheckNullString();
            Console.WriteLine();
            Console.WriteLine("Inserisci l'importo della spesa");
            decimal imp = Helpers.CheckDecimal();

            Spesa nuovaSpesa = new Spesa(desc, user, imp);

            return nuovaSpesa;
        }

        public static string InserisciUtente()
        {
            Console.WriteLine("Inserisci il nome utente di cui visualizzare le spese:");
            string user = Helpers.CheckNullString();

            return user;
        }

        public static int ApprovaSpesaDaId()
        {
            Console.WriteLine("Inserisci l'Id della spesa da approvare");
            int id = Helpers.CheckInt();

            return id;
        }

        public static int RimuoviSpesa()
        {
            Console.WriteLine("Inserisci l'Id della spesa da eliminare");
            int id = Helpers.CheckInt();

            return id;
        }

        public static int ScegliCategoria()
        {
            int idCat = 0;

            do
            {
                Console.WriteLine();
                Console.WriteLine("Scegli categoria:");
                Console.WriteLine();
                Console.WriteLine($"[1] - Alimentare");
                Console.WriteLine($"[2] - Informatica");
                Console.WriteLine($"[3] - Cosmesi");
                Console.WriteLine($"[4] - Viaggio");
                Console.WriteLine($"[5] - Business");
                Console.WriteLine($"[6] - Cultura");
                Console.WriteLine($"[7] - Sport");
                Console.WriteLine($"[8] - Salute");
                Console.WriteLine($"[9] - Altro");

                idCat = Helpers.CheckInt();
            }
            while (idCat < 1 && idCat > 9);

            return idCat;
        }

    }
}
