namespace Runniac.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CourseFileName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Events", "CourseFileName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Events", "CourseFileName");
        }
    }
}
