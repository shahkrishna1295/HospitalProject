namespace HospitalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class donation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DonationModels",
                c => new
                    {
                        DonationID = c.Int(nullable: false, identity: true),
                        DonatorName = c.String(),
                        DonatorEmail = c.String(),
                        DonationDate = c.DateTime(nullable: false),
                        DonatorPhone = c.Int(nullable: false),
                        DonationAmount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.DonationID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.DonationModels");
        }
    }
}
