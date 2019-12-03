using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Domain
{
    public class Group : Model, ISeed<Group>
    {
        [Display(Name = "Nome")]
        [Required(ErrorMessage = "Campo obrigatório")]
        public string Name { get; set; }

        [Display(Name = "Padrão")]
        public bool Default { get; set; }

        public virtual List<GroupPermission> GroupPermissions { get; set; }

        public virtual List<GroupUser> GroupUser { get; set; }

        public Group()
        {
			GroupPermissions = new List<GroupPermission>();
            GroupUser = new List<GroupUser>();
        }

        public List<Permission> GetPermissions()
        {
            return GroupPermissions.Select(x => x.Permission).ToList();
        }

		public List<Group> GetSeedData()
		{
			var groups = new List<Group>();

			groups.Add(new Group
			{
				Id = 1,
				CreatedAt = new DateTime(2019, 11, 24, 14, 34, 00),
				UpdatedAt = new DateTime(2019, 11, 24, 14, 34, 00),
				Name = "Default",
				Default = true
			});

			groups.Add(new Group
			{
				Id = 2,
				CreatedAt = new DateTime(2019, 11, 24, 14, 34, 00),
				UpdatedAt = new DateTime(2019, 11, 24, 14, 24, 00),
				Name = "Support"
			});

			groups.Add(new Group
			{
				Id = 3,
				CreatedAt = new DateTime(2019, 11, 24, 14, 34, 00),
				UpdatedAt = new DateTime(2019, 11, 24, 14, 24, 00),
				Name = "Admin"
			});

			return groups;
		}
	}
}
