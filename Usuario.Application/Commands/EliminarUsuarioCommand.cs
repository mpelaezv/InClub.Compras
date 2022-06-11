using Dapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace Usuario.Application.Commands
{
    public class EliminarUsuarioCommand : IRequest<string>
    {
        public int UsuarioID { get; set; }
        public class EliminarUsuarioCommandHandler : IRequestHandler<EliminarUsuarioCommand, string>
        {
            private readonly IConfiguration _configuration;
            public EliminarUsuarioCommandHandler(IConfiguration configuration)
            {
                _configuration = configuration;
            }
            public async Task<string> Handle(EliminarUsuarioCommand command, CancellationToken cancellationToken)
            {
                var sql = "DELETE FROM dbo.Usuario WHERE ID = @UsuarioID";
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Open();
                    var result = await connection.ExecuteAsync(sql, new { UsuarioID = command.UsuarioID });
                    return "Se ha eliminado los datos del Usuario con exito.";
                }
            }
        }
    }
}
