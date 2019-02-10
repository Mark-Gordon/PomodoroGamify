namespace PomodoroGamify.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class asd : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.UserQuestProgresses");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.UserQuestProgresses",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        QuestId = c.String(),
                        ProgressPomodoros = c.Int(nullable: false),
                        UserModelID = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
    }
}
