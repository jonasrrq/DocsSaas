using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Docs.Saas.Model.Domaim
{
  public  class Primium
    {
        public Guid Id { get; set; }
        [Required, StringLength(100)]
        public string Descripcion { get; set; }

        public virtual Licencia Licencia { get; set; }
    }
}
