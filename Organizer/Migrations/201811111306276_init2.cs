namespace Organizer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Notes",
                c => new
                    {
                        Nr = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        Content = c.String(),
                        Visibility = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Nr)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.TODOItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        Title = c.String(),
                        Description = c.String(),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TODOItems", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Notes", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.TODOItems", new[] { "UserId" });
            DropIndex("dbo.Notes", new[] { "UserId" });
            DropTable("dbo.TODOItems");
            DropTable("dbo.Notes");
        }
    }
}
