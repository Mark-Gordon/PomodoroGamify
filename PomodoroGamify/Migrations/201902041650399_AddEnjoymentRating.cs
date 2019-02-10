namespace PomodoroGamify.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddEnjoymentRating : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Enjoyments",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        AverageEnjoymentRating = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.UserModels", "EnjoymentID", c => c.String(maxLength: 128));
            CreateIndex("dbo.UserModels", "EnjoymentID");
            AddForeignKey("dbo.UserModels", "EnjoymentID", "dbo.Enjoyments", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserModels", "EnjoymentID", "dbo.Enjoyments");
            DropIndex("dbo.UserModels", new[] { "EnjoymentID" });
            DropColumn("dbo.UserModels", "EnjoymentID");
            DropTable("dbo.Enjoyments");
        }
    }
}
