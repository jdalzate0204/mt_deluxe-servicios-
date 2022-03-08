using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Utilitarios
{
    [Serializable]
    [Table("destino", Schema = "delux")]
    public class Destino
    {
        private int id;
        private string lugarDestino;
        private string lugarUbicacion;

        [Key]
        [Column("id")]
        public int Id { get => id; set => id = value; }
        [Column("lugar_destino")]
        public string LugarDestino { get => lugarDestino; set => lugarDestino = value; }
        [Column("lugar_ubicacion")]
        public string LugarUbicacion { get => lugarUbicacion; set => lugarUbicacion = value; }
    }
}
