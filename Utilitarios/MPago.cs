using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Utilitarios
{
    [Serializable]
    [Table("pago", Schema = "delux")]
    public class MPago
    {
        private int id;
        private string descripcion;

        [Key]
        [Column("id")]
        public int Id { get => id; set => id = value; }
        [Column("descripcion")]
        public string Descripcion { get => descripcion; set => descripcion = value; }
    }
}
