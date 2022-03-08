using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Utilitarios
{
    [Serializable]
    [Table("notificacion_de_servicio", Schema = "delux")]
    public class Notificacion
    {
        private int idCliente;
        private Nullable<int> idUbicacion;
        private Nullable<int> idDestino;
        private int id;
        private string descripcionServicio;
        private double tarifa;
        private DateTime fechaCarrera;
        private Nullable<int> pago;
        private double kilometro;
        private string estado;
        private string conductor;
        private Nullable<int> idConductor;
        private string comentarioDeConductor;
        private Nullable<DateTime> fechaFinCarrera;
        private string comentarioDeCliente;
        private string conversacion;

        private string nombreCl;
        private string destino;
        private string ubicacion;
        private string metodoPago;
        private string sesion;

        private string nombreCo;
        private string apellidoCo;
        private string placa;
        private string cedula;
        private List<Notificacion> listaConductores;
        
        [Key]
        [Column("id")]
        public int Id { get => id; set => id = value; }
        [Column("id_cliente")]
        public int IdCliente { get => idCliente; set => idCliente = value; }
        [Column("id_ubicacion")]
        public Nullable<int> IdUbicacion { get => idUbicacion; set => idUbicacion = value; }
        [Column("id_destino")]
        public Nullable<int> IdDestino { get => idDestino; set => idDestino = value; }
        [Column("descripcion_servicio")]
        public string DescripcionServicio { get => descripcionServicio; set => descripcionServicio = value; }
        [Column("tarifa")]
        public double Tarifa { get => tarifa; set => tarifa = value; }
        [Column("fecha_carrera")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaCarrera { get => fechaCarrera; set => fechaCarrera = value; }
        [Column("pago")]
        public Nullable<int> Pago { get => pago; set => pago = value; }
        [Column("kilometros")]
        public double Kilometro { get => kilometro; set => kilometro = value; }
        [Column("estado")]
        public string Estado { get => estado; set => estado = value; }
        [Column("conductor")]
        public string Conductor { get => conductor; set => conductor = value; }
        [Column("id_conductor")]
        public Nullable<int> IdConductor { get => idConductor; set => idConductor = value; }
        [Column("comentario_de_conductor")]
        public string ComentarioDeConductor { get => comentarioDeConductor; set => comentarioDeConductor = value; }
        [Column("comentario_de_cliente")]
        public string ComentarioDeCliente { get => comentarioDeCliente; set => comentarioDeCliente = value; }
        [Column("fecha_fin_carrera")]
        public Nullable<DateTime> FechaFinCarrera { get => fechaFinCarrera; set => fechaFinCarrera = value; }
        [Column("conversacion")]
        public string Conversacion { get => conversacion; set => conversacion = value; }

        [NotMapped]
        public string NombreCl { get => nombreCl; set => nombreCl = value; }
        [NotMapped]
        public string Destino { get => destino; set => destino = value; }
        [NotMapped]
        public string Ubicacion { get => ubicacion; set => ubicacion = value; }
        [NotMapped]
        public string MetodoPago { get => metodoPago; set => metodoPago = value; }
        [NotMapped]
        public string Sesion { get => sesion; set => sesion = value; }

        [NotMapped]
        public string NombreCo { get => nombreCo; set => nombreCo = value; }
        [NotMapped]
        public string ApellidoCo { get => apellidoCo; set => apellidoCo = value; }
        [NotMapped]
        public string Placa { get => placa; set => placa = value; }
        [NotMapped]
        public string Cedula { get => cedula; set => cedula = value; }
        [NotMapped]
        public List<Notificacion> ListaConductores { get => listaConductores; set => listaConductores = value; }

    }
}
