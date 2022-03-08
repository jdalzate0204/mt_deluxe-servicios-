using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Utilitarios
{
    [Serializable]
    [Table("estado", Schema = "delux")]
    public class Estado
    {
        private int id;
        private string disponibilidad;

        [Key]
        [Column("id")]
        public int Id { get => id; set => id = value; }
        [Column("disponibilidad")]
        public string Disponibilidad { get => disponibilidad; set => disponibilidad = value; }
    }
}
