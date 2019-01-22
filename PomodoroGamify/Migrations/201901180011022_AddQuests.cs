namespace PomodoroGamify.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddQuests : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Quests",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        QuestName = c.String(),
                        QuestDescription = c.String(),
                        LevelRequirement = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Quests");
        }
    }
}
