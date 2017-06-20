namespace Runniac.Membership.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNameLastName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserProfile", "Name", c => c.String());
            AddColumn("dbo.UserProfile", "Lastname", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserProfile", "Lastname");
            DropColumn("dbo.UserProfile", "Name");
        }
    }
}
