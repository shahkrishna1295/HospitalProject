namespace HospitalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class datetime1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Keywords", "KeywordId", "dbo.Emergencies");
            DropIndex("dbo.Keywords", new[] { "KeywordId" });
            DropColumn("dbo.Emergencies", "KeywordId");
            DropTable("dbo.Keywords");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Keywords",
                c => new
                    {
                        KeywordId = c.Int(nullable: false),
                        KeywordDesc = c.String(),
                    })
                .PrimaryKey(t => t.KeywordId);
            
            AddColumn("dbo.Emergencies", "KeywordId", c => c.Int(nullable: false));
            CreateIndex("dbo.Keywords", "KeywordId");
            AddForeignKey("dbo.Keywords", "KeywordId", "dbo.Emergencies", "EmergencyId");
        }
    }
}
