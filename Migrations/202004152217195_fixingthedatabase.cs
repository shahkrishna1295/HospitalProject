namespace HospitalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixingthedatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RespondWays",
                c => new
                    {
                        RespondWayID = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.RespondWayID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.RespondWays");
        }
    }
}
