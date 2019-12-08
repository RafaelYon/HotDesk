using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Category : Model
    {
        [Display(Name = "Nome")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo obrigatório")]
        public string Name { get; set; }

        [Display(Name = "Descrição")]
        public string Description { get; set; }

        [JsonIgnore]
        public virtual List<Issue> Issues { get; set; }

        public Category()
        {
            Issues = new List<Issue>();
        }
    }
}
