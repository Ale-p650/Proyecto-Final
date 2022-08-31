
using Microsoft.AspNetCore.Mvc;
using Proyecto_Final.Models;
using Proyecto_Final.Repo;

namespace Proyecto_Final.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class NombreController : ControllerBase
    {

        [HttpGet]
        public string GetNombre()
        {
            return "Bienvenido a la CoderStore";
        }

    }
}
