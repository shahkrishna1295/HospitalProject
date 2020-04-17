namespace HospitalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class inital : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DocReviews",
                c => new
                    {
                        DocReviewID = c.Int(nullable: false, identity: true),
                        DocReviewDesc = c.String(),
                        DocReviewDate = c.DateTime(nullable: false),
                        DocRating = c.Int(nullable: false),
                        DocID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.DocReviewID)
                .ForeignKey("dbo.Doctors", t => t.DocID, cascadeDelete: true)
                .Index(t => t.DocID);
            
            CreateTable(
                "dbo.Doctors",
                c => new
                    {
                        DocID = c.Int(nullable: false, identity: true),
                        DocFname = c.String(),
                        DocLname = c.String(),
                        DocEmail = c.String(),
                        DocHLoc = c.String(),
                    })
                .PrimaryKey(t => t.DocID);
            
            CreateTable(
                "dbo.Specialities",
                c => new
                    {
                        SpecialityID = c.Int(nullable: false, identity: true),
                        SpecialityName = c.String(),
                    })
                .PrimaryKey(t => t.SpecialityID);
            
            CreateTable(
                "dbo.SpecialityDoctors",
                c => new
                    {
                        Speciality_SpecialityID = c.Int(nullable: false),
                        Doctor_DocID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Speciality_SpecialityID, t.Doctor_DocID })
                .ForeignKey("dbo.Specialities", t => t.Speciality_SpecialityID, cascadeDelete: true)
                .ForeignKey("dbo.Doctors", t => t.Doctor_DocID, cascadeDelete: true)
                .Index(t => t.Speciality_SpecialityID)
                .Index(t => t.Doctor_DocID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SpecialityDoctors", "Doctor_DocID", "dbo.Doctors");
            DropForeignKey("dbo.SpecialityDoctors", "Speciality_SpecialityID", "dbo.Specialities");
            DropForeignKey("dbo.DocReviews", "DocID", "dbo.Doctors");
            DropIndex("dbo.SpecialityDoctors", new[] { "Doctor_DocID" });
            DropIndex("dbo.SpecialityDoctors", new[] { "Speciality_SpecialityID" });
            DropIndex("dbo.DocReviews", new[] { "DocID" });
            DropTable("dbo.SpecialityDoctors");
            DropTable("dbo.Specialities");
            DropTable("dbo.Doctors");
            DropTable("dbo.DocReviews");
        }
    }
}
