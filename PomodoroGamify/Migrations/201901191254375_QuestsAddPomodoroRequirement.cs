namespace PomodoroGamify.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class QuestsAddPomodoroRequirement : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Quests", "NumberOfPomodorosToComplete", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Quests", "NumberOfPomodorosToComplete");
        }
    }
}
