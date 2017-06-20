namespace Runniac.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserToContext : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        UserName = c.String(),
                        Name = c.String(),
                        Lastname = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            Sql("INSERT INTO dbo.Users(Id, UserName, Name) VALUES (1, 'admin@runniac.com', 'Administrador')");
            CreateIndex("dbo.Comments", "UserId");
            CreateIndex("dbo.Photos", "UserId");
            AddForeignKey("dbo.Photos", "UserId", "dbo.Users", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Comments", "UserId", "dbo.Users", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "UserId", "dbo.Users");
            DropForeignKey("dbo.Photos", "UserId", "dbo.Users");
            DropIndex("dbo.Photos", new[] { "UserId" });
            DropIndex("dbo.Comments", new[] { "UserId" });
            DropTable("dbo.Users");
        }
    }
}
