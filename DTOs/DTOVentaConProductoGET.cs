namespace Proyecto_Final.DTOs
{
    public class DTOVentaConProductoGET
    {
        // Clave foranea IdVenta ya que en la tabla Venta se llama ID
        // y en ProductoVendido se llama IdVenta 
        public int IdVenta { get; set; }


        public int IdProducto { get; set; }
        public int Stock { get; set; }

    }
}
