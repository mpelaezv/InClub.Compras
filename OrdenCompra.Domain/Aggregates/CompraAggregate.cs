using System.Collections.Generic;

namespace OrdenCompra.Domain
{
    public class CompraAggregate : Compra
    {
        public CompraAggregate()
        {
            this.CompraDetalles = new List<CompraDetalleAggregate>();
        }

        public string CompraUsuarioDesc { get; set; }
        public string AuditUsuarioCreacionDesc { get; set; }
        public string AuditUsuarioUltimaModifDesc { get; set; }

        public List<CompraDetalleAggregate> CompraDetalles { get; set; }
    }
}
