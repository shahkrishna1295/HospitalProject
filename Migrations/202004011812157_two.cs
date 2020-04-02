namespace HospitalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class two : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Emergencies",
                c => new
                    {
                        EmergencyId = c.Int(nullable: false, identity: true),
                        Date = c.String(),
                        Title = c.String(),
                        Article = c.String(),
                        KeywordOne = c.String(),
                        KeywordTwo = c.String(),
                        KeywordThree = c.String(),
                    })
                .PrimaryKey(t => t.EmergencyId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Emergencies");
        }
    }
}
