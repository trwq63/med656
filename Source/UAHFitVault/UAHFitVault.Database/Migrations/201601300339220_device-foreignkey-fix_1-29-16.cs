namespace UAHFitVault.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deviceforeignkeyfix_12916 : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.ZephyrAccelerometers", "PatientDataId");
            CreateIndex("dbo.ZephyrBreathingWaveforms", "PatientDataId");
            CreateIndex("dbo.ZephyrECGWaveforms", "PatientDataId");
            CreateIndex("dbo.ZephyrEventDatas", "PatientDataId");
            CreateIndex("dbo.ZephyrSummaryDatas", "PatientDataId");
            AddForeignKey("dbo.ZephyrAccelerometers", "PatientDataId", "dbo.PatientDatas", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ZephyrBreathingWaveforms", "PatientDataId", "dbo.PatientDatas", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ZephyrECGWaveforms", "PatientDataId", "dbo.PatientDatas", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ZephyrEventDatas", "PatientDataId", "dbo.PatientDatas", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ZephyrSummaryDatas", "PatientDataId", "dbo.PatientDatas", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ZephyrSummaryDatas", "PatientDataId", "dbo.PatientDatas");
            DropForeignKey("dbo.ZephyrEventDatas", "PatientDataId", "dbo.PatientDatas");
            DropForeignKey("dbo.ZephyrECGWaveforms", "PatientDataId", "dbo.PatientDatas");
            DropForeignKey("dbo.ZephyrBreathingWaveforms", "PatientDataId", "dbo.PatientDatas");
            DropForeignKey("dbo.ZephyrAccelerometers", "PatientDataId", "dbo.PatientDatas");
            DropIndex("dbo.ZephyrSummaryDatas", new[] { "PatientDataId" });
            DropIndex("dbo.ZephyrEventDatas", new[] { "PatientDataId" });
            DropIndex("dbo.ZephyrECGWaveforms", new[] { "PatientDataId" });
            DropIndex("dbo.ZephyrBreathingWaveforms", new[] { "PatientDataId" });
            DropIndex("dbo.ZephyrAccelerometers", new[] { "PatientDataId" });
        }
    }
}
