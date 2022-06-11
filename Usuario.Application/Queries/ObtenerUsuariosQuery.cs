using Dapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Usuario.Domain;

namespace Usuario.Application.Queries
{
    public class ObtenerUsuariosQuery : IRequest<IList<UsuarioAggregate>>
    {
        public class ObtenerUsuariosQueryHandler : IRequestHandler<ObtenerUsuariosQuery, IList<UsuarioAggregate>>
        {
            private readonly IConfiguration _configuration;
            public ObtenerUsuariosQueryHandler(IConfiguration configuration)
            {
                _configuration = configuration;
            }
            public async Task<IList<UsuarioAggregate>> Handle(ObtenerUsuariosQuery query, CancellationToken cancellationToken)
            {
                var sql = "SELECT u1.ID ,u1.UsCorreo ,u1.UsNombre ,u1.UsApellidos ,u1.AuditFechaCreacion ,u1.AuditUsuarioCreacion ,LTRIM(RTRIM(CONCAT(u2.UsNombre, ' ', u2.UsApellidos))) AS AuditUsuarioCreacionDesc ,u1.AuditFechaUltimaModif ,u1.AuditUsuarioUltimaModif ,LTRIM(RTRIM(CONCAT(u3.UsNombre, ' ', u3.UsApellidos))) AS AuditUsuarioUltimaModifDesc ,u1.AuditEstado FROM dbo.Usuario u1 LEFT JOIN dbo.Usuario u2 ON u1.AuditUsuarioCreacion = u2.ID LEFT JOIN dbo.Usuario u3 ON u1.AuditUsuarioUltimaModif = u3.ID;";
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Open();
                    var result = await connection.QueryAsync<UsuarioAggregate>(sql);
                    return result.ToList();
                }
            }
        }
    }
}
