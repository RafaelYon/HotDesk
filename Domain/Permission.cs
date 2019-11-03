using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
	public enum PermissionTypes
	{
		None,
		
		// Issue actions
		[Display(Description = "Criar chamado")]
		IssueCreate,

		[Display(Description = "Assumir chamado")]
		IssueAccept,

		[Display(Description = "Encerrar chamado")]
		IssueClose,

		[Display(Description = "Escalar chamado")]
		IssueEscalate,

		[Display(Description = "Avaliar assitencia")]
		IssueRateAssistence,

		// User manager actions
		[Display(Description = "Gerenciar contas")]
		ManageAccounts,

		[Display(Description = "Gerenciar grupos")]
		ManageGroups
	};

	public class Permission : Model
    {
        [Display(Name = "Nome")]
		[MaxLength(256, ErrorMessage = "O nome não pode possuir mais de 256 caracteres")]
		public string Name { get; set; }

		[Display(Name = "Descrição")]
		public string Description { get; set; }

		private PermissionTypes _type;

		public PermissionTypes Type
		{
			get
			{
				if (_type == PermissionTypes.None && !String.IsNullOrWhiteSpace(Name))
				{
					Enum.TryParse(Name, out _type);
				}

				return _type;
			}
		}

		public virtual List<GroupPermission> GroupPermission { get; set; }

        public Permission()
        {
            GroupPermission = new List<GroupPermission>();
        }
    }
}
