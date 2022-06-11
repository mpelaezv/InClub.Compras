using Dapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using Usuario.Infrastructure.Security;

namespace Usuario.Application.Commands
{
    public class ActualizarUsuarioCommand : IRequest<string>
    {
        [Required]
        public int ID { get; set; }
        public string UsCorreo { get; set; }
        public string UsPass { get; set; }
        public string UsNombre { get; set; }
        public string UsApellidos { get; set; }

        public class ActualizarUsuarioCommandHandler : IRequestHandler<ActualizarUsuarioCommand, string>
        {
            private readonly IConfiguration configuration;
            private int UsuarioActualID;
            public ActualizarUsuarioCommandHandler(IConfiguration configuration)
            {
                this.configuration = configuration;
                UsuarioActualID = 1000; //Se obtiene ID de Usuario logueado para operaciones
            }

            object bindData(ActualizarUsuarioCommand cmd)
            {
                return new
                {
                    ID = cmd.ID,
                    UsCorreo = cmd.UsCorreo,
                    UsPass = Auth.hashPass(cmd.UsPass),
                    UsNombre = cmd.UsNombre,
                    UsApellidos = cmd.UsApellidos,
                    AuditFechaUltimaModif = DateTime.Now,
                    AuditUsuarioUltimaModif = UsuarioActualID
                };
            }

            public async Task<string> Handle(ActualizarUsuarioCommand cmd, CancellationToken cancellationToken)
            {
                var sql = "UPDATE dbo.Usuario SET UsCorreo = @UsCorreo, UsPass = @UsPass, UsNombre = @UsNombre, UsApellidos = @UsApellidos, AuditFechaUltimaModif = @AuditFechaUltimaModif, AuditUsuarioUltimaModif = @AuditUsuarioUltimaModif WHERE ID = @ID;";
                using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Open();
                    var result = await connection.ExecuteAsync(sql, bindData(cmd));
                    return "Usuario Actualizado";
                }
            }
        }
    }
}
