using System.ComponentModel.DataAnnotations;

namespace Domain
{
	public enum Permission
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
}
