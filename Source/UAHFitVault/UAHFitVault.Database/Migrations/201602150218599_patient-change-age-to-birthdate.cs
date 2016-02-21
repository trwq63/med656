namespace UAHFitVault.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class patientchangeagetobirthdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Patients", "Birthdate", c => c.DateTime(nullable: false));
            DropColumn("dbo.Patients", "Age");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Patients", "Age", c => c.Int(nullable: false));
            DropColumn("dbo.Patients", "Birthdate");
        }
    }
}
