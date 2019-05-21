using Docs.Saas.Model.Domaim;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Docs.Saas.Model.Auth
{
    public class ApplicationUser : IdentityUser
    {
        [Required, StringLength(100)]
        public string Nombres { get; set; }
        [Display(Name ="Estatus del Usuario")]
        public bool Baja { get; set; }
       
        public virtual Licencia Licencia { get; set; }

        public virtual ICollection<LicenciaUser>    LicenciaUsers { get; set; }
        public virtual ICollection<PrimiumUser>     PrimiumUsers { get; set; }
   
    }
}
