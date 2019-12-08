using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
	public enum PermissionType
	{
		None,
		
		// Issue actions
		[Display(Description = "Criar chamado")]
		IssueCreate,

		[Display(Description = "Assumir chamado")]
		IssueAccept,

		[Display(Description = "Encerrar chamado")]
		IssueClose,

		[Display(Description = "Avaliar assitencia")]
		IssueRateAssistence,

		// Manager actions
		[Display(Description = "Gerenciar contas")]
		ManageAccounts,

		[Display(Description = "Gerenciar grupos")]
		ManageGroups,

		[Display(Description = "Gerenciar categorias")]
		ManageCategories
	};

	public class Permission : Model, ISeed<Permission>
	{
		[Display(Name = "Nome")]
		[Required]
		public string Name { get; set; }

		public PermissionType PermissionType
		{
			get => (PermissionType) Id;
		}

		public virtual List<GroupPermission> GroupPermissions { get; set; }

		public Permission()
		{
			GroupPermissions = new List<GroupPermission>();
		}

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, GetType());
        }

        public List<Permission> GetSeedData()
		{
			var permissions = new List<Permission>();

			foreach (PermissionType permissionType in (PermissionType[])Enum.GetValues(typeof(PermissionType)))
			{
				if (permissionType == PermissionType.None)
				{
					continue;
				}

				permissions.Add(new Permission
				{
					Id = (int) permissionType,
					Name = permissionType.ToString(),
					CreatedAt = new DateTime(2019, 11, 24, 14, 34, 00),
					UpdatedAt = new DateTime(2019, 11, 24, 14, 34, 00),
				});
			}

			return permissions;
		}
	}
}
