using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Usuario.API.Attributes;
using Usuario.Application.Commands;
using Usuario.Application.Queries;

namespace Usuario.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : BaseController
    {
        /// <summary>
        /// Crea un Usuario
        /// </summary>
        /// <remarks>
        /// Ejemplo de Request:
        ///
        ///     POST /Usuario/RegistrarUsuario
        ///     {
        ///         "UsCorreo": "nombre@servidorcorreco.com",
        ///         "UsPass": "c3p0",
        ///         "UsNombre": "Juan",
        ///         "UsApellidos": "Perez"
        ///     }
        ///
        /// </remarks>
        /// <param name="command"></param>
        /// <returns>Resultado de Operacion</returns>
        /// <response code="200">Texto de Confirmacion de Operacion</response>
        /// <response code="400">JSON Error Detallado</response>            
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [EntityTag("Usuario")]
        [HttpPost("RegistrarUsuario")]
        public async Task<IActionResult> CrearUsuario(CrearUsuarioCommand command) => Ok(await Mediator.Send(command));

        /// <summary>
        /// Obtiene lista de todos los Usuarios
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <returns>Resultado de Operacion</returns>
        /// <response code="200">Listado de Usuarios</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("ObtenerUsuarios")]
        public async Task<IActionResult> ListarUsuarios() => Ok(await Mediator.Send(new ObtenerUsuariosQuery()));

        /// <summary>
        /// Actualiza los datos de un Usuario
        /// </summary>
        /// <remarks>
        /// Ejemplo de Request:
        ///
        ///     PUT /Usuario/ActualizarUsuario
        ///     {
        ///         "ID": 1000,
        ///         "UsCorreo": "nombre@servidorcorreco.com",
        ///         "UsPass": "07r@c14v3",
        ///         "UsNombre": "Juan",
        ///         "UsApellidos": "Perez"
        ///     }
        ///
        /// </remarks>
        /// <param name="command"></param>
        /// <returns>Resultado de Operacion</returns>
        /// <response code="200">Texto de Confirmacion de Operacion</response>
        /// <response code="400">JSON Error Detallado</response>            
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [EntityTag("Usuario")]
        [HttpPut("ActualizarUsuario")]
        public async Task<IActionResult> ModificarUsuario(ActualizarUsuarioCommand command) => Ok(await Mediator.Send(command));

        /// <summary>
        /// Elimina los datos de un Usuario
        /// </summary>
        /// <remarks>
        /// Ejemplo de Request:
        ///
        ///     DELETE /Usuario/EliminarUsuario
        ///     {
        ///         "UsuarioID": 1000
        ///     }
        ///
        /// </remarks>
        /// <param name="command"></param>
        /// <returns>Resultado de Operacion</returns>
        /// <response code="200">Texto de Confirmacion de Operacion</response>
        /// <response code="400">JSON Error Detallado</response>            
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [EntityTag("Usuario")]
        [HttpDelete(nameof(EliminarUsuario))]
        public async Task<IActionResult> EliminarUsuario(EliminarUsuarioCommand command) => Ok(await Mediator.Send(command));
    }
}
