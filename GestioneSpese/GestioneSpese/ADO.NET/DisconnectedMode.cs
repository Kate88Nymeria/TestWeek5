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
    public class DisconnectedMode
    {
        static IConfigurationRoot config = new ConfigurationBuilder()
                                           .SetBasePath(Directory.GetCurrentDirectory())
                                           .AddJsonFile("appsettings.json")
                                           .Build();

        public static string connectionStringSQL = config.GetConnectionString("GestioneSpese");
        private static SqlConnection conn;
        public static DataSet speseDs = new DataSet();
        private static SqlDataAdapter speseAdapter;

        static DisconnectedMode()
        {
            conn = new SqlConnection(connectionStringSQL);
        }

        public static void PopolaDataSet()
        {
            Console.WriteLine("Attendere prego...");
            try
            {
                conn.Open();
                if (conn.State != ConnectionState.Open)
                {
                    Console.WriteLine("Non connesso al database");
                }

                speseAdapter = InitCustomersDataSetAndAdapter(conn, speseDs);

                conn.Close();
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"SQL Exception: {sqlEx.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Generic Exception: {ex.Message}");
            }
            finally
            {
                conn.Close();
            }
        }

        public static void StampaListaSpese()
        {
            Console.Clear();
            Console.WriteLine("========== ELENCO SPESE ==========");
            Console.WriteLine();
            string titolo = $"{"Id",-12} {"Data",-15}{"Descrizione",-20}{"Utente",20}{"Importo",15}{"Approvato",15}";
            Console.WriteLine(titolo);
            Console.WriteLine(new string('-', 120));
            foreach (DataRow row in speseDs.Tables["Spese"].Rows)
            {
                if (row.RowState != DataRowState.Deleted)
                {
                    DateTime.TryParse($"{row["Data"]}", out DateTime data);

                    Spesa spesa = new Spesa
                    {
                        Id = (int)row["Id"],
                        Data = data,
                        Descrizione = row["Descrizione"].ToString(),
                        Utente = row["Utente"].ToString(),
                        Importo = (decimal)row["Importo"],
                        Approvato = (bool)row["Approvato"]
                    };

                    string stampa = $"[ {spesa.Id + " ]",-10} {spesa.Data.ToShortDateString(),-15}{spesa.Descrizione,-20}{spesa.Utente,20}{spesa.Importo,15}{spesa.StampaApprovato(),11}";
                    Console.WriteLine(stampa);
                }
                else
                    Console.WriteLine("Deleted row...");
            }
        }

        //public static void InserisciNuovaSpesa()
        //{
        //    Spesa nuovaSpesa = Forms.CreaNuovaSpesa();
        //    DataRow newRow = speseDs.Tables["Spese"].NewRow();

        //    newRow["Data"] = nuovaSpesa.Data;
        //    newRow["CategoriaID"] = nuovaSpesa.Categoria.CategoriaID;
        //    newRow["Descrizione"] = nuovaSpesa.Descrizione;
        //    newRow["Utente"] = nuovaSpesa.Utente;
        //    newRow["Importo"] = nuovaSpesa.Importo;
        //    newRow["Approvato"] = nuovaSpesa.Approvato;

        //    speseDs.Tables["Spese"].Rows.Add(newRow);
        //}

        public static void AggiornaDatabase()
        {
            Console.WriteLine("Attendere prego...");

            try
            {
                conn.Open();
                if (conn.State != ConnectionState.Open)
                {
                    Console.WriteLine("Non connesso al database");
                }

                speseAdapter.SelectCommand.Connection = conn;
                speseAdapter.InsertCommand.Connection = conn;
                //speseAdapter.UpdateCommand.Connection = conn;
                speseAdapter.DeleteCommand.Connection = conn;

                speseAdapter.Update(speseDs, "Spese"); //aggiornamento
                speseDs.Reset(); //svuotato il DataSet
                speseAdapter.Fill(speseDs, "Spese"); //riempito nuovamente

                conn.Close();

                Console.WriteLine();
                Console.WriteLine("Database aggiornato correttamente");
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"SQL Exception: {sqlEx.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Generic Exception: {ex.Message}");
            }
            finally
            {
                conn.Close();
            }
        }

        #region METODI DI SUPPORTO
        private static SqlDataAdapter InitCustomersDataSetAndAdapter(SqlConnection conn, DataSet ds)
        {
            SqlDataAdapter adapter = new SqlDataAdapter();

            adapter.SelectCommand = GenerateSelectCommand(conn);
            adapter.InsertCommand = GenerateInsertCommand(conn);
            //adapter.UpdateCommand = GenerateUpdateCommand(conn);
            adapter.DeleteCommand = GenerateDeleteCommand(conn);

            adapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;

            adapter.Fill(ds, "Spese");

            return adapter;
        }

        private static SqlCommand GenerateSelectCommand(SqlConnection conn)
        {
            string querySql = "SELECT * FROM Spese";
            SqlCommand spesaSelectCommand = new SqlCommand(querySql, conn);
            spesaSelectCommand.CommandType = CommandType.Text;

            return spesaSelectCommand;
        }

        private static SqlCommand GenerateInsertCommand(SqlConnection conn)
        {
            string querySql = "INSERT INTO Spese " +
                              "VALUES (@Data, @CategoriaID, @Descrizione, @Utente, @Importo, @Approvato)";
            SqlCommand spesaInsertCommand = new SqlCommand(querySql, conn);
            spesaInsertCommand.CommandType = CommandType.Text;

            spesaInsertCommand.Parameters.Add(new SqlParameter("@Data", SqlDbType.DateTime, 0, "Data"));
            spesaInsertCommand.Parameters.Add(new SqlParameter("@CategoriaID", SqlDbType.Int, 0, "CategoriaID"));
            spesaInsertCommand.Parameters.Add(new SqlParameter("@Descrizione", SqlDbType.NVarChar, 500, "Descrizione"));
            spesaInsertCommand.Parameters.Add(new SqlParameter("@Utente", SqlDbType.NVarChar, 100, "Utente"));
            spesaInsertCommand.Parameters.Add(new SqlParameter("@Importo", SqlDbType.Decimal, 0, "Importo"));
            spesaInsertCommand.Parameters.Add(new SqlParameter("@Approvato", SqlDbType.Bit, 0, "Approvato"));

            return spesaInsertCommand;
        }

        //private static SqlCommand GenerateUpdateCommand(SqlConnection conn)
        //{
        //    throw new NotImplementedException(SqlConnection conn);
        //}

        private static SqlCommand GenerateDeleteCommand(SqlConnection conn)
        {
            string querySql = "DELETE FROM Spese " +
                              "WHERE Id = @Id";

            SqlCommand spesaDeleteCommand = new SqlCommand(querySql, conn);
            spesaDeleteCommand.CommandType = CommandType.Text;

            spesaDeleteCommand.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int, 0, "Id"));

            return spesaDeleteCommand;
        }

        #endregion
    }
}
