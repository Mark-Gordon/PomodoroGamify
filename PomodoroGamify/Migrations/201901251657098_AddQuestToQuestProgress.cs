namespace PomodoroGamify.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddQuestToQuestProgress : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.UserQuestProgresses", "QuestId", c => c.String(maxLength: 128));
            CreateIndex("dbo.UserQuestProgresses", "QuestId");
            AddForeignKey("dbo.UserQuestProgresses", "QuestId", "dbo.Quests", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserQuestProgresses", "QuestId", "dbo.Quests");
            DropIndex("dbo.UserQuestProgresses", new[] { "QuestId" });
            AlterColumn("dbo.UserQuestProgresses", "QuestId", c => c.String());
        }
    }
}
