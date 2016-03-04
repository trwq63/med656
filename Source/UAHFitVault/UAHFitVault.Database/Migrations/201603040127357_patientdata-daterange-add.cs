namespace UAHFitVault.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class patientdatadaterangeadd : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PatientDatas", "FromDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.PatientDatas", "ToDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.PatientDatas", "Date");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PatientDatas", "Date", c => c.DateTime(nullable: false));
            DropColumn("dbo.PatientDatas", "ToDate");
            DropColumn("dbo.PatientDatas", "FromDate");
        }
    }
}
