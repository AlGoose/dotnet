using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebTavern.Models.EFModels
{
    public class Ingredient
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(64)]
        public string Name { get; set; }

        [ForeignKey("Type")]
        public int TypeId { get; set; }

        public Type Type { get; set; }

        public virtual ICollection<Recipe> Recipes { get; set; }

        public virtual ICollection<AttributeValue> AttributeValues { get; set; }
    }
}
