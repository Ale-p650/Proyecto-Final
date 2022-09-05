using Proyecto_Final.Models;
using System.Data;
using System.Data.SqlClient;

namespace Proyecto_Final.Repo
{
    public class ProductoVendidoHandler : DBHandler
    {
        public static List<ProductoVendido> GetProdVen(int id)
        {
            List<ProductoVendido> productosven = new List<ProductoVendido>();
            List<Producto> prod = ProductoHandler.GetProducto(id);
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                string query = "SELECT * FROM PRODUCTOVENDIDO WHERE IDPRODUCTO = @idProducto";
                SqlParameter idProdParam = new SqlParameter("idProducto", SqlDbType.Int);
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    command.Parameters.Add(idProdParam);
                    conn.Open();

                    foreach(Producto p in prod)
                    {
                        
                        idProdParam.Value= p.Id;
                        
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
                    }
                    
                    conn.Close();
                }
            }
            return productosven;
        }


    }
}
