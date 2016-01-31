namespace UAHFitVault.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class zephyrsummarydata2_12816 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ZephyrSummaryDatas", "BRAmplitude", c => c.Single(nullable: false));
            AlterColumn("dbo.ZephyrSummaryDatas", "BRNoise", c => c.Single(nullable: false));
            AlterColumn("dbo.ZephyrSummaryDatas", "ECGAmplitude", c => c.Single(nullable: false));
            AlterColumn("dbo.ZephyrSummaryDatas", "ECGNoise", c => c.Single(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ZephyrSummaryDatas", "ECGNoise", c => c.Int(nullable: false));
            AlterColumn("dbo.ZephyrSummaryDatas", "ECGAmplitude", c => c.Int(nullable: false));
            AlterColumn("dbo.ZephyrSummaryDatas", "BRNoise", c => c.Int(nullable: false));
            AlterColumn("dbo.ZephyrSummaryDatas", "BRAmplitude", c => c.Int(nullable: false));
        }
    }
}
