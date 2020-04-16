namespace HospitalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class applicant : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ApplicantModels",
                c => new
                    {
                        ApplicantID = c.Int(nullable: false, identity: true),
                        ApplicantFirstName = c.String(),
                        ApplicantLastName = c.String(),
                        ApplicantAddress = c.String(),
                        ApplicantEmail = c.String(),
                        ApplicantPhone = c.Int(nullable: false),
                        ApplicantEducationSummary = c.String(),
                        ApplicantWorkExperience = c.String(),
                        ApplicantSkills = c.String(),
                    })
                .PrimaryKey(t => t.ApplicantID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ApplicantModels");
        }
    }
}
