using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Runniac.Web.ViewModels
{
    public class AbstractRegisterModel
    {
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Nombre")]
        [StringLength(30, ErrorMessage = "El {0} debe tener como máximmo {1} caracteres.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Apellidos")]
        [StringLength(50, ErrorMessage = "Los {0} deben tener como máximmo {1} caracteres.")]
        public string Lastname { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [EmailAddress]
        public string Email { get; set; }
    }
}