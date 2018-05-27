using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebTavern.Models.EFModels
{
    public class Attribute
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(32)]
        public string Name { get; set; }

        public virtual ICollection<TypeAttribute> TypeAttributes { get; set; }

        public virtual ICollection<AttributeValue> AttributeValues { get; set; }
    }
}
