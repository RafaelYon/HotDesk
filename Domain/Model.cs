using System;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public abstract class Model
    {
		[Display(AutoGenerateField = true)]
		public int Id { get; set; }

        [Display(Name = "Criado em")]
        public DateTime CreatedAt { get; set; }

        [Display(Name = "Atualizado em")]
        public DateTime UpdatedAt { get; set; }

        public Model()
        {
            CreatedAt = DateTime.Now;
            UpdateDate();
        }

        public void UpdateDate()
        {
            UpdatedAt = DateTime.Now;
        }

        public override bool Equals(object obj)
        {
            return GetHashCode() == obj?.GetHashCode();
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, GetType());
        }
    }
}
