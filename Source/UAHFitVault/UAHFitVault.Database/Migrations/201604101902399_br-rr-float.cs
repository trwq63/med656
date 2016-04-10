namespace UAHFitVault.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class brrrfloat : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ZephyrBRRRs", "BR", c => c.Single());
            AlterColumn("dbo.ZephyrBRRRs", "RR", c => c.Single());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ZephyrBRRRs", "RR", c => c.Int());
            AlterColumn("dbo.ZephyrBRRRs", "BR", c => c.Int());
        }
    }
}
