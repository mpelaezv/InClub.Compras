namespace Usuario.Domain
{
    public class UsuarioAggregate : UsuarioItem
    {
        public string AuditUsuarioCreacionDesc { get; set; }
        public string AuditUsuarioUltimaModifDesc { get; set; }
    }
}
