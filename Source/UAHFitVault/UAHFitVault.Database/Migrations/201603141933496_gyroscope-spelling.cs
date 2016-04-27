namespace UAHFitVault.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class gyroscopespelling : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.MSBandGryoscopes", newName: "MSBandGyroscopes");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.MSBandGyroscopes", newName: "MSBandGryoscopes");
        }
    }
}
