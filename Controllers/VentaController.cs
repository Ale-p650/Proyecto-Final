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
        [HttpGet]
        public List<Venta> GetVentas()
        {
            return VentasHandler.GetVentas();
        }

        [HttpPost]
        public void CargarVenta([FromBody] List<DTOProductoVenta> lista)
        {
            VentasHandler.CargarVenta(lista);
        }


    }
}
