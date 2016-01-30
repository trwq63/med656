namespace UAHFitVault.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1_30_16 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AccountRequests", "ReasonForRequest", c => c.String(nullable: false));
            AddColumn("dbo.AccountRequests", "PhysicianID", c => c.Int(nullable: false));
            AddColumn("dbo.AccountRequests", "ExperimentAdministratorID", c => c.Int(nullable: false));
            DropColumn("dbo.AccountRequests", "Request");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AccountRequests", "Request", c => c.String(nullable: false));
            DropColumn("dbo.AccountRequests", "ExperimentAdministratorID");
            DropColumn("dbo.AccountRequests", "PhysicianID");
            DropColumn("dbo.AccountRequests", "ReasonForRequest");
        }
    }
}
