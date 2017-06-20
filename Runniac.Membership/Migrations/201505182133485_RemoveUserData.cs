namespace Runniac.Membership.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveUserData : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.UserProfile", "Name");
            DropColumn("dbo.UserProfile", "Lastname");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserProfile", "Lastname", c => c.String());
            AddColumn("dbo.UserProfile", "Name", c => c.String());
        }
    }
}
