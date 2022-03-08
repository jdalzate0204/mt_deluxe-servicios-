using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilitarios
{
    [Serializable]
    [Table("token_login_administrador", Schema = "seguridad")]
    public class AdministradorTokenLogin
    {
        private int id;
        private int idAdmin;
        private DateTime fechaGenerado;
        private DateTime fechaVigencia;
        private string token;

        [Key]
        [Column("id")]
        public int Id { get => id; set => id = value; }
        [Column("id_admin")]
        public int IdAdmin { get => idAdmin; set => idAdmin = value; }
        [Column("fecha_generado")]
        public DateTime FechaGenerado { get => fechaGenerado; set => fechaGenerado = value; }
        [Column("fecha_vigencia")]
        public DateTime FechaVigencia { get => fechaVigencia; set => fechaVigencia = value; }
        [Column("token")]
        public string Token { get => token; set => token = value; }
    }
}
