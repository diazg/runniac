namespace Runniac.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddEventDetailsLink : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Rating = c.Int(nullable: false),
                        Title = c.String(),
                        Text = c.String(),
                        CommentDate = c.DateTime(),
                        UserId = c.Int(nullable: false),
                        Event_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Events", t => t.Event_Id)
                .Index(t => t.Event_Id);
            
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        EventDate = c.DateTime(),
                        Location = c.String(),
                        DistanceKms = c.Single(nullable: false),
                        Type = c.String(),
                        ResultsUrl = c.String(),
                        Url = c.String(),
                        Fee = c.Single(nullable: false),
                        ImageUrl = c.String(),
                        Published = c.Boolean(nullable: false),
                        DetailsLink = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Photos",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Url = c.String(),
                        PhotoDate = c.DateTime(nullable: false),
                        UserId = c.Int(nullable: false),
                        Event_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Events", t => t.Event_Id)
                .Index(t => t.Event_Id);
            
            CreateTable(
                "dbo.Votes",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Positive = c.Boolean(nullable: false),
                        UserId = c.Int(nullable: false),
                        Comment_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Comments", t => t.Comment_Id)
                .Index(t => t.Comment_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Votes", "Comment_Id", "dbo.Comments");
            DropForeignKey("dbo.Photos", "Event_Id", "dbo.Events");
            DropForeignKey("dbo.Comments", "Event_Id", "dbo.Events");
            DropIndex("dbo.Votes", new[] { "Comment_Id" });
            DropIndex("dbo.Photos", new[] { "Event_Id" });
            DropIndex("dbo.Comments", new[] { "Event_Id" });
            DropTable("dbo.Votes");
            DropTable("dbo.Photos");
            DropTable("dbo.Events");
            DropTable("dbo.Comments");
        }
    }
}
