using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Group : Model
    {
        [Display(Name = "Nome")]
        [Required(ErrorMessage = "Campo obrigatório")]
        public string Name { get; set; }

        public ICollection<GroupPermission> Permissions { get; set; }

        public virtual ICollection<GroupUser> Users { get; set; }

        public Group()
        {
            Permissions = new List<GroupPermission>();
            Users = new List<GroupUser>();
        }
    }
}
