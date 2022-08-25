

using System.Data.SqlClient;

namespace ConsoleApp
{
    public static class InicioSesion 
    {
        public const string ConnectionString = "Server=DESKTOP-CJ1ULQ0;Database=SistemaGestion;Trusted_Connection=True";
        public static void IniciarSesion()
        {
            Console.WriteLine("Ingrese Nombre de Usuario :   ");
            var nom = Console.ReadLine();

            Console.WriteLine("Ingrese Contraseña :   ");
            var pass = Console.ReadLine();

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                var query = "SELECT * FROM USUARIO WHERE NombreUsuario = @nomusu AND Contraseña = @contra;";

                var PARAMusu = new SqlParameter("nomusu", System.Data.SqlDbType.VarChar) { Value = nom };
                var PARAMpass = new SqlParameter("contra", System.Data.SqlDbType.VarChar) { Value = pass };

                conn.Open();
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    command.Parameters.Add(PARAMusu);
                    command.Parameters.Add(PARAMpass);

                    SqlDataReader dr = command.ExecuteReader();

                    if (dr.Read())
                    {
                        Console.WriteLine($"Login Exitoso :  {nom} ");
                    }
                    else Console.WriteLine("Usuario Incorrecto");
                }
                conn.Close();
            }
            
           
        
        
        }

    }
}
