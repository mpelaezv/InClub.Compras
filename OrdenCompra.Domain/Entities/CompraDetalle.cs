using System;

namespace OrdenCompra.Domain
{
    public class CompraDetalle
    {
        [Obsolete]
        public int ID { get; set; }
        [Obsolete]
        public int CompraID { get; set; }
        public int ProductoID { get; set; }
        public int ItemCant { get; set; }
        [Obsolete]
        public int ItemUM { get; set; }
        [Obsolete]
        public decimal ItemPU { get; set; }
        [Obsolete]
        public decimal ItemTotal { get; set; }
        [Obsolete]
        public DateTime AuditFechaCreacion { get; set; }
        public int AuditUsuarioCreacion { get; set; }
    }
}
