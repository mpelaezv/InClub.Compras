using Dapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using Producto.Domain;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Producto.Application.Queries
{
    public class ObtenerProductosQuery : IRequest<IList<ProductoAggregate>>
    {
        public class ObtenerProductosQueryHandler : IRequestHandler<ObtenerProductosQuery, IList<ProductoAggregate>>
        {
            private readonly IConfiguration _configuration;
            public ObtenerProductosQueryHandler(IConfiguration configuration)
            {
                _configuration = configuration;
            }
            public async Task<IList<ProductoAggregate>> Handle(ObtenerProductosQuery query, CancellationToken cancellationToken)
            {
                var prc = "ProdProductoListar";
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Open();
                    var result = await connection.QueryAsync<ProductoAggregate>(prc, commandType: CommandType.StoredProcedure);
                    return result.ToList();
                }
            }
        }
    }
}
