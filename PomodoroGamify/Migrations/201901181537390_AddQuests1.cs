namespace PomodoroGamify.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddQuests1 : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Quests (Id, QuestName, QuestDescription, LevelRequirement) VALUES ('0', 'Quest1', 'Kill the goblin!', 1)");
            Sql("INSERT INTO Quests (Id, QuestName, QuestDescription, LevelRequirement) VALUES ('1', 'Quest2', 'Kill the monster!', 5)");
            Sql("INSERT INTO Quests (Id, QuestName, QuestDescription, LevelRequirement) VALUES ('2', 'Quest3', 'Kill the werewolf!', 10)");
            Sql("INSERT INTO Quests (Id, QuestName, QuestDescription, LevelRequirement) VALUES ('3', 'Quest4', 'Kill the ghoul!', 15)");
            Sql("INSERT INTO Quests (Id, QuestName, QuestDescription, LevelRequirement) VALUES ('4', 'Quest5', 'Kill the dragon!', 20)");
        }
        
        public override void Down()
        {
        }
    }
}
