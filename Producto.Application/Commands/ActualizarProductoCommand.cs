using Dapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace Producto.Application.Commands
{
    public class ActualizarProductoCommand : IRequest<string>
    {
        public int ID { get; set; }
        public string ProdNombre { get; set; }
        public int ProdUM { get; set; }
        public decimal ProdPrecio { get; set; }
        public int AuditUsuarioUltimaModif { get; set; }

        public class ActualizarProductoCommandHandler : IRequestHandler<ActualizarProductoCommand, string>
        {
            private readonly IConfiguration configuration;
            public ActualizarProductoCommandHandler(IConfiguration configuration)
            {
                this.configuration = configuration;
            }

            DynamicParameters bindData(ActualizarProductoCommand cmd)
            {
                var parameters = new DynamicParameters();
                parameters.Add("@ParamID", cmd.ID);
                parameters.Add("@ParamProdNombre", cmd.ProdNombre);
                parameters.Add("@ParamProdUM", cmd.ProdUM);
                parameters.Add("@ParamProdPrecio", cmd.ProdPrecio);
                parameters.Add("@ParamAuditUsuarioUltimaModif", cmd.AuditUsuarioUltimaModif);
                return parameters;
            }

            public async Task<string> Handle(ActualizarProductoCommand cmd, CancellationToken cancellationToken)
            {
                var prc = "ProcProductoActualizar";
                using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Open();
                    var result = await connection.ExecuteAsync(prc, bindData(cmd), commandType: CommandType.StoredProcedure);
                    return "Producto Actualizado";
                }
            }
        }
    }
}
