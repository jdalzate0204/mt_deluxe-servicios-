using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilitarios.Entrada
{
    public class ServicioClienteRequest
    {
        [Required(ErrorMessage = "El idDestino es requerido.")]
        public Nullable<int> idDestino { get; set; }
        [Required(ErrorMessage = "El idUbicacion es requerido.")]
        public Nullable<int> idUbicacion { get; set; }
        [Required(ErrorMessage = "El pago es requerido.")]
        public Nullable<int> pago { get; set; }
        public string descripcionServicio { get; set; }
        public int idCliente { get; set; }
        public double tarifa { get; set; }
        public double kilometros { get; set; }




    }
}
