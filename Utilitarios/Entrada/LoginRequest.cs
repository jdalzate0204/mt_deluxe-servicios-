using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilitarios
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "El usuario es requerido.")]
        public string Usuario { get; set; }
        [Required(ErrorMessage = "La contraseña es requerida.")]
        public string Contrasena { get; set; }
    }
}
