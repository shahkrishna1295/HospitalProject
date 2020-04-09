namespace HospitalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class first : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Emergencies", newName: "EmergencyModels");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.EmergencyModels", newName: "Emergencies");
        }
    }
}
