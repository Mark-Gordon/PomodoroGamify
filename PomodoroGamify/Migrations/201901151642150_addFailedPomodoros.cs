namespace PomodoroGamify.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addFailedPomodoros : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pomodoroes", "NumberOfFailedPomodos", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Pomodoroes", "NumberOfFailedPomodos");
        }
    }
}
