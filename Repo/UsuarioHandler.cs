using Proyecto_Final.Models;
using Proyecto_Final.DTOs;
using System.Data.SqlClient;

namespace Proyecto_Final.Repo
{
    public class UsuarioHandler : DBHandler
    {
        public static bool CrearUsuario(Usuario usuario)
        {
            bool devolucion;

            Usuario usuarioTest = GetUsuarioConParam(usuario.NombreUsuario);

            

            if (string.IsNullOrEmpty(usuario.Nombre) || string.IsNullOrEmpty(usuario.Apellido) || string.IsNullOrEmpty(usuario.NombreUsuario) || string.IsNullOrEmpty(usuario.Contraseña) || string.IsNullOrEmpty(usuario.Mail))
            {
                throw new Exception("ERROR :  Datos Faltantes");

            }

            else if (usuarioTest.Id != 0)
            {
                throw new Exception("ERROR :  Nombre de Usuario ya existente");
            }

            
            else devolucion = true; 

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                string query = "INSERT INTO USUARIO (NOMBRE,APELLIDO,NOMBREUSUARIO,CONTRASEÑA,MAIL) VALUES (@nombre,@apellido,@nombreusuario,@contraseña,@mail)";

                SqlParameter nombreParam = new SqlParameter("nombre", System.Data.SqlDbType.VarChar) { Value = usuario.Nombre };
                SqlParameter apellidoParam = new SqlParameter("apellido", System.Data.SqlDbType.VarChar) { Value = usuario.Apellido };
                SqlParameter nombreUsuParam = new SqlParameter("nombreusuario", System.Data.SqlDbType.VarChar) { Value = usuario.NombreUsuario };
                SqlParameter contraseñaParam = new SqlParameter("contraseña", System.Data.SqlDbType.VarChar) { Value = usuario.Contraseña };
                SqlParameter mailParam = new SqlParameter("mail", System.Data.SqlDbType.VarChar) { Value = usuario.Mail };

                conn.Open();

                using (SqlCommand command = new SqlCommand(query, conn))
                {

                    command.Parameters.Add(nombreParam);
                    command.Parameters.Add(apellidoParam);
                    command.Parameters.Add(nombreUsuParam);
                    command.Parameters.Add(contraseñaParam);
                    command.Parameters.Add(mailParam);

                    command.ExecuteNonQuery();
                   
                }
                conn.Close();



            }
            return devolucion;
        }
        public static Usuario GetUsuarioConParam(string nombreUsuario)
        {
            Usuario usu = new Usuario();
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                string query = "SELECT * FROM USUARIO WHERE NOMBREUSUARIO= @nombreUsuario";

                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    command.Parameters.AddWithValue("nombreUsuario", nombreUsuario);

                    conn.Open();
                    using (SqlDataReader dr = command.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                usu.Id = Convert.ToInt32(dr["ID"]);
                                usu.Nombre = Convert.ToString(dr["Nombre"]);
                                usu.Apellido = Convert.ToString(dr["Apellido"]);
                                usu.NombreUsuario = Convert.ToString(dr["NombreUsuario"]);
                                usu.Mail = Convert.ToString(dr["Mail"]);
                                usu.Contraseña = Convert.ToString(dr["Contraseña"]);
                            }
                        }
                        else
                        {
                            usu.Id = 0;
                            usu.Nombre = "";
                            usu.Apellido = "";
                            usu.NombreUsuario = "";
                            usu.Mail = "";
                            usu.Contraseña = "";
                        }
                    }
                    conn.Close();
                }

            }
            return usu;
        }
        public static Usuario GetUsuarioConParam(string nombreUsuario, string contraseña )
        {
            Usuario usu = new Usuario();
            using(SqlConnection conn = new SqlConnection(ConnectionString))
            {
                string query = "SELECT * FROM USUARIO WHERE NOMBREUSUARIO= @nombreUsuario AND CONTRASEÑA=@contraseña";

                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (SqlDataReader dr = command.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                usu.Id = Convert.ToInt32(dr["ID"]);
                                usu.Nombre = Convert.ToString(dr["Nombre"]);
                                usu.Apellido = Convert.ToString(dr["Apellido"]);
                                usu.NombreUsuario = Convert.ToString(dr["NombreUsuario"]);
                                usu.Mail = Convert.ToString(dr["Mail"]);
                                usu.Contraseña = Convert.ToString(dr["Contraseña"]);
                            }
                        }
                        else
                        {
                            throw new Exception("ERROR : Usuario Inexistente");
                        }
                    }
                    conn.Close();
                }
            
            }
            return usu;
        }
        public static List<Usuario> GetUsuario()
        {
            List<Usuario> usuarios = new List<Usuario>();
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                string query = "SELECT * FROM USUARIO";

                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (SqlDataReader dr = command.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                Usuario usuario = new Usuario();
                                usuario.Id = Convert.ToInt32(dr["ID"]);
                                usuario.Nombre = Convert.ToString(dr["Nombre"]);
                                usuario.Apellido = Convert.ToString(dr["Apellido"]);
                                usuario.NombreUsuario = Convert.ToString(dr["NombreUsuario"]);
                                usuario.Mail = Convert.ToString(dr["Mail"]);
                                usuario.Contraseña = Convert.ToString(dr["Contraseña"]);

                                usuarios.Add(usuario);
                            }
                        }
                    }
                    conn.Close();
                }
            }
            return usuarios;
        }
        public static bool ModificarUsuario(DTOUsuario usu)
        {
            bool devolucion = false;
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                string query = "UPDATE USUARIO SET NOMBRE=@nombre, APELLIDO=@apellido, NOMBREUSUARIO= @nombreusuario, CONTRASEÑA=@contraseña WHERE Id=@id";


                SqlParameter nombreParam = new SqlParameter("nombre", System.Data.SqlDbType.VarChar) { Value = usu.Nombre };
                SqlParameter apelliParam = new SqlParameter("apellido", System.Data.SqlDbType.VarChar) { Value = usu.Apellido };
                SqlParameter nombreUsuParam = new SqlParameter("nombreusuario", System.Data.SqlDbType.VarChar) { Value = usu.NombreUsuario };
                SqlParameter contraParam = new SqlParameter("contraseña", System.Data.SqlDbType.VarChar) { Value = usu.Contraseña };
                SqlParameter idParam = new SqlParameter("id", System.Data.SqlDbType.BigInt) { Value=usu.Id };

                conn.Open();

                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    command.Parameters.Add(nombreParam);
                    command.Parameters.Add(apelliParam);
                    command.Parameters.Add(idParam);
                    command.Parameters.Add(contraParam);
                    command.Parameters.Add(nombreUsuParam);
                    int columnasAfec = command.ExecuteNonQuery();
                    if (columnasAfec > 0) devolucion = true;
                }
                conn.Close();
            }

            return devolucion;
        }
    }
}

