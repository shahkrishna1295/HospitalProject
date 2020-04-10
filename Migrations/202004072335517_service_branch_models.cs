namespace HospitalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class service_branch_models : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Emergencies", newName: "EmergencyModels");
            AddColumn("dbo.BranchModel", "BranchAddress", c => c.String());
            DropColumn("dbo.BranchModel", "BranchLocation");
        }
        
        public override void Down()
        {
            AddColumn("dbo.BranchModel", "BranchLocation", c => c.String());
            DropColumn("dbo.BranchModel", "BranchAddress");
            RenameTable(name: "dbo.EmergencyModels", newName: "Emergencies");
        }
    }
}
