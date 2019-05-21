using Docs.Saas.Model.Auth;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Docs.Saas.Model.Domaim
{
    public class UserApplicationUser
    {
        [StringLength(450)]
        public string RevEmpId { get; set; }
        [StringLength(450)]
        public string UserId { get; set; }

        [ForeignKey("RevEmpId")]
        public virtual ApplicationUser UserRevEmp { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser UserFinal { get; set; }

    }
}
