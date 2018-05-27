using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebTavern.Models.EFModels
{
    public class AttributeValue
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Ingredient")]
        public int IngredientId { get; set; }

        [ForeignKey("Attribute")]
        public int AttributeId { get; set; }

        [Required]
        [MaxLength(32)]
        public string Value { get; set; }

        public Ingredient Ingredient { get; set; }

        public Attribute Attribute { get; set; }
    }
}
