namespace WebTavern.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DrinkPicture : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Drinks", "Image", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Drinks", "Image");
        }
    }
}
