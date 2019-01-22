namespace PomodoroGamify.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateAverageEffectivenessToDouble : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Effectives", "AverageEffectiveRating", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Effectives", "AverageEffectiveRating", c => c.Int(nullable: false));
        }
    }
}
