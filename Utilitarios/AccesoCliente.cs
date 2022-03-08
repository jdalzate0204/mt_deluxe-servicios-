using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Utilitarios
{
    [Serializable]
    [Table("acceso_cliente", Schema = "seguridad")]
    public class AccesoCliente
    {
        private int id;
        private int idCliente;
        private string ip;
        private string mac;
        private DateTime fechaInicio;
        private string session;
        private Nullable<DateTime> fechaFin;

        [Key]
        [Column("id")]
        public int Id { get => id; set => id = value; }
        [Column("ip")]
        public string Ip { get => ip; set => ip = value; }
        [Column("mac")]
        public string Mac { get => mac; set => mac = value; }
        [Column("fecha_inicio")]
        public DateTime FechaInicio { get => fechaInicio; set => fechaInicio = value; }
        [Column("session")]
        public string Session { get => session; set => session = value; }
        [Column("id_cliente")]
        public int IdCliente { get => idCliente; set => idCliente = value; }
        [Column("fecha_fin")]
        public Nullable<DateTime> FechaFin { get => fechaFin; set => fechaFin = value; }
    }
}
