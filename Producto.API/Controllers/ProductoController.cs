using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Producto.API.Attributes;
using Producto.Application.Commands;
using Producto.Application.Queries;
using System.Threading.Tasks;

namespace Producto.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : BaseController
    {
        /// <summary>
        /// Agregar un Producto
        /// </summary>
        /// <remarks>
        /// Ejemplo de Request:
        ///
        ///     POST /Producto/AgregarProducto
        ///     {
        ///         "ProdNombre": "Producto Nuevo",
        ///         "ProdUM": 2,
        ///         "ProdPrecio": 12.50,
        ///         "AuditUsuarioCreacion": 1000
        ///     }
        ///
        /// Alcances de Pruebas:
        /// <br /> 
        /// (!) En el campo ProdUM se puede usar el valor entero: 1 o 2.
        /// <br /> 
        /// (!) En el campo AuditUsuarioCreacion puede usar cualquier ID del Listado de Usuarios.
        /// </remarks>
        /// <param name="command"></param>
        /// <returns>Resultado de Operacion</returns>
        /// <response code="200">Texto de Confirmacion de Operacion</response>
        /// <response code="400">JSON Error Detallado</response>            
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [EntityTag("Producto")]
        [HttpPost("AgregarProducto")]
        public async Task<IActionResult> CrearProducto(CrearProductoCommand command) => Ok(await Mediator.Send(command));

        /// <summary>
        /// Obtiene lista de todos los Productos
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <returns>Resultado de Operacion</returns>
        /// <response code="200">Listado de Productos</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("ObtenerProductos")]
        public async Task<IActionResult> ListarProductos() => Ok(await Mediator.Send(new ObtenerProductosQuery()));

        /// <summary>
        /// Actualiza los datos de un Producto
        /// </summary>
        /// <remarks>
        /// Ejemplo de Request:
        ///
        ///     PUT /Producto/ActualizarProducto
        ///     {
        ///         "ID": 0,
        ///         "ProdNombre": "string",
        ///         "ProdUM": 0,
        ///         "ProdPrecio": 0,
        ///         "AuditUsuarioUltimaModif": 0
        ///     }
        ///
        /// Alcances de Pruebas:
        /// <br /> 
        /// (!) En el campo ProdUM se puede usar el valor entero: 1 o 2.
        /// <br /> 
        /// (!) En el campo AuditUsuarioUltimaModif puede usar cualquier ID del Listado de Usuarios.
        /// </remarks>
        /// <param name="command"></param>
        /// <returns>Resultado de Operacion</returns>
        /// <response code="200">Texto de Confirmacion de Operacion</response>
        /// <response code="400">JSON Error Detallado</response>            
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [EntityTag("Producto")]
        [HttpPut("ActualizarProducto")]
        public async Task<IActionResult> ModificarProducto(ActualizarProductoCommand command) => Ok(await Mediator.Send(command));

        /// <summary>
        /// Elimina un Producto
        /// </summary>
        /// <remarks>
        /// Ejemplo de Request:
        ///
        ///     DELETE /Producto/EliminarProducto
        ///     {
        ///         "ProductoID": 1000
        ///     }
        ///
        /// </remarks>
        /// <param name="command"></param>
        /// <returns>Resultado de Operacion</returns>
        /// <response code="200">Texto de Confirmacion de Operacion</response>
        /// <response code="400">JSON Error Detallado</response>            
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [EntityTag("Producto")]
        [HttpDelete(nameof(EliminarProducto))]
        public async Task<IActionResult> EliminarProducto(EliminarProductoCommand command) => Ok(await Mediator.Send(command));
    }
}
