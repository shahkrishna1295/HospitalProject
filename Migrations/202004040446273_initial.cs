namespace HospitalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
               "dbo.AspNetRoles",
               c => new
               {
                   Id = c.String(nullable: false, maxLength: 128),
                   Name = c.String(nullable: false, maxLength: 256),
               })
               .PrimaryKey(t => t.Id)
               .Index(t => t.Name, unique: true, name: "RoleNameIndex");

            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                {
                    UserId = c.String(nullable: false, maxLength: 128),
                    RoleId = c.String(nullable: false, maxLength: 128),
                })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);

            CreateTable(
                "dbo.AspNetUsers",
                c => new
                {
                    Id = c.String(nullable: false, maxLength: 128),
                    Email = c.String(maxLength: 256),
                    EmailConfirmed = c.Boolean(nullable: false),
                    PasswordHash = c.String(),
                    SecurityStamp = c.String(),
                    PhoneNumber = c.String(),
                    PhoneNumberConfirmed = c.Boolean(nullable: false),
                    TwoFactorEnabled = c.Boolean(nullable: false),
                    LockoutEndDateUtc = c.DateTime(),
                    LockoutEnabled = c.Boolean(nullable: false),
                    AccessFailedCount = c.Int(nullable: false),
                    UserName = c.String(nullable: false, maxLength: 256),
                })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");

            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    UserId = c.String(nullable: false, maxLength: 128),
                    ClaimType = c.String(),
                    ClaimValue = c.String(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);

            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                {
                    LoginProvider = c.String(nullable: false, maxLength: 128),
                    ProviderKey = c.String(nullable: false, maxLength: 128),
                    UserId = c.String(nullable: false, maxLength: 128),
                })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);

            CreateTable(
                "dbo.BranchModel",
                c => new
                    {
                        BranchID = c.Int(nullable: false, identity: true),
                        BranchName = c.String(),
                        BranchLocation = c.String(),
                        BranchContactNumber = c.String(),
                        BranchEmail = c.String(),
                        BranchImage = c.String(),
                    })
                .PrimaryKey(t => t.BranchID);
            
            CreateTable(
                "dbo.Emergencies",
                c => new
                    {
                        EmergencyId = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Title = c.String(),
                        Article = c.String(),
                        KeywordOne = c.String(),
                        KeywordTwo = c.String(),
                        KeywordThree = c.String(),
                    })
                .PrimaryKey(t => t.EmergencyId);
            
            CreateTable(
                "dbo.ServiceModel",
                c => new
                    {
                        ServiceID = c.Int(nullable: false, identity: true),
                        ServiceTitle = c.String(),
                        ServiceCategory = c.String(),
                    })
                .PrimaryKey(t => t.ServiceID);
            
            CreateTable(
                "dbo.TestimonialModel",
                c => new
                    {
                        TestimonialID = c.Int(nullable: false, identity: true),
                        TestimonialTitle = c.String(),
                        TestimonialMessage = c.String(),
                        TestimonialAttachment = c.String(),
                        NameOfPatient = c.String(),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.TestimonialID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.TestimonialModels");
            DropTable("dbo.ServiceModels");
            DropTable("dbo.Emergencies");
            DropTable("dbo.BranchModels");
        }
    }
}
