using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Group : Model
    {
        [Display(Name = "Nome")]
        [Required(ErrorMessage = "Campo obrigatório")]
        public string Name { get; set; }

		public List<GroupPermission> GroupPermissions { get; set; }

        public virtual List<GroupUser> GroupUser { get; set; }

        public Group()
        {
			GroupPermissions = new List<GroupPermission>();
            GroupUser = new List<GroupUser>();
        }
    }
}
