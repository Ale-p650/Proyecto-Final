using Proyecto_Final.Repo;
using Proyecto_Final.Models;
using Proyecto_Final.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Proyecto_Final.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VentaController : ControllerBase
    {
        [HttpGet("{id}")]
        public List<DTOVentaConProductoGET> TraerVentas(int id)
        {
            return VentasHandler.GetVentas(id);
        }

        [HttpPost]
        public void CargarVenta([FromBody] List<DTOProductoVenta> lista)
        {
            VentasHandler.CargarVenta(lista);
        }

        [HttpDelete("{id}")]
        public void EliminarVenta(int id)
        {
            VentasHandler.EliminarVenta(id);
        }

    }
}
