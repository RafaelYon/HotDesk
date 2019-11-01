using System;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public abstract class Model
    {
        public int Id { get; set; }

        [Display(Name = "Criado em")]
        public DateTime CreatedAt { get; set; }

        [Display(Name = "Atualizado em")]
        public DateTime UpdatedAt { get; set; }

        public Model()
        {
            CreatedAt = DateTime.Now;
        }

        public void UpdateData()
        {
            UpdatedAt = DateTime.Now;
        }
    }
}
