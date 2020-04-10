namespace HospitalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class job : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.JobModels",
                c => new
                    {
                        JobID = c.Int(nullable: false, identity: true),
                        JobTitle = c.String(),
                        JobDepartmentName = c.String(),
                        JobPostedDate = c.DateTime(nullable: false),
                        JobDescription = c.String(),
                        JobRequirements = c.String(),
                    })
                .PrimaryKey(t => t.JobID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.JobModels");
        }
    }
}
