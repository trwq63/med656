namespace UAHFitVault.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class basissummarforeignkey2_12916 : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.BasisPeakSummaryDatas", "PatientDataId");
            AddForeignKey("dbo.BasisPeakSummaryDatas", "PatientDataId", "dbo.PatientDatas", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BasisPeakSummaryDatas", "PatientDataId", "dbo.PatientDatas");
            DropIndex("dbo.BasisPeakSummaryDatas", new[] { "PatientDataId" });
        }
    }
}
