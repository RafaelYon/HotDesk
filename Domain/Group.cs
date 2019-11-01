using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Group : Model
    {
        [Display(Name = "Nome")]
        [Required(ErrorMessage = "Campo obrigatório")]
        public string Name { get; set; }

        public List<GroupPermission> GroupPermission { get; set; }

        public virtual List<GroupUser> GroupUser { get; set; }

        public Group()
        {
            GroupPermission = new List<GroupPermission>();
            GroupUser = new List<GroupUser>();
        }
    }
}
