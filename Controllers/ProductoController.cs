using ConsoleApp;
using ConsoleApp.Models;
using Integrando_APIs_con_ADO.NET.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Integrando_APIs_con_ADO.NET.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class ProductoController : ControllerBase
    {

        [HttpGet]
        public List<Producto> GetProductos()
        {
            return ProductoHandler.GetProducto();
        }

        [HttpPost]
        public bool CrearProducto([FromBody] DTOProducto prod)
        {
            return ProductoHandler.CrearProducto(prod);
        }

        [HttpPut]
        public bool UpdateProducto([FromBody] DTOProducto prod)
        {
            return ProductoHandler.ModificarProducto(prod);
        }

        [HttpDelete]
        public bool BorrarProducto([FromBody] int id)
        {
            return ProductoHandler.BorrarProducto(id);
        }
    }
}
