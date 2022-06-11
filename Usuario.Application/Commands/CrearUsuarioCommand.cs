using Dapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using Usuario.Infrastructure.Security;

namespace Usuario.Application.Commands
{
    public class CrearUsuarioCommand : IRequest<string>
    {
        public string UsCorreo { get; set; }
        public string UsPass { get; set; }
        public string UsNombre { get; set; }
        public string UsApellidos { get; set; }

        public class CrearUsuarioCommandHandler : IRequestHandler<CrearUsuarioCommand, string>
        {
            private readonly IConfiguration configuration;
            private int UsuarioActualID;
            public CrearUsuarioCommandHandler(IConfiguration configuration)
            {
                this.configuration = configuration;
                UsuarioActualID = 1000; //Se obtiene ID de Usuario logueado para operaciones
            }

            object bindData(CrearUsuarioCommand cmd)
            {
                return new
                {
                    UsCorreo = cmd.UsCorreo,
                    UsPass = Auth.hashPass(cmd.UsPass),
                    UsNombre = cmd.UsNombre,
                    UsApellidos = cmd.UsApellidos,
                    AuditUsuarioCreacion = UsuarioActualID
                };
            }

            public async Task<string> Handle(CrearUsuarioCommand cmd, CancellationToken cancellationToken)
            {
                var sql = "INSERT INTO dbo.Usuario (UsCorreo , UsPass , UsNombre , UsApellidos , AuditUsuarioCreacion) ";
                sql += "VALUES (@UsCorreo , @UsPass , @UsNombre , @UsApellidos , @AuditUsuarioCreacion)";
                using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Open();
                    var result = await connection.ExecuteAsync(sql, bindData(cmd));
                    return "Usuario Creado";
                }
            }
        }
    }
}
