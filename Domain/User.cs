using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Domain
{
    public class User : Model, ISeed<User>
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
        [JsonIgnore]
        public string Password { get; set; }

        [Display(Name = "Confirmação de senha")]
        [Required(ErrorMessage = "Campo obrigatório")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "A senhas não concidem")]
        [NotMapped]
        [JsonIgnore]
        public string ConfirmPassword { get; set; }

        [JsonIgnore]
        public string Image { get; set; }

        [JsonIgnore]
        public virtual List<GroupUser> GroupUser { get; set; }

        [JsonIgnore]
        public virtual List<Issue> IssuesCreated { get; set; }

        [JsonIgnore]
        public virtual List<Issue> IssuesAssigned { get; set; }

        [JsonIgnore]
        public virtual List<IssuesComment> IssuesComments { get; set; }

        public User()
        {
            ResetGroups();
        }

        public void ResetGroups()
        {
            GroupUser = new List<GroupUser>();
        }

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

        public List<User> GetSeedData()
        {
            var result = new List<User>();

            result.Add(new User
            {
                Id = 1,
                Name = "Admin",
                Image = "iVBORw0KGgoAAAANSUhEUgAAAEEAAABBAQMAAAC0OVsGAAAABlBMVEX///84SyvAiwjYAAAAAnRSTlMA/1uRIrUAAAAiSURBVHicY2Bg/w8EHxiAYOSyBhz8h4KGgWP9ALmDf+BYACldHJem9JdHAAAAAElFTkSuQmCC",
                Email = "admin@hotdesk.com",                
                Password = "$2a$11$R0eCbYJa.keZBbcvJxtmu.pNFH7xS/q3e9izFAYae8dQAgb4ky2Z6", // "admin1234"
                CreatedAt = new DateTime(2019, 12, 3, 14, 34, 00),
                UpdatedAt = new DateTime(2019, 12, 3, 14, 24, 00),
            });

            return result;
        }

        public static implicit operator UserEditable(User u)
        {
            return new UserEditable
            {
                Id = u.Id,
                CreatedAt = u.CreatedAt,
                UpdatedAt = u.UpdatedAt,
                Name = u.Name,
                Email = u.Email,
                Password = u.Password,
                Image = u.Image
            };
        }
    }
}
