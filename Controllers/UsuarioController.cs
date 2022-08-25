using ConsoleApp;
using ConsoleApp.Models;
using Integrando_APIs_con_ADO.NET.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Integrando_APIs_con_ADO.NET.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
        [HttpGet]
        public List<Usuario> GetUsuario()
        {
            return UsuarioHandler.GetUsuario();
        }

        [HttpPut]
        
        public bool UpdateUsuario([FromBody] DTOUsuario usu)
        {
            return UsuarioHandler.ModificarUsuario(usu);
        }



    }
}
