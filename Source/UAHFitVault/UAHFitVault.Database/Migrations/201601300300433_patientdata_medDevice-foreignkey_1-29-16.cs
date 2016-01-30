namespace UAHFitVault.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class patientdata_medDeviceforeignkey_12916 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.PatientDatas", name: "MedicalDevice_Id", newName: "MedicalDeviceId");
            RenameIndex(table: "dbo.PatientDatas", name: "IX_MedicalDevice_Id", newName: "IX_MedicalDeviceId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.PatientDatas", name: "IX_MedicalDeviceId", newName: "IX_MedicalDevice_Id");
            RenameColumn(table: "dbo.PatientDatas", name: "MedicalDeviceId", newName: "MedicalDevice_Id");
        }
    }
}
