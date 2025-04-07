using System;
using System.Threading.Tasks;
using MySqlConnector;

namespace AzureMySql
{
    class MySqlCreate
    {
        static async Task Main(string[] args)
        {
            Centralita centralita = new();

            // Registrar llamadas
            LlamadaLocal llamada1 = new LlamadaLocal("8091234567", "8097654321", 60);
            LlamadaProvincial llamadaP1 = new LlamadaProvincial("8091234567", "8297654321", 120, 1);
            LlamadaProvincial llamadaP2 = new LlamadaProvincial("8295555555", "8097777777", 90, 2);
            centralita.RegistrarLlamada(llamada1);
            centralita.RegistrarLlamada(llamadaP1);
            centralita.RegistrarLlamada(llamadaP2);

            // Mostrar llamadas e informe
            Console.WriteLine("Llamadas registradas:");
            centralita.MostrarLlamadas();
            centralita.GenerarInforme();

            var builder = new MySqlConnectionStringBuilder
            {
                Server = "localhost",
                Database = "centralita",
                UserID = "root",
                Password = "Theboss0910$",
            };

            using (var conn = new MySqlConnection(builder.ConnectionString))
            {
                Console.WriteLine("Opening connection");
                await conn.OpenAsync();

                using (var command = conn.CreateCommand())
                {

                    command.CommandText = @"INSERT INTO llamadas (Origen, Destino, Duracion, Precio) VALUES (@origen1, @destino1, @duracion1, @precio1),
                        (@origen2, @destino2, @duracion2, @precio2), (@origen3, @destino3, @duracion3, @precio3);";
                    command.Parameters.AddWithValue("@origen1", llamada1.Destino);
                    command.Parameters.AddWithValue("@destino1", llamada1.Origen);
                    command.Parameters.AddWithValue("@duracion1", llamada1.Duracion);
                    command.Parameters.AddWithValue("@precio1", llamada1.CalcularCosto());
                    command.Parameters.AddWithValue("@origen2", llamadaP1.Destino);
                    command.Parameters.AddWithValue("@destino2", llamadaP1.Origen);
                    command.Parameters.AddWithValue("@duracion2", llamadaP1.Duracion);
                    command.Parameters.AddWithValue("@precio2", llamadaP1.CalcularCosto());
                    command.Parameters.AddWithValue("@origen3", llamadaP2.Destino);
                    command.Parameters.AddWithValue("@destino3", llamadaP2.Origen);
                    command.Parameters.AddWithValue("@duracion3", llamadaP2.Duracion);
                    command.Parameters.AddWithValue("@precio3", llamadaP2.CalcularCosto());

                    int rowCount = await command.ExecuteNonQueryAsync();
                    Console.WriteLine(String.Format("Number of rows inserted={0}", rowCount));
                }

                // connection will be closed by the 'using' block
                Console.WriteLine("Closing connection");
            }

            Console.WriteLine("Press RETURN to exit");
            Console.ReadLine();
        }
    }
}