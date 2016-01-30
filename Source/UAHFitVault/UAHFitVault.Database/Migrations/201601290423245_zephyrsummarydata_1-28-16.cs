namespace UAHFitVault.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class zephyrsummarydata_12816 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ZephyrSummaryDatas", "BreathingRate", c => c.Single(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ZephyrSummaryDatas", "BreathingRate", c => c.Int(nullable: false));
        }
    }
}
