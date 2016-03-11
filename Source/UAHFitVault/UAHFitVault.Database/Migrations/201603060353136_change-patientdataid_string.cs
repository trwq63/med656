namespace UAHFitVault.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changepatientdataid_string : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Activities", "PatientData_Id", "dbo.PatientDatas");
            DropForeignKey("dbo.BasisPeakSummaryDatas", "PatientDataId", "dbo.PatientDatas");
            DropForeignKey("dbo.ZephyrAccelerometers", "PatientDataId", "dbo.PatientDatas");
            DropForeignKey("dbo.ZephyrBreathingWaveforms", "PatientDataId", "dbo.PatientDatas");
            DropForeignKey("dbo.ZephyrECGWaveforms", "PatientDataId", "dbo.PatientDatas");
            DropForeignKey("dbo.ZephyrEventDatas", "PatientDataId", "dbo.PatientDatas");
            DropForeignKey("dbo.ZephyrSummaryDatas", "PatientDataId", "dbo.PatientDatas");
            DropIndex("dbo.Activities", new[] { "PatientData_Id" });
            DropIndex("dbo.BasisPeakSummaryDatas", new[] { "PatientDataId" });
            DropIndex("dbo.ZephyrAccelerometers", new[] { "PatientDataId" });
            DropIndex("dbo.ZephyrBreathingWaveforms", new[] { "PatientDataId" });
            DropIndex("dbo.ZephyrECGWaveforms", new[] { "PatientDataId" });
            DropIndex("dbo.ZephyrEventDatas", new[] { "PatientDataId" });
            DropIndex("dbo.ZephyrSummaryDatas", new[] { "PatientDataId" });
            DropPrimaryKey("dbo.PatientDatas");
            AlterColumn("dbo.Activities", "PatientData_Id", c => c.String(maxLength: 128));
            AlterColumn("dbo.BasisPeakSummaryDatas", "PatientDataId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.PatientDatas", "Id", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.MSBandAccelerometers", "PatientDataId", c => c.String(nullable: false));
            AlterColumn("dbo.MSBandCalories", "PatientDataId", c => c.String(nullable: false));
            AlterColumn("dbo.MSBandDistances", "PatientDataId", c => c.String(nullable: false));
            AlterColumn("dbo.MSBandGryoscopes", "PatientDataId", c => c.String(nullable: false));
            AlterColumn("dbo.MSBandHeartRates", "PatientDataId", c => c.String(nullable: false));
            AlterColumn("dbo.MSBandPedometers", "PatientDataId", c => c.String(nullable: false));
            AlterColumn("dbo.MSBandTemperatures", "PatientDataId", c => c.String(nullable: false));
            AlterColumn("dbo.MSBandUVs", "PatientDataId", c => c.String(nullable: false));
            AlterColumn("dbo.ZephyrAccelerometers", "PatientDataId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.ZephyrBreathingWaveforms", "PatientDataId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.ZephyrECGWaveforms", "PatientDataId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.ZephyrEventDatas", "PatientDataId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.ZephyrSummaryDatas", "PatientDataId", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.PatientDatas", "Id");
            CreateIndex("dbo.Activities", "PatientData_Id");
            CreateIndex("dbo.BasisPeakSummaryDatas", "PatientDataId");
            CreateIndex("dbo.ZephyrAccelerometers", "PatientDataId");
            CreateIndex("dbo.ZephyrBreathingWaveforms", "PatientDataId");
            CreateIndex("dbo.ZephyrECGWaveforms", "PatientDataId");
            CreateIndex("dbo.ZephyrEventDatas", "PatientDataId");
            CreateIndex("dbo.ZephyrSummaryDatas", "PatientDataId");
            AddForeignKey("dbo.Activities", "PatientData_Id", "dbo.PatientDatas", "Id");
            AddForeignKey("dbo.BasisPeakSummaryDatas", "PatientDataId", "dbo.PatientDatas", "Id", cascadeDelete: true);
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
            DropForeignKey("dbo.BasisPeakSummaryDatas", "PatientDataId", "dbo.PatientDatas");
            DropForeignKey("dbo.Activities", "PatientData_Id", "dbo.PatientDatas");
            DropIndex("dbo.ZephyrSummaryDatas", new[] { "PatientDataId" });
            DropIndex("dbo.ZephyrEventDatas", new[] { "PatientDataId" });
            DropIndex("dbo.ZephyrECGWaveforms", new[] { "PatientDataId" });
            DropIndex("dbo.ZephyrBreathingWaveforms", new[] { "PatientDataId" });
            DropIndex("dbo.ZephyrAccelerometers", new[] { "PatientDataId" });
            DropIndex("dbo.BasisPeakSummaryDatas", new[] { "PatientDataId" });
            DropIndex("dbo.Activities", new[] { "PatientData_Id" });
            DropPrimaryKey("dbo.PatientDatas");
            AlterColumn("dbo.ZephyrSummaryDatas", "PatientDataId", c => c.Guid(nullable: false));
            AlterColumn("dbo.ZephyrEventDatas", "PatientDataId", c => c.Guid(nullable: false));
            AlterColumn("dbo.ZephyrECGWaveforms", "PatientDataId", c => c.Guid(nullable: false));
            AlterColumn("dbo.ZephyrBreathingWaveforms", "PatientDataId", c => c.Guid(nullable: false));
            AlterColumn("dbo.ZephyrAccelerometers", "PatientDataId", c => c.Guid(nullable: false));
            AlterColumn("dbo.MSBandUVs", "PatientDataId", c => c.Guid(nullable: false));
            AlterColumn("dbo.MSBandTemperatures", "PatientDataId", c => c.Guid(nullable: false));
            AlterColumn("dbo.MSBandPedometers", "PatientDataId", c => c.Guid(nullable: false));
            AlterColumn("dbo.MSBandHeartRates", "PatientDataId", c => c.Guid(nullable: false));
            AlterColumn("dbo.MSBandGryoscopes", "PatientDataId", c => c.Guid(nullable: false));
            AlterColumn("dbo.MSBandDistances", "PatientDataId", c => c.Guid(nullable: false));
            AlterColumn("dbo.MSBandCalories", "PatientDataId", c => c.Guid(nullable: false));
            AlterColumn("dbo.MSBandAccelerometers", "PatientDataId", c => c.Guid(nullable: false));
            AlterColumn("dbo.PatientDatas", "Id", c => c.Guid(nullable: false));
            AlterColumn("dbo.BasisPeakSummaryDatas", "PatientDataId", c => c.Guid(nullable: false));
            AlterColumn("dbo.Activities", "PatientData_Id", c => c.Guid());
            AddPrimaryKey("dbo.PatientDatas", "Id");
            CreateIndex("dbo.ZephyrSummaryDatas", "PatientDataId");
            CreateIndex("dbo.ZephyrEventDatas", "PatientDataId");
            CreateIndex("dbo.ZephyrECGWaveforms", "PatientDataId");
            CreateIndex("dbo.ZephyrBreathingWaveforms", "PatientDataId");
            CreateIndex("dbo.ZephyrAccelerometers", "PatientDataId");
            CreateIndex("dbo.BasisPeakSummaryDatas", "PatientDataId");
            CreateIndex("dbo.Activities", "PatientData_Id");
            AddForeignKey("dbo.ZephyrSummaryDatas", "PatientDataId", "dbo.PatientDatas", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ZephyrEventDatas", "PatientDataId", "dbo.PatientDatas", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ZephyrECGWaveforms", "PatientDataId", "dbo.PatientDatas", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ZephyrBreathingWaveforms", "PatientDataId", "dbo.PatientDatas", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ZephyrAccelerometers", "PatientDataId", "dbo.PatientDatas", "Id", cascadeDelete: true);
            AddForeignKey("dbo.BasisPeakSummaryDatas", "PatientDataId", "dbo.PatientDatas", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Activities", "PatientData_Id", "dbo.PatientDatas", "Id");
        }
    }
}
