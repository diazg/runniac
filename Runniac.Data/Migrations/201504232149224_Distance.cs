namespace Runniac.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Distance : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Events", "DistanceFromUser");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Events", "DistanceFromUser", c => c.Single(nullable: false));
        }
    }
}
