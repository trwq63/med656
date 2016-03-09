namespace UAHFitVault.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class activityaddstartstoptimes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Activities", "StartTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.Activities", "EndTime", c => c.DateTime(nullable: false));
            DropColumn("dbo.Activities", "Timestamp");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Activities", "Timestamp", c => c.DateTime(nullable: false));
            DropColumn("dbo.Activities", "EndTime");
            DropColumn("dbo.Activities", "StartTime");
        }
    }
}
