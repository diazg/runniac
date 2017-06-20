namespace Runniac.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFksToEntities : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Photos", "Event_Id", "dbo.Events");
            DropForeignKey("dbo.Towns", "Province_Id", "dbo.Provinces");
            DropForeignKey("dbo.Votes", "Comment_Id", "dbo.Comments");
            DropIndex("dbo.Photos", new[] { "Event_Id" });
            DropIndex("dbo.Towns", new[] { "Province_Id" });
            DropIndex("dbo.Votes", new[] { "Comment_Id" });
            RenameColumn(table: "dbo.Photos", name: "Event_Id", newName: "EventId");
            RenameColumn(table: "dbo.Towns", name: "Province_Id", newName: "ProvinceId");
            RenameColumn(table: "dbo.Votes", name: "Comment_Id", newName: "CommentId");
            AlterColumn("dbo.Photos", "EventId", c => c.Long(nullable: false));
            AlterColumn("dbo.Towns", "ProvinceId", c => c.Long(nullable: false));
            AlterColumn("dbo.Votes", "CommentId", c => c.Long(nullable: false));
            CreateIndex("dbo.Photos", "EventId");
            CreateIndex("dbo.Towns", "ProvinceId");
            CreateIndex("dbo.Votes", "CommentId");
            AddForeignKey("dbo.Photos", "EventId", "dbo.Events", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Towns", "ProvinceId", "dbo.Provinces", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Votes", "CommentId", "dbo.Comments", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Votes", "CommentId", "dbo.Comments");
            DropForeignKey("dbo.Towns", "ProvinceId", "dbo.Provinces");
            DropForeignKey("dbo.Photos", "EventId", "dbo.Events");
            DropIndex("dbo.Votes", new[] { "CommentId" });
            DropIndex("dbo.Towns", new[] { "ProvinceId" });
            DropIndex("dbo.Photos", new[] { "EventId" });
            AlterColumn("dbo.Votes", "CommentId", c => c.Long());
            AlterColumn("dbo.Towns", "ProvinceId", c => c.Long());
            AlterColumn("dbo.Photos", "EventId", c => c.Long());
            RenameColumn(table: "dbo.Votes", name: "CommentId", newName: "Comment_Id");
            RenameColumn(table: "dbo.Towns", name: "ProvinceId", newName: "Province_Id");
            RenameColumn(table: "dbo.Photos", name: "EventId", newName: "Event_Id");
            CreateIndex("dbo.Votes", "Comment_Id");
            CreateIndex("dbo.Towns", "Province_Id");
            CreateIndex("dbo.Photos", "Event_Id");
            AddForeignKey("dbo.Votes", "Comment_Id", "dbo.Comments", "Id");
            AddForeignKey("dbo.Towns", "Province_Id", "dbo.Provinces", "Id");
            AddForeignKey("dbo.Photos", "Event_Id", "dbo.Events", "Id");
        }
    }
}
