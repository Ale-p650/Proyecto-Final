using ConsoleApp;
using ConsoleApp.Models;
using Integrando_APIs_con_ADO.NET.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Integrando_APIs_con_ADO.NET.Controllers
{
    [ApiController]
    [Route("[controller]")]
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
