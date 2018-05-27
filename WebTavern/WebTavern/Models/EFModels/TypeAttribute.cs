using System.ComponentModel.DataAnnotations.Schema;

namespace WebTavern.Models.EFModels
{
    public class TypeAttribute
    {
        public int Id { get; set; }

        [ForeignKey("Type")]
        public int TypeId { get; set; }

        [ForeignKey("Attribute")]
        public int AttributeId { get; set; }

        public Type Type { get; set; }

        public Attribute Attribute { get; set; }
    }
}
