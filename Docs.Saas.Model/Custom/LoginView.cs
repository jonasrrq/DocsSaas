using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Docs.Saas.Model.Custom
{
    public class LoginView
    {
        [Required, Display(Name ="E-Mail")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        [Display(Name = "¿Recuérdame?")]
        public bool RememberMe { get; set; }
    }
}
