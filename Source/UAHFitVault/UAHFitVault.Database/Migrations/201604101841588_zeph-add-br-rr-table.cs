namespace UAHFitVault.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class zephaddbrrrtable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ZephyrBRRRs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TimeStamp = c.DateTime(nullable: false),
                        BR = c.Int(),
                        RR = c.Int(),
                        PatientDataId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PatientDatas", t => t.PatientDataId, cascadeDelete: true)
                .Index(t => t.PatientDataId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ZephyrBRRRs", "PatientDataId", "dbo.PatientDatas");
            DropIndex("dbo.ZephyrBRRRs", new[] { "PatientDataId" });
            DropTable("dbo.ZephyrBRRRs");
        }
    }
}
