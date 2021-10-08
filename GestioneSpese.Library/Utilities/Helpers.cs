using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestioneSpese.Library.Utilities
{
    public class Helpers
    {
        public static void ContinuaEsecuzione()
        {
            Console.WriteLine();
            Console.WriteLine("Premi un tasto per continuare...");
            Console.ReadKey();
        }

        public static DateTime CheckDate()
        {
            bool isDate = false;
            DateTime date;

            do
            {
                isDate = DateTime.TryParse(Console.ReadLine(), out date);

                if (!isDate)
                {
                    Console.WriteLine("Errore: inserisci un formato di data valido.");
                }
                if (date > DateTime.Now)
                {
                    Console.WriteLine("Errore: non puoi inserire una data successiva all'odierna. Riprova:");
                }
            } while (!isDate || date > DateTime.Now);

            return date;
        }

        public static string CheckNullString()
        {
            string stringa = string.Empty;

            do
            {
                stringa = Console.ReadLine();

                if (String.IsNullOrEmpty(stringa))
                {
                    Console.WriteLine("Errore: non puoi inserire una stringa vuota. Riprova");
                }
            }
            while (String.IsNullOrEmpty(stringa));

            return stringa;
        }

        public static int CheckInt()
        {
            bool isInt = false;
            int numero;

            do
            {
                isInt = Int32.TryParse(Console.ReadLine(), out numero);

                if (!isInt || numero < 0)
                {
                    Console.WriteLine("Errore: inserisci un numero valido positivo. Riprova:");
                }
            } while (!isInt || numero < 0);

            return numero;
        }

        public static decimal CheckDecimal()
        {
            bool isDecimal = false;
            decimal numero;

            do
            {
                isDecimal = decimal.TryParse(Console.ReadLine(), out numero);

                if (!isDecimal || numero <= 0)
                {
                    Console.WriteLine("Errore: inserisci un numero valido positivo. Riprova:");
                }
            } while (!isDecimal || numero <= 0);

            return numero;
        }
    }
}
