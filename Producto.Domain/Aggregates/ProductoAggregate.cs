namespace Producto.Domain
{
    public class ProductoAggregate : ProductoItem
    {
        public string ProdUMDesc { get; set; }
        public string AuditUsuarioCreacionDesc { get; set; }
        public string AuditUsuarioUltimaModifDesc { get; set; }
    }
}
