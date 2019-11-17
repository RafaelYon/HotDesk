using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Domain
{
    public class User : Model
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
        [Required(ErrorMessage = "Campo obrigatório")]
        [MinLength(6, ErrorMessage = "A senha deve possuir no mínimo 6 caracteres")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Confirmação de senha")]
        [Required(ErrorMessage = "Campo obrigatório")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "A senhas não concidem")]
        [NotMapped]
        public string ConfirmPassword { get; set; }

		public List<GroupUser> GroupUser { get; set; }

        public User()
        {
            GroupUser = new List<GroupUser>();
        }

		public List<Permission> GetPermissions()
		{
			var result = new List<Permission>();

			foreach (var groupPermissions in GroupUser.Select(x => x.Group.GroupPermissions).ToArray())
			{
				foreach (GroupPermission groupPermission in groupPermissions)
				{
					if (result.Contains(groupPermission.Permission))
					{
						continue;
					}

					result.Add(groupPermission.Permission);
				}
			}

			return result;
		}
    }
}
