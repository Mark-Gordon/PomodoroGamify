namespace PomodoroGamify.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPomodoro : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Pomodoroes",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        NumberOfPomodoros = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.UserModels", "PomodoroId", c => c.String(maxLength: 128));
            CreateIndex("dbo.UserModels", "PomodoroId");
            AddForeignKey("dbo.UserModels", "PomodoroId", "dbo.Pomodoroes", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserModels", "PomodoroId", "dbo.Pomodoroes");
            DropIndex("dbo.UserModels", new[] { "PomodoroId" });
            DropColumn("dbo.UserModels", "PomodoroId");
            DropTable("dbo.Pomodoroes");
        }
    }
}
