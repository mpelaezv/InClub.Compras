using Dapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace Producto.Application.Commands
{
    public class EliminarProductoCommand : IRequest<string>
    {
        public int ProductoID { get; set; }
        public class EliminarProductoCommandHandler : IRequestHandler<EliminarProductoCommand, string>
        {
            private readonly IConfiguration _configuration;
            public EliminarProductoCommandHandler(IConfiguration configuration)
            {
                _configuration = configuration;
            }

            DynamicParameters bindData(EliminarProductoCommand cmd)
            {
                var parameters = new DynamicParameters();
                parameters.Add("@ParamID", cmd.ProductoID);
                return parameters;
            }

            public async Task<string> Handle(EliminarProductoCommand cmd, CancellationToken cancellationToken)
            {
                var prc = "ProcProductoEliminar";
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Open();
                    var result = await connection.ExecuteAsync(prc, bindData(cmd), commandType: CommandType.StoredProcedure);
                    return "Se ha eliminado el Producto con exito.";
                }
            }
        }
    }
}
