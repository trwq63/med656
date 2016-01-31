namespace UAHFitVault.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class basispeaknullablefields_12916 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.BasisPeakSummaryDatas", "Calories", c => c.Single());
            AlterColumn("dbo.BasisPeakSummaryDatas", "GSR", c => c.Single());
            AlterColumn("dbo.BasisPeakSummaryDatas", "HeartRate", c => c.Int());
            AlterColumn("dbo.BasisPeakSummaryDatas", "SkinTemp", c => c.Single());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.BasisPeakSummaryDatas", "SkinTemp", c => c.Single(nullable: false));
            AlterColumn("dbo.BasisPeakSummaryDatas", "HeartRate", c => c.Int(nullable: false));
            AlterColumn("dbo.BasisPeakSummaryDatas", "GSR", c => c.Single(nullable: false));
            AlterColumn("dbo.BasisPeakSummaryDatas", "Calories", c => c.Single(nullable: false));
        }
    }
}
