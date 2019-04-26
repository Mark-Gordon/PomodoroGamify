namespace PomodoroGamify.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeperateLevelling : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Levellings",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Experience = c.Int(nullable: false),
                        Level = c.Int(nullable: false),
                        ExperienceOfNextLevel = c.Int(nullable: false),
                        ExperienceOfCurrentLevel = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.UserModels", "LevellingID", c => c.String(maxLength: 128));
            CreateIndex("dbo.UserModels", "LevellingID");
            AddForeignKey("dbo.UserModels", "LevellingID", "dbo.Levellings", "Id");
            DropColumn("dbo.UserModels", "Experience");
            DropColumn("dbo.UserModels", "Level");
            DropColumn("dbo.UserModels", "ExperienceOfNextLevel");
            DropColumn("dbo.UserModels", "ExperienceOfCurrentLevel");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserModels", "ExperienceOfCurrentLevel", c => c.Int(nullable: false));
            AddColumn("dbo.UserModels", "ExperienceOfNextLevel", c => c.Int(nullable: false));
            AddColumn("dbo.UserModels", "Level", c => c.Int(nullable: false));
            AddColumn("dbo.UserModels", "Experience", c => c.Int(nullable: false));
            DropForeignKey("dbo.UserModels", "LevellingID", "dbo.Levellings");
            DropIndex("dbo.UserModels", new[] { "LevellingID" });
            DropColumn("dbo.UserModels", "LevellingID");
            DropTable("dbo.Levellings");
        }
    }
}
