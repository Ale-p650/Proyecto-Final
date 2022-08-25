using ConsoleApp.Models;
using System.Data;
using System.Data.SqlClient;

namespace ConsoleApp
{
    public class ProductoVendidoHandler : DBHandler
    {
        public List<ProductoVendido> GetProdVen()
        {
            List<ProductoVendido> productosven = new List<ProductoVendido>();
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                string query = "SELECT * FROM PRODUCTOVENDIDO";

                using (SqlCommand command = new SqlCommand(query, conn))
                {
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
    }
}
