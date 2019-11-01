using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Permission : Model
    {
        [Display(Name = "Nome")]
        [Required(ErrorMessage = "Campo obrigatório")]
        public string Name { get; set; }

        public virtual List<GroupPermission> GroupPermission { get; set; }

        public Permission()
        {
            GroupPermission = new List<GroupPermission>();
        }
    }
}
