using Proyecto_Final.Repo;
using Proyecto_Final.Models;
using Proyecto_Final.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Proyecto_Final.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {

        [HttpGet("{nombreUsuario}/{contraseña}")]
        public Usuario InicioSesion(string nombreUsuario,string contraseña)
        {
            return UsuarioHandler.GetUsuario();
        }

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
