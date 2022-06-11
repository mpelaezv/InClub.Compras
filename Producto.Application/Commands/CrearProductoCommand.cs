using Dapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace Producto.Application.Commands
{
    public class CrearProductoCommand : IRequest<string>
    {
        public string ProdNombre { get; set; }
        public int ProdUM { get; set; }
        public decimal ProdPrecio { get; set; }
        public int AuditUsuarioCreacion { get; set; }

        public class CrearProductoCommandHandler : IRequestHandler<CrearProductoCommand, string>
        {
            private readonly IConfiguration configuration;
            public CrearProductoCommandHandler(IConfiguration configuration)
            {
                this.configuration = configuration;
            }

            DynamicParameters bindData(CrearProductoCommand cmd)
            {
                var parameters = new DynamicParameters();
                parameters.Add("@ParamProdNombre", cmd.ProdNombre);
                parameters.Add("@ParamProdUM", cmd.ProdUM);
                parameters.Add("@ParamProdPrecio", cmd.ProdPrecio);
                parameters.Add("@ParamAuditUsuarioCreacion", cmd.AuditUsuarioCreacion);
                return parameters;
            }

            public async Task<string> Handle(CrearProductoCommand cmd, CancellationToken cancellationToken)
            {
                var prc = "ProcProductoAgregar";
                using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Open();
                    var result = await connection.ExecuteAsync(prc, bindData(cmd), commandType: CommandType.StoredProcedure);
                    return "Producto Creado";
                }
            }
        }
    }
}
