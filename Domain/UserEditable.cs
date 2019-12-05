using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Domain
{
    public class UserEditable : Model
    {
        [Display(Name = "Nome")]
        [Required(ErrorMessage = "Campo obrigatório")]
        public string Name { get; set; }

        [Display(Name = "E-mail")]
        [Required(ErrorMessage = "Campo obrigatório")]
        [EmailAddress(ErrorMessage = "E-mail inválido")]
        [MaxLength(256, ErrorMessage = "O e-mail não pode possuir mais de 256 caracteres")]
        public string Email { get; set; }

        [Display(Name = "Senha")]
        [MinLength(6, ErrorMessage = "A senha deve possuir no mínimo 6 caracteres")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string Image { get; set; }

        public virtual List<GroupUser> GroupUser { get; set; }

        public virtual List<Issue> IssuesCreated { get; set; }

        public virtual List<Issue> IssuesAssigned { get; set; }

        public virtual List<IssuesComment> IssuesComments { get; set; }

        public List<PermissionType> GetPermissions()
        {
            var result = new List<PermissionType>();

            foreach (List<GroupPermission> groupsPermissions in GroupUser.Select(x => x.Group.GroupPermissions).ToArray())
            {
				foreach (GroupPermission groupPermission in groupsPermissions)
				{
					if (result.Contains(groupPermission.Permission.PermissionType))
					{
						continue;
					}

					result.Add(groupPermission.Permission.PermissionType);
				}
            }

            return result;
        }

        public List<Group> GetGroups()
        {
            return GroupUser.Select(x => x.Group).ToList();
        }

        public static implicit operator User(UserEditable ue)
        {
            return new User
            {
                Id = ue.Id,
                CreatedAt = ue.CreatedAt,
                UpdatedAt = ue.UpdatedAt,
                Name = ue.Name,
                Email = ue.Email,
                Password = ue.Password,
                ConfirmPassword = ue.Password,
                Image = ue.Image
            };
        }
    }
}
