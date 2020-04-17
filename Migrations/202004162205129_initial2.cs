namespace HospitalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FaqCategories",
                c => new
                    {
                        FaqCatID = c.Int(nullable: false, identity: true),
                        FaqCatName = c.String(),
                    })
                .PrimaryKey(t => t.FaqCatID);
            
            CreateTable(
                "dbo.Faqs",
                c => new
                    {
                        FaqID = c.Int(nullable: false, identity: true),
                        FaqQtn = c.String(),
                        FaqAns = c.String(),
                        FaqCatID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.FaqID)
                .ForeignKey("dbo.FaqCategories", t => t.FaqCatID, cascadeDelete: true)
                .Index(t => t.FaqCatID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Faqs", "FaqCatID", "dbo.FaqCategories");
            DropIndex("dbo.Faqs", new[] { "FaqCatID" });
            DropTable("dbo.Faqs");
            DropTable("dbo.FaqCategories");
        }
    }
}
