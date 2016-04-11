namespace UAHFitVault.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class zephsumnullablefields : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ZephyrSummaryDatas", "BreathingRate", c => c.Single());
            AlterColumn("dbo.ZephyrSummaryDatas", "SkinTemp", c => c.Single());
            AlterColumn("dbo.ZephyrSummaryDatas", "Posture", c => c.Int());
            AlterColumn("dbo.ZephyrSummaryDatas", "Activity", c => c.Single());
            AlterColumn("dbo.ZephyrSummaryDatas", "PeakAccel", c => c.Single());
            AlterColumn("dbo.ZephyrSummaryDatas", "BatteryVolts", c => c.Single());
            AlterColumn("dbo.ZephyrSummaryDatas", "BatteryLevel", c => c.Single());
            AlterColumn("dbo.ZephyrSummaryDatas", "BRAmplitude", c => c.Single());
            AlterColumn("dbo.ZephyrSummaryDatas", "BRNoise", c => c.Single());
            AlterColumn("dbo.ZephyrSummaryDatas", "BRConfidence", c => c.Int());
            AlterColumn("dbo.ZephyrSummaryDatas", "ECGAmplitude", c => c.Single());
            AlterColumn("dbo.ZephyrSummaryDatas", "ECGNoise", c => c.Single());
            AlterColumn("dbo.ZephyrSummaryDatas", "HRConfidence", c => c.Int());
            AlterColumn("dbo.ZephyrSummaryDatas", "HRV", c => c.Int());
            AlterColumn("dbo.ZephyrSummaryDatas", "SystemConfidence", c => c.Int());
            AlterColumn("dbo.ZephyrSummaryDatas", "GSR", c => c.Int());
            AlterColumn("dbo.ZephyrSummaryDatas", "ROGState", c => c.Int());
            AlterColumn("dbo.ZephyrSummaryDatas", "ROGTime", c => c.Int());
            AlterColumn("dbo.ZephyrSummaryDatas", "VerticalMin", c => c.Single());
            AlterColumn("dbo.ZephyrSummaryDatas", "VerticalPeak", c => c.Single());
            AlterColumn("dbo.ZephyrSummaryDatas", "LateralMin", c => c.Single());
            AlterColumn("dbo.ZephyrSummaryDatas", "LateralPeak", c => c.Single());
            AlterColumn("dbo.ZephyrSummaryDatas", "SagittalMin", c => c.Single());
            AlterColumn("dbo.ZephyrSummaryDatas", "SagittalPeak", c => c.Single());
            AlterColumn("dbo.ZephyrSummaryDatas", "DeviceTemp", c => c.Single());
            AlterColumn("dbo.ZephyrSummaryDatas", "StatusInfo", c => c.Int());
            AlterColumn("dbo.ZephyrSummaryDatas", "LinkQuality", c => c.Int());
            AlterColumn("dbo.ZephyrSummaryDatas", "RSSI", c => c.Int());
            AlterColumn("dbo.ZephyrSummaryDatas", "TxPower", c => c.Int());
            AlterColumn("dbo.ZephyrSummaryDatas", "CoreTemp", c => c.Single());
            AlterColumn("dbo.ZephyrSummaryDatas", "AuxADC1", c => c.Int());
            AlterColumn("dbo.ZephyrSummaryDatas", "AuxADC2", c => c.Int());
            AlterColumn("dbo.ZephyrSummaryDatas", "AuxADC3", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ZephyrSummaryDatas", "AuxADC3", c => c.Int(nullable: false));
            AlterColumn("dbo.ZephyrSummaryDatas", "AuxADC2", c => c.Int(nullable: false));
            AlterColumn("dbo.ZephyrSummaryDatas", "AuxADC1", c => c.Int(nullable: false));
            AlterColumn("dbo.ZephyrSummaryDatas", "CoreTemp", c => c.Single(nullable: false));
            AlterColumn("dbo.ZephyrSummaryDatas", "TxPower", c => c.Int(nullable: false));
            AlterColumn("dbo.ZephyrSummaryDatas", "RSSI", c => c.Int(nullable: false));
            AlterColumn("dbo.ZephyrSummaryDatas", "LinkQuality", c => c.Int(nullable: false));
            AlterColumn("dbo.ZephyrSummaryDatas", "StatusInfo", c => c.Int(nullable: false));
            AlterColumn("dbo.ZephyrSummaryDatas", "DeviceTemp", c => c.Single(nullable: false));
            AlterColumn("dbo.ZephyrSummaryDatas", "SagittalPeak", c => c.Single(nullable: false));
            AlterColumn("dbo.ZephyrSummaryDatas", "SagittalMin", c => c.Single(nullable: false));
            AlterColumn("dbo.ZephyrSummaryDatas", "LateralPeak", c => c.Single(nullable: false));
            AlterColumn("dbo.ZephyrSummaryDatas", "LateralMin", c => c.Single(nullable: false));
            AlterColumn("dbo.ZephyrSummaryDatas", "VerticalPeak", c => c.Single(nullable: false));
            AlterColumn("dbo.ZephyrSummaryDatas", "VerticalMin", c => c.Single(nullable: false));
            AlterColumn("dbo.ZephyrSummaryDatas", "ROGTime", c => c.Int(nullable: false));
            AlterColumn("dbo.ZephyrSummaryDatas", "ROGState", c => c.Int(nullable: false));
            AlterColumn("dbo.ZephyrSummaryDatas", "GSR", c => c.Int(nullable: false));
            AlterColumn("dbo.ZephyrSummaryDatas", "SystemConfidence", c => c.Int(nullable: false));
            AlterColumn("dbo.ZephyrSummaryDatas", "HRV", c => c.Int(nullable: false));
            AlterColumn("dbo.ZephyrSummaryDatas", "HRConfidence", c => c.Int(nullable: false));
            AlterColumn("dbo.ZephyrSummaryDatas", "ECGNoise", c => c.Single(nullable: false));
            AlterColumn("dbo.ZephyrSummaryDatas", "ECGAmplitude", c => c.Single(nullable: false));
            AlterColumn("dbo.ZephyrSummaryDatas", "BRConfidence", c => c.Int(nullable: false));
            AlterColumn("dbo.ZephyrSummaryDatas", "BRNoise", c => c.Single(nullable: false));
            AlterColumn("dbo.ZephyrSummaryDatas", "BRAmplitude", c => c.Single(nullable: false));
            AlterColumn("dbo.ZephyrSummaryDatas", "BatteryLevel", c => c.Single(nullable: false));
            AlterColumn("dbo.ZephyrSummaryDatas", "BatteryVolts", c => c.Single(nullable: false));
            AlterColumn("dbo.ZephyrSummaryDatas", "PeakAccel", c => c.Single(nullable: false));
            AlterColumn("dbo.ZephyrSummaryDatas", "Activity", c => c.Single(nullable: false));
            AlterColumn("dbo.ZephyrSummaryDatas", "Posture", c => c.Int(nullable: false));
            AlterColumn("dbo.ZephyrSummaryDatas", "SkinTemp", c => c.Single(nullable: false));
            AlterColumn("dbo.ZephyrSummaryDatas", "BreathingRate", c => c.Single(nullable: false));
        }
    }
}
