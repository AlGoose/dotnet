namespace WebTavern.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeletePicture : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.Pictures");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Pictures",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DrinkId = c.Int(nullable: false),
                        Name = c.String(),
                        Image = c.Binary(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
    }
}
