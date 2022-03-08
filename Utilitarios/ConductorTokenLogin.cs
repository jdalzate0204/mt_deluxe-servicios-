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
    [Table("token_login_conductor", Schema = "seguridad")]
    public class ConductorTokenLogin
    {
        private int id;
        private int idConductor;
        private DateTime fechaGenerado;
        private DateTime fechaVigencia;
        private string token;

        [Key]
        [Column("id")]
        public int Id { get => id; set => id = value; }
        [Column("id_conductor")]
        public int IdConductor { get => idConductor; set => idConductor = value; }
        [Column("fecha_generado")]
        public DateTime FechaGenerado { get => fechaGenerado; set => fechaGenerado = value; }
        [Column("fecha_vigencia")]
        public DateTime FechaVigencia { get => fechaVigencia; set => fechaVigencia = value; }
        [Column("token")]
        public string Token { get => token; set => token = value; }
    }
}
