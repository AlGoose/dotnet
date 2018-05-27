namespace WebTavern.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IntRating : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Ratings", "Value", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Ratings", "Value", c => c.Double(nullable: false));
        }
    }
}
