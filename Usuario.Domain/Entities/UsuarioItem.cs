using System;

namespace Usuario.Domain
{
    public class UsuarioItem
    {
        public int ID { get; set; }
        public string UsCorreo { get; set; }
        public byte[] UsPass { get; set; }
        public string UsNombre { get; set; }
        public string UsApellidos { get; set; }
        public DateTime AuditFechaCreacion { get; set; }
        public int AuditUsuarioCreacion { get; set; }
        public DateTime? AuditFechaUltimaModif { get; set; }
        public int? AuditUsuarioUltimaModif { get; set; }
        public bool? AuditEstado { get; set; }
    }
}
