namespace PomodoroGamify.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddEffective : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Effectives",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        AverageEffectiveRating = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.UserModels", "EffectiveID", c => c.String(maxLength: 128));
            CreateIndex("dbo.UserModels", "EffectiveID");
            AddForeignKey("dbo.UserModels", "EffectiveID", "dbo.Effectives", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserModels", "EffectiveID", "dbo.Effectives");
            DropIndex("dbo.UserModels", new[] { "EffectiveID" });
            DropColumn("dbo.UserModels", "EffectiveID");
            DropTable("dbo.Effectives");
        }
    }
}
