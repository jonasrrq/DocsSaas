using Docs.Saas.Model.Auth;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Docs.Saas.Model.Domaim
{
    public class Licencia
    {
        public Guid Id { get; set; }
        [Required, StringLength(100)]
        public String Descripcion { get; set; }

        public virtual ICollection<Primium> Primiums { get; set; }
        public virtual ICollection<ApplicationUser> Users { get; set; }
    }
}
