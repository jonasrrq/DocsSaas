using Docs.Saas.Model.Auth;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Docs.Saas.Model.Domaim
{
    public class PrimiumUser
    {
        public Guid Id { get; set; }
        [Display(Name = "Fecha Inicio"), Column(TypeName = "date"), DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
        public DateTime FechaInicio { get; set; }
        [Display(Name = "Fecha Fin"), Column(TypeName = "date"), DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
        public DateTime FechaFin { get; set; }

        public virtual Primium  Primium { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
