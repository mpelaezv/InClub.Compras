using Dapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using OrdenCompra.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace OrdenCompra.Application.Commands
{
    public class CrearOrdenCompraCommand : IRequest<string>
    {
        private int ID { get; set; }
        public int CompraUsuarioID { get; set; }
        public int AuditUsuarioCreacion { get; set; }
        public List<CompraDetalle> CompraDetalles { get; set; }

        public CrearOrdenCompraCommand()
        {
            CompraDetalles = new List<CompraDetalle>();       
        }

        public class CrearOrdenCompraCommandHandler : IRequestHandler<CrearOrdenCompraCommand, string>
        {
            private readonly IConfiguration configuration;
            public CrearOrdenCompraCommandHandler(IConfiguration configuration)
            {
                this.configuration = configuration;
            }

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            
            CrearOrdenCompraCommand AgregarMaestro(CrearOrdenCompraCommand oMaestro)
            {
                var parameters = new DynamicParameters();
                parameters.Add("@ID", value: oMaestro.ID, dbType: DbType.Int32, direction: ParameterDirection.InputOutput);
                parameters.Add("@ParamCompraUsuarioID", value: oMaestro.CompraUsuarioID);
                parameters.Add("@ParamAuditUsuarioCreacion", value: oMaestro.AuditUsuarioCreacion);

                var prc = "ProcCompraAgregar";
                using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Open();
                    var result = connection.Execute(prc, parameters, commandType: CommandType.StoredProcedure);
                    oMaestro.ID = parameters.Get<int>("@ID");
                }
                return oMaestro;
            }

            CompraDetalle AgregarDetalle(CompraDetalle oDetalle)
            {
                var parameters = new DynamicParameters();
                parameters.Add("@ParamCompraID", value: oDetalle.CompraID);
                parameters.Add("@ParamProductoID", value: oDetalle.ProductoID);
                parameters.Add("@ParamItemCant", value: oDetalle.ItemCant);
                parameters.Add("@ParamAuditUsuarioCreacion", value: oDetalle.AuditUsuarioCreacion);

                var prc = "ProcCompraDetalleAgregar";
                using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Open();
                    var result = connection.Execute(prc, parameters, commandType: CommandType.StoredProcedure);
                }
                return oDetalle;
            }

            public async Task<string> Handle(CrearOrdenCompraCommand cmd, CancellationToken cancellationToken)
            {
                using (var txScope = new TransactionScope())
                {
                    try
                    {
                        AgregarMaestro(cmd);

                        foreach (CompraDetalle item in cmd.CompraDetalles)
                        {
                            item.CompraID = cmd.ID;
                            AgregarDetalle(item);
                        }

                        txScope.Complete();
                        return "Orden de Compra Creada";
                    }
                    catch (Exception ex )
                    {
                        return ex.Message;
                    }
                }
            }
        }
    }
}
