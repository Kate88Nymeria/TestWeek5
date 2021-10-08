using GestioneSpese.Library.Utilities;
using System;

namespace GestioneSpese
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("========== BENVENUTO NEL GESTORE SPESE ==========");

            Menu.Start();

            Console.WriteLine("========== Arrivederci ==========");
            Helpers.ContinuaEsecuzione();
        }
    }
}
