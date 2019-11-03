using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
	public enum PermissionTypes
	{
		// Issue actions
		[Display(Description = "Criar chamado")]
		IssueCreate = 1,

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
        public string Name { get; set; }

		[Display(Name = "Descrição")]
		public string Description { get; set; }

		public virtual List<GroupPermission> GroupPermission { get; set; }

        public Permission()
        {
            GroupPermission = new List<GroupPermission>();
        }
    }
}
