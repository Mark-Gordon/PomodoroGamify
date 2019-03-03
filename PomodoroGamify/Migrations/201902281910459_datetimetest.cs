namespace PomodoroGamify.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class datetimetest : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PomodoroLogs", "DateCompleted", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PomodoroLogs", "DateCompleted", c => c.String());
        }
    }
}
