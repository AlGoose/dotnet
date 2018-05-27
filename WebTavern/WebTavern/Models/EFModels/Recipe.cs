using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebTavern.Models.EFModels
{
    public class Recipe
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Drink")]
        public int DrinkId { get; set; }

        [ForeignKey("Ingredient")]
        public int IngredientId { get; set; }

        [Required]
        public int Amount { get; set; }

        public Drink Drink { get; set; }

        public Ingredient Ingredient { get; set; }
    }
}
