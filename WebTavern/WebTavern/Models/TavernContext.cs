using WebTavern.Models.EFModels;

namespace WebTavern.Models
{
    using System.Data.Entity;

    public class TavernContext : DbContext
    {
        public TavernContext() : base("TavernContext")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }

        public DbSet<Drink> Drinks { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Type> Types { get; set; }
        public DbSet<Attribute> Attributes { get; set; }
        public DbSet<AttributeValue> AttributeValues { get; set; }
        public DbSet<TypeAttribute> TypeAttributes { get; set; }
    }
}