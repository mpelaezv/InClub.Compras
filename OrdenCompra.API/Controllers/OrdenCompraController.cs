using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrdenCompra.API.Attributes;
using OrdenCompra.Application.Commands;
using System.Threading.Tasks;

namespace OrdenCompra.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdenCompraController : BaseController
    {
        /// <summary>
        /// Agregar una Orden de Compra
        /// </summary>
        /// <remarks>
        /// Ejemplo de Request:
        ///
        ///     POST /OrdenCompra/AgregarOrdenCompra
        ///     {
        ///         "CompraUsuarioID": 1000,
        ///         "AuditUsuarioCreacion": 1000,
        ///         "CompraDetalles": [
        ///                             {
        ///                                "ProductoID": 1001,
        ///                                "ItemCant": 7,
        ///                                "AuditUsuarioCreacion": 1000
        ///                             }
        ///                           ]
        ///     }
        ///
        /// Alcances de Pruebas:
        /// <br /> 
        /// (!) En el campo CompraUsuarioID se puede usar cualquier ID del Listado de Usuarios.
        /// <br /> 
        /// (!) En el campo AuditUsuarioCreacion puede usar cualquier ID del Listado de Usuarios.
        /// <br /> 
        /// (!) En el campo ProductoID puede usar cualquier ID del Listado de Productos.
        /// <br /> 
        /// (!) En el campo ItemCant, es la cantidad deseada del producto, el calculo del precio es interno.
        /// </remarks>
        /// <param name="command"></param>
        /// <returns>Resultado de Operacion</returns>
        /// <response code="200">Texto de Confirmacion de Operacion</response>
        /// <response code="400">JSON Error Detallado</response>            
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [EntityTag("Orden de Compra")]
        [HttpPost("AgregarOrdenCompra")]
        public async Task<IActionResult> CrearOrdenCompra(CrearOrdenCompraCommand command) => Ok(await Mediator.Send(command));
    }
}
