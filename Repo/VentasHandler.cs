using Proyecto_Final.DTOs;
using Proyecto_Final.Models;
using System.Data;
using System.Data.SqlClient;

namespace Proyecto_Final.Repo
{
    public class VentasHandler : DBHandler
    {
        public static List<DTOVentaConProductoGET> GetVentas(int id)
        {
            List<DTOVentaConProductoGET> ventas = new List<DTOVentaConProductoGET>();
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                string query = "select * from ProductoVendido left join Venta on ProductoVendido.IdVenta=Venta.Id where venta.Id=@id;";
                SqlParameter idParam = new SqlParameter("id", id);
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    command.Parameters.Add(idParam);
                    conn.Open();
                    using (SqlDataReader dr = command.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                DTOVentaConProductoGET obj = new DTOVentaConProductoGET();
                                obj.IdVenta = Convert.ToInt32(dr["IdVenta"]);
                                obj.IdProducto = Convert.ToInt32(dr["IdProducto"]);
                                obj.Stock = Convert.ToInt32(dr["Stock"]);


                                ventas.Add(obj);
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

        public static void EliminarVenta(int id)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                string query = "DELETE FROM PRODUCTOVENDIDO WHERE IDVENTA=@id;";
                string queryVenta = "DELETE FROM VENTA WHERE ID=@id";
                string queryStockProd = "UPDATE PRODUCTO SET STOCK = @nuevostock WHERE ID = @idProducto";

                SqlParameter idProdParam = new SqlParameter("idProducto", SqlDbType.BigInt);
                SqlParameter nuevostockParam = new SqlParameter("nuevostock", SqlDbType.Int);
                SqlParameter idParam = new SqlParameter("id", SqlDbType.Int);

                conn.Open();

                static List<ProductoVendido> GetProdVenPorIDVenta(int id)
                {
                    List<ProductoVendido> productosven = new List<ProductoVendido>();
                    using (SqlConnection conn = new SqlConnection(ConnectionString))
                    {
                        string query = "SELECT * FROM PRODUCTOVENDIDO WHERE IDVENTA=@id";
                        SqlParameter idParam = new SqlParameter("id", SqlDbType.Int) { Value = id };
                        using (SqlCommand command = new SqlCommand(query, conn))
                        {
                            command.Parameters.Add(idParam);
                            conn.Open();
                            using (SqlDataReader dr = command.ExecuteReader())
                            {
                                if (dr.HasRows)
                                {
                                    while (dr.Read())
                                    {
                                        ProductoVendido pv = new ProductoVendido();
                                        pv.IdProducto = Convert.ToInt32(dr["IdProducto"]);
                                        pv.Id = Convert.ToInt32(dr["Id"]);
                                        pv.Stock = Convert.ToInt32(dr["Stock"]);
                                        pv.IdVenta = Convert.ToInt32(dr["IdVenta"]);

                                        productosven.Add(pv);
                                    }
                                }
                            }
                            conn.Close();
                        }
                    }
                    return productosven;
                }

                using (SqlCommand commandStock = new SqlCommand(queryStockProd, conn))
                {
                    commandStock.Parameters.Add(nuevostockParam);
                    commandStock.Parameters.Add(idProdParam);

                    var listaDeProductos = ProductoHandler.GetProducto();

                    var listaDeProdVend = GetProdVenPorIDVenta(id);

                    foreach (ProductoVendido pv in listaDeProdVend)
                    {
                        foreach (Producto p in listaDeProductos)
                        {
                            if (p.Id == pv.IdProducto)
                            {
                                int stock = pv.Stock;

                                nuevostockParam.Value = p.Stock + pv.Stock;
                                idProdParam.Value = p.Id;
                                commandStock.ExecuteNonQuery();
                            }
                        }
                    }
                }

                using (SqlCommand commandVenta = new SqlCommand(queryVenta, conn))
                {
                    commandVenta.Parameters.Add(idParam);
                    idParam.Value = id;
                    commandVenta.ExecuteNonQuery();
                }

                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    command.Parameters.Add(idParam);
                    idParam.Value = id;
                    command.ExecuteNonQuery();

                }

                
                conn.Close();
            }

        }


    }
}

