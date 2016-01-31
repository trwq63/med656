namespace UAHFitVault.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class devadmintoolbranchmerge : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.BasisPeakSummaryDatas", "PatientDataId", "dbo.PatientDatas");
            DropForeignKey("dbo.ZephyrAccelerometers", "PatientDataId", "dbo.PatientDatas");
            DropForeignKey("dbo.ZephyrBreathingWaveforms", "PatientDataId", "dbo.PatientDatas");
            DropForeignKey("dbo.ZephyrECGWaveforms", "PatientDataId", "dbo.PatientDatas");
            DropForeignKey("dbo.ZephyrEventDatas", "PatientDataId", "dbo.PatientDatas");
            DropForeignKey("dbo.ZephyrSummaryDatas", "PatientDataId", "dbo.PatientDatas");
            DropIndex("dbo.BasisPeakSummaryDatas", new[] { "PatientDataId" });
            DropIndex("dbo.ZephyrAccelerometers", new[] { "PatientDataId" });
            DropIndex("dbo.ZephyrBreathingWaveforms", new[] { "PatientDataId" });
            DropIndex("dbo.ZephyrECGWaveforms", new[] { "PatientDataId" });
            DropIndex("dbo.ZephyrEventDatas", new[] { "PatientDataId" });
            DropIndex("dbo.ZephyrSummaryDatas", new[] { "PatientDataId" });
            RenameColumn(table: "dbo.PatientDatas", name: "MedicalDeviceId", newName: "MedicalDevice_Id");
            RenameIndex(table: "dbo.PatientDatas", name: "IX_MedicalDeviceId", newName: "IX_MedicalDevice_Id");
            AddColumn("dbo.AccountRequests", "ReasonForAccount", c => c.String(nullable: false));
            AlterColumn("dbo.BasisPeakSummaryDatas", "Calories", c => c.Single(nullable: false));
            AlterColumn("dbo.BasisPeakSummaryDatas", "GSR", c => c.Single(nullable: false));
            AlterColumn("dbo.BasisPeakSummaryDatas", "HeartRate", c => c.Int(nullable: false));
            AlterColumn("dbo.BasisPeakSummaryDatas", "SkinTemp", c => c.Single(nullable: false));
            DropColumn("dbo.AccountRequests", "Request");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AccountRequests", "Request", c => c.String(nullable: false));
            AlterColumn("dbo.BasisPeakSummaryDatas", "SkinTemp", c => c.Single());
            AlterColumn("dbo.BasisPeakSummaryDatas", "HeartRate", c => c.Int());
            AlterColumn("dbo.BasisPeakSummaryDatas", "GSR", c => c.Single());
            AlterColumn("dbo.BasisPeakSummaryDatas", "Calories", c => c.Single());
            DropColumn("dbo.AccountRequests", "ReasonForAccount");
            RenameIndex(table: "dbo.PatientDatas", name: "IX_MedicalDevice_Id", newName: "IX_MedicalDeviceId");
            RenameColumn(table: "dbo.PatientDatas", name: "MedicalDevice_Id", newName: "MedicalDeviceId");
            CreateIndex("dbo.ZephyrSummaryDatas", "PatientDataId");
            CreateIndex("dbo.ZephyrEventDatas", "PatientDataId");
            CreateIndex("dbo.ZephyrECGWaveforms", "PatientDataId");
            CreateIndex("dbo.ZephyrBreathingWaveforms", "PatientDataId");
            CreateIndex("dbo.ZephyrAccelerometers", "PatientDataId");
            CreateIndex("dbo.BasisPeakSummaryDatas", "PatientDataId");
            AddForeignKey("dbo.ZephyrSummaryDatas", "PatientDataId", "dbo.PatientDatas", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ZephyrEventDatas", "PatientDataId", "dbo.PatientDatas", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ZephyrECGWaveforms", "PatientDataId", "dbo.PatientDatas", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ZephyrBreathingWaveforms", "PatientDataId", "dbo.PatientDatas", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ZephyrAccelerometers", "PatientDataId", "dbo.PatientDatas", "Id", cascadeDelete: true);
            AddForeignKey("dbo.BasisPeakSummaryDatas", "PatientDataId", "dbo.PatientDatas", "Id", cascadeDelete: true);
        }
    }
}
