namespace OrdenCompra.Domain
{
    public class CompraDetalleAggregate : CompraDetalle
    {
        public string ProductoDesc { get; set; }
        public string ItemUMDesc { get; set; }
        public string AuditUsuarioCreacionDesc { get; set; }
    }
}
