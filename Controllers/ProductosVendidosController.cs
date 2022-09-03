using Microsoft.AspNetCore.Mvc;
using Proyecto_Final.Models;
using Proyecto_Final.Repo;

namespace Proyecto_Final.Controllers
{

    [ApiController]
    [Route("api/[controller]")]


    public class ProductosVendidosController : ControllerBase
    {


        [HttpGet("{id}")]
        public List<ProductoVendido> TraerProductosVendidos(int id)
        {
            return ProductoVendidoHandler.GetProdVen(id);
        }


    }
}
