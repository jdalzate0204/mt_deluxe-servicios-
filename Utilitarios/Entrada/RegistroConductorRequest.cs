using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilitarios.Entrada
{
    public class RegistroConductorRequest
    {
        [Required(ErrorMessage = "El Nombre es requerido.")]
        public string Nombre { get; set; }
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
        [Required(ErrorMessage = "La Placa es requerida.")]
        public string Placa { get; set; }
        [Required(ErrorMessage = "El Celular es requerida.")]
        public string Celular { get; set; }
        [Required(ErrorMessage = "La Cedula es requerida.")]
        public string Cedula { get; set; }

    }
}
