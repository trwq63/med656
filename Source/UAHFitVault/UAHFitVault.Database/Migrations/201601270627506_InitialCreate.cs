namespace UAHFitVault.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Activities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Timestamp = c.DateTime(nullable: false),
                        DataActivity = c.Int(nullable: false),
                        PatientData_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PatientDatas", t => t.PatientData_Id)
                .Index(t => t.PatientData_Id);
            
            CreateTable(
                "dbo.BasisPeakSummaryDatas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Calories = c.Single(nullable: false),
                        GSR = c.Single(nullable: false),
                        HeartRate = c.Int(nullable: false),
                        SkinTemp = c.Single(nullable: false),
                        Steps = c.Int(nullable: false),
                        PatientDataId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ExperimentAdministrators",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        Address = c.String(),
                        Email = c.String(nullable: false),
                        PhoneNumber = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Experiments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        LastModified = c.DateTime(nullable: false),
                        QueryString = c.String(nullable: false),
                        ExperimentAdministrator_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ExperimentAdministrators", t => t.ExperimentAdministrator_Id, cascadeDelete: true)
                .Index(t => t.ExperimentAdministrator_Id);
            
            CreateTable(
                "dbo.MedicalDevices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MSBandAccelerometers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Vertical = c.Single(nullable: false),
                        Lateral = c.Single(nullable: false),
                        Sagittal = c.Single(nullable: false),
                        PatientDataId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MSBandCalories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Total = c.Int(nullable: false),
                        PatientDataId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MSBandDistances",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        MotionType = c.String(nullable: false),
                        Pace = c.Single(nullable: false),
                        Speed = c.Single(nullable: false),
                        Total = c.Single(nullable: false),
                        PatientDataId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MSBandGryoscopes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Timestamp = c.DateTime(nullable: false),
                        X = c.Single(nullable: false),
                        Y = c.Single(nullable: false),
                        Z = c.Single(nullable: false),
                        PatientDataId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MSBandHeartRates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        ReadStatus = c.String(nullable: false),
                        HeartRate = c.Int(nullable: false),
                        PatientDataId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MSBandPedometers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Steps = c.Int(nullable: false),
                        PatientDataId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MSBandTemperatures",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Temperature = c.Single(nullable: false),
                        PatientDataId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MSBandUVs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        UVIndex = c.Int(nullable: false),
                        PatientDataId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PatientDatas",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false),
                        Date = c.DateTime(nullable: false),
                        UploadDate = c.DateTime(nullable: false),
                        DataType = c.Int(nullable: false),
                        MedicalDevice_Id = c.Int(nullable: false),
                        Patient_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MedicalDevices", t => t.MedicalDevice_Id, cascadeDelete: true)
                .ForeignKey("dbo.Patients", t => t.Patient_Id, cascadeDelete: true)
                .Index(t => t.MedicalDevice_Id)
                .Index(t => t.Patient_Id);
            
            CreateTable(
                "dbo.Patients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Age = c.Int(nullable: false),
                        Weight = c.Single(nullable: false),
                        Height = c.Int(nullable: false),
                        Location = c.Int(nullable: false),
                        Ethnicity = c.Int(nullable: false),
                        Race = c.Int(nullable: false),
                        Gender = c.Int(nullable: false),
                        Physician_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Physicians", t => t.Physician_Id)
                .Index(t => t.Physician_Id);
            
            CreateTable(
                "dbo.Physicians",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        Address = c.String(),
                        Email = c.String(nullable: false),
                        PhoneNumber = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ZephyrAccelerometers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Time = c.DateTime(nullable: false),
                        Vertical = c.Int(nullable: false),
                        Lateral = c.Int(nullable: false),
                        Sagittal = c.Int(nullable: false),
                        PatentDataId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ZephyrBreathingWaveforms",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Time = c.DateTime(nullable: false),
                        Data = c.Int(nullable: false),
                        PatientDataId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ZephyrECGWaveforms",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Time = c.DateTime(nullable: false),
                        Data = c.Int(nullable: false),
                        PatientDataId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ZephyrEventDatas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Time = c.Int(nullable: false),
                        EventCode = c.Int(nullable: false),
                        Type = c.String(),
                        Source = c.String(),
                        EventId = c.Int(nullable: false),
                        EventSpecificData = c.String(),
                        PatientDataId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ZephyrSummaryDatas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        HeartRate = c.Int(nullable: false),
                        BreathingRate = c.Int(nullable: false),
                        SkinTemp = c.Single(nullable: false),
                        Posture = c.Int(nullable: false),
                        Activity = c.Single(nullable: false),
                        PeakAccel = c.Single(nullable: false),
                        BatteryVolts = c.Single(nullable: false),
                        BatteryLevel = c.Single(nullable: false),
                        BRAmplitude = c.Int(nullable: false),
                        BRNoise = c.Int(nullable: false),
                        BRConfidence = c.Int(nullable: false),
                        ECGAmplitude = c.Int(nullable: false),
                        ECGNoise = c.Int(nullable: false),
                        HRConfidence = c.Int(nullable: false),
                        HRV = c.Int(nullable: false),
                        SystemConfidence = c.Int(nullable: false),
                        GSR = c.Int(nullable: false),
                        ROGState = c.Int(nullable: false),
                        ROGTime = c.Int(nullable: false),
                        VerticalMin = c.Single(nullable: false),
                        VerticalPeak = c.Single(nullable: false),
                        LateralMin = c.Single(nullable: false),
                        LateralPeak = c.Single(nullable: false),
                        SagittalMin = c.Single(nullable: false),
                        SagittalPeak = c.Single(nullable: false),
                        DeviceTemp = c.Single(nullable: false),
                        StatusInfo = c.Int(nullable: false),
                        LinkQuality = c.Int(nullable: false),
                        RSSI = c.Int(nullable: false),
                        TxPower = c.Int(nullable: false),
                        CoreTemp = c.Single(nullable: false),
                        AuxADC1 = c.Int(nullable: false),
                        AuxADC2 = c.Int(nullable: false),
                        AuxADC3 = c.Int(nullable: false),
                        PatientDataId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PatientDatas", "Patient_Id", "dbo.Patients");
            DropForeignKey("dbo.Patients", "Physician_Id", "dbo.Physicians");
            DropForeignKey("dbo.PatientDatas", "MedicalDevice_Id", "dbo.MedicalDevices");
            DropForeignKey("dbo.Activities", "PatientData_Id", "dbo.PatientDatas");
            DropForeignKey("dbo.Experiments", "ExperimentAdministrator_Id", "dbo.ExperimentAdministrators");
            DropIndex("dbo.Patients", new[] { "Physician_Id" });
            DropIndex("dbo.PatientDatas", new[] { "Patient_Id" });
            DropIndex("dbo.PatientDatas", new[] { "MedicalDevice_Id" });
            DropIndex("dbo.Experiments", new[] { "ExperimentAdministrator_Id" });
            DropIndex("dbo.Activities", new[] { "PatientData_Id" });
            DropTable("dbo.ZephyrSummaryDatas");
            DropTable("dbo.ZephyrEventDatas");
            DropTable("dbo.ZephyrECGWaveforms");
            DropTable("dbo.ZephyrBreathingWaveforms");
            DropTable("dbo.ZephyrAccelerometers");
            DropTable("dbo.Physicians");
            DropTable("dbo.Patients");
            DropTable("dbo.PatientDatas");
            DropTable("dbo.MSBandUVs");
            DropTable("dbo.MSBandTemperatures");
            DropTable("dbo.MSBandPedometers");
            DropTable("dbo.MSBandHeartRates");
            DropTable("dbo.MSBandGryoscopes");
            DropTable("dbo.MSBandDistances");
            DropTable("dbo.MSBandCalories");
            DropTable("dbo.MSBandAccelerometers");
            DropTable("dbo.MedicalDevices");
            DropTable("dbo.Experiments");
            DropTable("dbo.ExperimentAdministrators");
            DropTable("dbo.BasisPeakSummaryDatas");
            DropTable("dbo.Activities");
        }
    }
}
