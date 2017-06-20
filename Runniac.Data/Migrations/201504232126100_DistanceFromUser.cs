namespace Runniac.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DistanceFromUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Events", "DistanceFromUser", c => c.Single(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Events", "DistanceFromUser");
        }
    }
}
