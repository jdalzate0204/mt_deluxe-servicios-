using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Utilitarios
{
    [Serializable]
    [Table("admin", Schema = "delux")]
    public class Admin
    {
        private int idAdmin;
        private string usuario;
        private string contrasena;
        private int rol;

        [Key]
        [Column("id_admin")]
        public int IdAdmin { get => idAdmin; set => idAdmin = value; }
        [Column("usuario")]
        public string Usuario { get => usuario; set => usuario = value; }
        [Column("contrasena")]
        public string Contrasena { get => contrasena; set => contrasena = value; }
        [Column("rol")]
        public int Rol { get => rol; set => rol = value; }
    }
}
