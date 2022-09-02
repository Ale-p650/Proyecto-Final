﻿using Proyecto_Final.Repo;
using Proyecto_Final.Models;
using Proyecto_Final.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Proyecto_Final.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

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
