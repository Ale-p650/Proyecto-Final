using Integrando_APIs_con_ADO.NET.DTOs;
using ConsoleApp.Models;
using System.Data;
using System.Data.SqlClient;

namespace ConsoleApp
{
    public class VentasHandler : DBHandler
    {
        public static List<Venta> GetVentas()
        {
            List<Venta> ventas = new List<Venta>();
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                string query = "SELECT * FROM VENTA";

                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (SqlDataReader dr = command.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                Venta venta = new Venta();
                                venta.Id = Convert.ToInt32(dr["Id"]);


                                ventas.Add(venta);
                            }
                        }
                    }
                    conn.Close();
                }
            }
            return ventas;
        }

        public static void CargarVenta(List<DTOProductoVenta> lista)
        {
            
            
            
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                string query = "INSERT INTO PRODUCTOVENDIDO VALUES (@stock,@idprod,@idventa);";
                string queryVenta = "INSERT INTO VENTA VALUES ('') ";
                string queryStockProd = "UPDATE PRODUCTO SET STOCK = @nuevostock WHERE ID = @id";

                SqlParameter stockParam = new SqlParameter("stock", SqlDbType.Int);
                SqlParameter idProdParam = new SqlParameter("idprod", SqlDbType.BigInt);
                SqlParameter idVentaParam = new SqlParameter("idventa", SqlDbType.BigInt);
                SqlParameter nuevostockParam = new SqlParameter("nuevostock", SqlDbType.Int);
                SqlParameter idParam = new SqlParameter("id", SqlDbType.Int);

                conn.Open();

                using (SqlCommand commandVenta = new SqlCommand(queryVenta, conn))
                {
                    commandVenta.ExecuteNonQuery();
                }

                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    command.Parameters.Add(stockParam);
                    command.Parameters.Add(idProdParam);
                    command.Parameters.Add(idVentaParam);

                    foreach(DTOProductoVenta producto in lista)
                    {
                        idProdParam.Value= producto.IdProducto;
                        stockParam.Value = producto.Stock;
                        idVentaParam.Value = producto.IdUsuario;
                        command.ExecuteNonQuery();
                    }

                }

                using (SqlCommand commandStock = new SqlCommand(queryStockProd, conn))
                {
                    commandStock.Parameters.Add(nuevostockParam);
                    commandStock.Parameters.Add(idParam);

                    var listaDeProductos = ProductoHandler.GetProducto();
                    foreach(DTOProductoVenta p in lista)
                    {
                        idParam.Value = p.IdProducto;
                        foreach(Producto q in listaDeProductos)
                        {
                            if (p.IdProducto == q.Id)
                            {
                                nuevostockParam.Value = Convert.ToInt32(q.Stock - p.Stock);
                                commandStock.ExecuteNonQuery();
                            }
                        }
                        
                    }
                    
                    
                }
                conn.Close();
            }
            
        }
    }
}

