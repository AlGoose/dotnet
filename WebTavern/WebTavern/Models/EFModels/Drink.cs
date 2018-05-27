using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebTavern.Models.EFModels
{
    public class Drink
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(64)]
        public string Name { get; set; }

        public double Rating { get; set; }

        public byte[] Image { get; set; }

        public virtual ICollection<Recipe> Recipes { get; set; }

        public virtual ICollection<Rating> Ratings { get; set; }
    }
}
