using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebTavern.Models.EFModels
{
    public class Type
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(64)]
        public string Name { get; set; }

        public virtual ICollection<Ingredient> Ingredients { get; set; }

        public virtual ICollection<TypeAttribute> TypeAttributes { get; set; }
    }
}
