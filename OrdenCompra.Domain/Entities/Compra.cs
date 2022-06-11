using System;

namespace OrdenCompra.Domain
{
    public class Compra
    {
        public int ID { get; set; }
        public string CompraCod { get; set; }
        public int CompraUsuarioID { get; set; }
        public decimal CompraMontoTotal { get; set; }
        public DateTime AuditFechaCrecion { get; set; }
        public int AuditUsuarioCreacion { get; set; }
        public DateTime? AuditFechaUltimaModif { get; set; }
        public int? AuditUsuarioUltimaModif { get; set; }
    }
}
