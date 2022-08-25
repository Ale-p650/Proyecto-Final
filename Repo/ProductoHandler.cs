using ConsoleApp.Models;
using Integrando_APIs_con_ADO.NET.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace ConsoleApp
{
    public class ProductoHandler : DBHandler
    {
    
        public static List<Producto> GetProducto()
        {
            List<Producto> products = new List<Producto>();
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                string query = "SELECT * FROM PRODUCTO";
                
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (SqlDataReader dr = command.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                Producto producto = new Producto();
                                producto.Id = Convert.ToInt32(dr["Id"]);
                                producto.Descripciones = Convert.ToString(dr["Descripciones"]);
                                producto.Costo = Convert.ToDouble(dr["Costo"]);
                                producto.PrecioVenta = Convert.ToDouble(dr["PrecioVenta"]);
                                producto.Stock = Convert.ToInt32(dr["Stock"]);
                                producto.IdUsuario = Convert.ToInt32(dr["IdUsuario"]);

                                products.Add(producto);
                            }
                        }
                    }
                    conn.Close();
                }
            }

            return products;
        }

        public static bool CrearProducto(DTOProducto prod)
        {
            bool devolucion = false;
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                string query = "INSERT INTO PRODUCTO (DESCRIPCIONES,COSTO,PRECIOVENTA,STOCK,IDUSUARIO) VALUES (@descrip,@costo,@precioventa,@stock,@idusuario)";

                SqlParameter descripcionesParam = new SqlParameter("descrip", System.Data.SqlDbType.VarChar) { Value= prod.Descripciones };
                SqlParameter costoParam = new SqlParameter("costo", System.Data.SqlDbType.Decimal) { Value = prod.Costo };
                SqlParameter precioParam = new SqlParameter("precioventa", System.Data.SqlDbType.Decimal) { Value = prod.PrecioVenta };
                SqlParameter stockParam = new SqlParameter("stock", System.Data.SqlDbType.Int) { Value = prod.Stock };
                SqlParameter idParam = new SqlParameter("idusuario", System.Data.SqlDbType.Int) { Value = prod.IdUsuario };

                conn.Open();

                using (SqlCommand command = new SqlCommand(query, conn))
                {

                    command.Parameters.Add(descripcionesParam);
                    command.Parameters.Add(costoParam);
                    command.Parameters.Add(precioParam);
                    command.Parameters.Add(stockParam);
                    command.Parameters.Add(idParam);
                    int columnasAfec = command.ExecuteNonQuery();
                    if (columnasAfec > 0) devolucion = true;
                }
                conn.Close();


                
            }
            return devolucion;

        }

        public static bool ModificarProducto(DTOProducto prod)
        {
            bool devolucion = false;
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                string query = "UPDATE PRODUCTO SET DESCRIPCIONES=@descripciones, COSTO=@costo, PRECIOVENTA= @precioventa, STOCK=@stock , IDUSUARIO= @idusuario WHERE Id=@id";


                SqlParameter idParam = new SqlParameter("id", SqlDbType.Int) { Value = prod.Id };

                SqlParameter descripcionesParam = new SqlParameter("descripciones", System.Data.SqlDbType.VarChar) { Value = prod.Descripciones };
                SqlParameter costoParam = new SqlParameter("costo", System.Data.SqlDbType.Decimal) { Value = prod.Costo };
                SqlParameter precioParam = new SqlParameter("precioventa", System.Data.SqlDbType.Decimal) { Value = prod.PrecioVenta };
                SqlParameter stockParam = new SqlParameter("stock", System.Data.SqlDbType.Int) { Value = prod.Stock };
                SqlParameter idusuParam = new SqlParameter("idusuario", System.Data.SqlDbType.Int) { Value = prod.IdUsuario };

                conn.Open();

                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    command.Parameters.Add(idParam);
                    command.Parameters.Add(descripcionesParam);
                    command.Parameters.Add(costoParam);
                    command.Parameters.Add(precioParam);
                    command.Parameters.Add(stockParam);
                    command.Parameters.Add(idusuParam);
                    int columnasAfec = command.ExecuteNonQuery();
                    if (columnasAfec > 0) devolucion = true;
                }
                conn.Close();
            }

            return devolucion;
        }

        public static bool BorrarProducto(int idfrombody)
        {
            bool devolucion = false;

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                string query = "DELETE FROM PRODUCTO WHERE ID=@id ";

                SqlParameter idParam = new SqlParameter("id", SqlDbType.BigInt) { Value=idfrombody };
                conn.Open();

                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    command.Parameters.Add(idParam);
                    int columnasAfec = command.ExecuteNonQuery();
                    if (columnasAfec > 0) devolucion = true;
                }
                conn.Close();
            }
            return devolucion;
        }
    }

}

