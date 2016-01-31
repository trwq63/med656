namespace UAHFitVault.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1_30_162 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AccountRequests", "ReasonForAccount", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AccountRequests", "ReasonForAccount");
        }
    }
}
