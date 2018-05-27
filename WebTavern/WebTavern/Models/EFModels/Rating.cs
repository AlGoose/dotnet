using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebTavern.Models.EFModels
{
    public class Rating
    {   
        [Key]
        public int Id { get; set; }

        [ForeignKey("Drink")]
        public int DrinkId { get; set; }

        [Required]
        public int Value { get; set; }

        public virtual Drink Drink { get; set; }
    }
}
