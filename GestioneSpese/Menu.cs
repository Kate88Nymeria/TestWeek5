using GestioneSpese.ADO.NET;
using GestioneSpese.EF;
using GestioneSpese.Library.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestioneSpese
{
    public class Menu
    {
        public static void Start()
        {
            Console.WriteLine();

            int scelta;
            bool continuare = true;

            do
            {
                Console.WriteLine("[ 1 ] - Inserisci Nuova Spesa");
                Console.WriteLine("[ 2 ] - Approva Spesa Esistente");
                Console.WriteLine("[ 3 ] - Cancella Spesa Esistente");
                Console.WriteLine("[ 4 ] - Visualizza Elenco Spese Approvate");
                Console.WriteLine("[ 5 ] - Visualizza Elenco Spese di un Utente");
                Console.WriteLine("[ 6 ] - Visualizza Totale Spese per Categoria");
                Console.WriteLine();
                Console.WriteLine("[ 7 ] - Usa Ado.Net Connected Mode (NON TERMINATO)");
                Console.WriteLine("[ 8 ] - Usa Ado.Net Disconnected Mode (NON TERMINATO)");
                Console.WriteLine();
                Console.WriteLine("[ 0 ] - Esci");

                scelta = Helpers.CheckInt();

                switch (scelta)
                {
                    case 1:
                        EFCodeFirst.InserisciNuovaSpesa();
                        Helpers.ContinuaEsecuzione();
                        Console.Clear();
                        break;
                    case 2:
                        EFCodeFirst.ApprovaSpesa();
                        Helpers.ContinuaEsecuzione();
                        Console.Clear();
                        break;
                    case 3:
                        EFCodeFirst.EliminaSpesa();
                        Helpers.ContinuaEsecuzione();
                        Console.Clear();
                        break;
                    case 4:
                        EFCodeFirst.MostraSpeseApprovate();
                        Helpers.ContinuaEsecuzione();
                        Console.Clear();
                        break;
                    case 5:
                        EFCodeFirst.MostraSpeseUtente();
                        Helpers.ContinuaEsecuzione();
                        Console.Clear();
                        break;
                    case 6:
                        EFCodeFirst.MostraTotaleSpesePerCategoria();
                        Helpers.ContinuaEsecuzione();
                        Console.Clear();
                        break;
                    case 7:
                        AdoNetConnectedMode();
                        Console.Clear();
                        break;
                    case 8:
                        AdoNetDisconnectedMode();
                        Console.Clear();
                        break;
                    case 0:
                        Console.WriteLine();
                        continuare = false;
                        break;
                    default:
                        Console.WriteLine("Errore: Scelta non ammessa");
                        break;
                }

            } while (continuare);
        }

        public static void AdoNetConnectedMode()
        {
            Console.Clear();
            Console.WriteLine("========== ADO.NET CONNECTED MODE ==========");

            int scelta;
            bool continuare = true;

            do
            {
                Console.WriteLine("[ 1 ] - Visualizza Elenco Spese");
                //Console.WriteLine("[ 2 ] - Inserisci Nuova Spesa");
                Console.WriteLine("[ 3 ] - Cancella Spesa Esistente");
                Console.WriteLine("[ 0 ] - Esci");

                scelta = Helpers.CheckInt();

                switch (scelta)
                {
                    case 1:
                        ConnectedMode.StampaListaSpese();
                        Helpers.ContinuaEsecuzione();
                        Console.Clear();
                        break;
                    //case 2:
                    //    ConnectedMode.InserisciNuovaSpesa(); //DA TERMINARE
                    //    Helpers.ContinuaEsecuzione();
                    //    Console.Clear();
                    //    break;
                    case 3:
                        ConnectedMode.StampaListaSpese();
                        Console.WriteLine();
                        ConnectedMode.EliminaSpesa();
                        Helpers.ContinuaEsecuzione();
                        Console.Clear();
                        break;
                    case 0:
                        Console.WriteLine();
                        continuare = false;
                        break;
                    default:
                        Console.WriteLine("Errore: Scelta non ammessa");
                        break;
                }

            } while (continuare);
        }

        public static void AdoNetDisconnectedMode()
        {
            Console.Clear();
            Console.WriteLine("========== ADO.NET DISCONNECTED MODE ==========");

            DisconnectedMode.PopolaDataSet();

            int scelta;
            bool continuare = true;

            do
            {
                Console.WriteLine("[ 1 ] - Visualizza Elenco Spese");
                //Console.WriteLine("[ 2 ] - Inserisci Nuova Spesa");
                Console.WriteLine("[ 3 ] - Cancella Spesa Esistente");
                Console.WriteLine("[ 4 ] - Aggiorna Database");
                Console.WriteLine("[ 0 ] - Esci");

                scelta = Helpers.CheckInt();

                switch (scelta)
                {
                    case 1:
                        DisconnectedMode.StampaListaSpese();
                        Helpers.ContinuaEsecuzione();
                        Console.Clear();
                        break;
                    case 2:
                        //DisconnectedMode.InserisciNuovaSpesa(); //DA TERMINARE
                        Helpers.ContinuaEsecuzione();
                        Console.Clear();
                        break;
                    case 3:
                        DisconnectedMode.StampaListaSpese();
                        Console.WriteLine();
                        DisconnectedMode.EliminaSpesa();
                        Helpers.ContinuaEsecuzione();
                        Console.Clear();
                        break;
                    case 4:
                        DisconnectedMode.AggiornaDatabase();
                        Helpers.ContinuaEsecuzione();
                        Console.Clear();
                        break;
                    case 0:
                        Console.WriteLine();
                        continuare = false;
                        break;
                    default:
                        Console.WriteLine("Errore: Scelta non ammessa");
                        break;
                }

            } while (continuare);
        }
    }
}
