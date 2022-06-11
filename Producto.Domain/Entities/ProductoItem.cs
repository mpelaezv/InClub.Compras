using System;

namespace Producto.Domain
{
    public class ProductoItem
    {
        public int ID { get; set; }
        public string ProdCodigo { get; set; }
        public string ProdNombre { get; set; }
        public int ProdUM { get; set; }
        public decimal ProdPrecio { get; set; }
        public DateTime AuditFechaCreacion { get; set; }
        public int AuditUsuarioCreacion { get; set; }
        public DateTime? AuditFechaUltimaModif { get; set; }
        public int? AuditUsuarioUltimaModif { get; set; }
        public bool? AuditEstado { get; set; }
    }
}
