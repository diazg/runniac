namespace Runniac.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EventId : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Comments", "Event_Id", "dbo.Events");
            DropIndex("dbo.Comments", new[] { "Event_Id" });
            RenameColumn(table: "dbo.Comments", name: "Event_Id", newName: "EventId");
            AlterColumn("dbo.Comments", "EventId", c => c.Long(nullable: false));
            CreateIndex("dbo.Comments", "EventId");
            AddForeignKey("dbo.Comments", "EventId", "dbo.Events", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "EventId", "dbo.Events");
            DropIndex("dbo.Comments", new[] { "EventId" });
            AlterColumn("dbo.Comments", "EventId", c => c.Long());
            RenameColumn(table: "dbo.Comments", name: "EventId", newName: "Event_Id");
            CreateIndex("dbo.Comments", "Event_Id");
            AddForeignKey("dbo.Comments", "Event_Id", "dbo.Events", "Id");
        }
    }
}
