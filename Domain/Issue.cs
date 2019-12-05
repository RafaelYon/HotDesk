using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Domain
{
    public enum IssueStatus
    {
        Open,
        InProgress,
        Closed
    }

    public class Issue : Model
    {
        [Display(Name = "Título")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo obrigatório")]
        [MaxLength(256, ErrorMessage = "O título não pode possuir mais de 256 caracteres")]
        public string Title { get; set; }

        [Display(Name = "Descrição")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo obrigatório")]
        public string Description { get; set; }

        [Display(Name = "Categoria")]
        public virtual Category Category { get; set; }

        [Display(Name = "Avaliação")]
        public float? Rate { get; set; }

        public IssueStatus Status { get; set; }

        [Display(Name = "Solicitante")]
        public virtual User Owner { get; set; }

        [Display(Name = "Responsável")]
        public virtual User Responsible { get; set; }

        public virtual List<IssuesComment> Comments { get; set; }

        public Issue()
        {
            Comments = new List<IssuesComment>();
        }

        public List<IssuesComment> GetCommentsOrdened()
        {
            return Comments.OrderBy(x => x.Id).ToList();
        }
    }
}
