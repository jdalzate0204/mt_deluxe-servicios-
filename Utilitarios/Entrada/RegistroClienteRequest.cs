using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilitarios.Entrada
{
    public class RegistroClienteRequest
    {
        [Required(ErrorMessage = "El Nombrecl es requerido.")]
        public string Nombrecl { get; set; }
        [Required(ErrorMessage = "El Apellido es requerido.")]
        public string Apellido { get; set; }
        [Required(ErrorMessage = "La FechaDeNacimiento es requerida.")]
        public Nullable<DateTime> FechaDeNacimiento { get; set; }
        [Required(ErrorMessage = "El Email es requerido.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "El Usuario es requerido.")]
        public string Usuario { get; set; }
        [Required(ErrorMessage = "La Contrasena es requerida.")]
        public string Contrasena { get; set; }
        [Required(ErrorMessage = "La Confirmacion es requerida")]
        public string Confirmacion { get; set; }
    }
}
