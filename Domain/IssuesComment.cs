using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class IssuesComment : Model
    {
        [Display(Name = "Comentário")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo obrigatório")]
        public string Comment { get; set; }

        [Display(Name = "Chamado")]
        [Required(ErrorMessage = "Não é possível criar um comentário sem especificar o chamado desejado")]
        public Issue Issue { get; set; }

        [Display(Name = "Autor")]
        [Required]
        public User CreatedBy { get; set; }
    }
}
