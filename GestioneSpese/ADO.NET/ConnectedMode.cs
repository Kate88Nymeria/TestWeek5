using GestioneSpese.Library.Models;
using GestioneSpese.Library.Utilities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestioneSpese.ADO.NET
{
    public class ConnectedMode
    {
        static IConfigurationRoot config = new ConfigurationBuilder()
                                           .SetBasePath(Directory.GetCurrentDirectory())
                                           .AddJsonFile("appsettings.json")
                                           .Build();

        static string connectionStringSQL = config.GetConnectionString("GestioneSpese");

        public static void StampaListaSpese()
        {
            using SqlConnection conn = new SqlConnection(connectionStringSQL);

            try
            {
                Console.WriteLine("Attendere prego...");
                conn.Open();

                if(conn.State != ConnectionState.Open)
                {
                    Console.WriteLine("Non connesso al database");
                    return;
                }

                Console.Clear();
                string querySql = "SELECT * " +
                                  "FROM Spese " +
                                  "ORDER BY Data DESC";

                SqlCommand readCommand = new SqlCommand(querySql, conn);
                readCommand.CommandType = CommandType.Text;

                SqlDataReader reader = readCommand.ExecuteReader();

                Console.WriteLine("========== ELENCO SPESE ==========");
                Console.WriteLine();
                string titolo = $"{"Id",-12} {"Data",-15}{"Descrizione",-20}{"Utente",20}{"Importo",15}{"Approvato",15}";
                Console.WriteLine(titolo);
                Console.WriteLine(new string('-', 120));

                while (reader.Read())
                {
                    DateTime.TryParse($"{reader["Data"]}", out DateTime data);

                    Spesa spesa = new Spesa
                    {
                        Id = (int)reader["Id"],
                        Data = data,
                        Descrizione = reader["Descrizione"].ToString(),
                        Utente = reader["Utente"].ToString(),
                        Importo = (decimal)reader["Importo"],
                        Approvato = (bool)reader["Approvato"]
                    };

                    string stampa = $"[ {spesa.Id + " ]",-10} {spesa.Data.ToShortDateString(),-15}{spesa.Descrizione,-20}{spesa.Utente,20}{spesa.Importo,15}{spesa.StampaApprovato(),11}";
                    Console.WriteLine(stampa);
                }
            }
            catch(SqlException ex)
            {
                Console.WriteLine($"Errore Sql: {ex.Message}");
            }
            finally
            {
                conn.Close();
            }
        }

        //public static void InserisciNuovaSpesa()
        //{
        //    using SqlConnection conn = new SqlConnection(connectionStringSQL);

        //    try
        //    {
        //        Console.WriteLine("Attendere prego...");
        //        conn.Open();

        //        if (conn.State != ConnectionState.Open)
        //        {
        //            Console.WriteLine("Non connesso al database");
        //            return;
        //        }

        //        Console.Clear();
        //        string querySql = "INSERT INTO Spese " +
        //                          "VALUES (@Data, @CategoriaID, @Descrizione, @Utente, @Importo, @Approvato)";

        //        SqlCommand insertCommand = new SqlCommand(querySql, conn);
        //        insertCommand.CommandType = CommandType.Text;

        //        Spesa nuovaSpesa = Forms.CreaNuovaSpesa();

        //        insertCommand.Parameters.AddWithValue("@Data", nuovaSpesa.Data);
        //        insertCommand.Parameters.AddWithValue("@CategoriaID", nuovaSpesa.Categoria.CategoriaID);
        //        insertCommand.Parameters.AddWithValue("@Descrizione", nuovaSpesa.Descrizione);
        //        insertCommand.Parameters.AddWithValue("@Utente", nuovaSpesa.Utente);
        //        insertCommand.Parameters.AddWithValue("@Importo", nuovaSpesa.Importo);
        //        insertCommand.Parameters.AddWithValue("@Approvato", nuovaSpesa.Approvato);

        //        int result = insertCommand.ExecuteNonQuery();

        //        if(result != 1)
        //            Console.WriteLine("Errore: Spesa non inserita");
        //        else
        //            Console.WriteLine("Spesa inserita con successo");

        //    }
        //    catch (SqlException ex)
        //    {
        //        Console.WriteLine($"Errore Sql: {ex.Message}");
        //    }
        //    finally
        //    {
        //        conn.Close();
        //    }
        //}

        public static void EliminaSpesa()
        {
            using SqlConnection conn = new SqlConnection(connectionStringSQL);

            try
            {
                Console.WriteLine("Attendere prego...");
                conn.Open();

                if (conn.State != ConnectionState.Open)
                {
                    Console.WriteLine("Non connesso al database");
                    return;
                }

                string querySql = "DELETE FROM Spese " +
                                  "WHERE Id = @Id";

                int idSpesa = Forms.RimuoviSpesa();

                SqlCommand deleteCommand = new SqlCommand(querySql, conn);
                deleteCommand.CommandType = CommandType.Text;

                SqlParameter idParam = new SqlParameter();
                idParam.ParameterName = "@Id";
                idParam.Value = idSpesa;
                idParam.DbType = DbType.Int32;
                deleteCommand.Parameters.Add(idParam);

                int result = deleteCommand.ExecuteNonQuery();

                if (result != 1)
                    Console.WriteLine("Errore: Spesa non eliminata");
                else
                    Console.WriteLine("Spesa eliminata con successo");

            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Errore Sql: {ex.Message}");
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
