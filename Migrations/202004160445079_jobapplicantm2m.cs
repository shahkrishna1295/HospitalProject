namespace HospitalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class jobapplicantm2m : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.JobModelApplicantModels",
                c => new
                    {
                        JobModel_JobID = c.Int(nullable: false),
                        ApplicantModel_ApplicantID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.JobModel_JobID, t.ApplicantModel_ApplicantID })
                .ForeignKey("dbo.JobModels", t => t.JobModel_JobID, cascadeDelete: true)
                .ForeignKey("dbo.ApplicantModels", t => t.ApplicantModel_ApplicantID, cascadeDelete: true)
                .Index(t => t.JobModel_JobID)
                .Index(t => t.ApplicantModel_ApplicantID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.JobModelApplicantModels", "ApplicantModel_ApplicantID", "dbo.ApplicantModels");
            DropForeignKey("dbo.JobModelApplicantModels", "JobModel_JobID", "dbo.JobModels");
            DropIndex("dbo.JobModelApplicantModels", new[] { "ApplicantModel_ApplicantID" });
            DropIndex("dbo.JobModelApplicantModels", new[] { "JobModel_JobID" });
            DropTable("dbo.JobModelApplicantModels");
        }
    }
}
