using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Permission : Model
    {
        [Display(Name = "Nome")]
        [Required(ErrorMessage = "Campo obrigatório")]
        public string Name { get; set; }

        public virtual ICollection<GroupPermission> Groups { get; set; }

        public Permission()
        {
            Groups = new List<GroupPermission>();
        }
    }
}
