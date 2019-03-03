namespace PomodoroGamify.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBadges : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserBadges",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        BronzeBadges = c.Int(nullable: false),
                        SilverBadges = c.Int(nullable: false),
                        GoldBadges = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.UserModels", "UserBadgesId", c => c.String(maxLength: 128));
            CreateIndex("dbo.UserModels", "UserBadgesId");
            AddForeignKey("dbo.UserModels", "UserBadgesId", "dbo.UserBadges", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserModels", "UserBadgesId", "dbo.UserBadges");
            DropIndex("dbo.UserModels", new[] { "UserBadgesId" });
            DropColumn("dbo.UserModels", "UserBadgesId");
            DropTable("dbo.UserBadges");
        }
    }
}
