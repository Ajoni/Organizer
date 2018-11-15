namespace Organizer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init4 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Groups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OwnerId = c.String(maxLength: 128),
                        Title = c.String(nullable: false),
                        Tags = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.OwnerId)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.OwnerId)
                .Index(t => t.ApplicationUser_Id);
            
            AddColumn("dbo.Events", "Visibility", c => c.Boolean());
            AddColumn("dbo.Events", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.Events", "Group_Id", c => c.Int());
            AddColumn("dbo.AspNetUsers", "Group_Id", c => c.Int());
            AddColumn("dbo.AspNetUsers", "ApplicationUser_Id", c => c.String(maxLength: 128));
            AlterColumn("dbo.Events", "Title", c => c.String(nullable: false));
            CreateIndex("dbo.Events", "Group_Id");
            CreateIndex("dbo.AspNetUsers", "Group_Id");
            CreateIndex("dbo.AspNetUsers", "ApplicationUser_Id");
            AddForeignKey("dbo.AspNetUsers", "Group_Id", "dbo.Groups", "Id");
            AddForeignKey("dbo.Events", "Group_Id", "dbo.Groups", "Id");
            AddForeignKey("dbo.AspNetUsers", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Groups", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Groups", "OwnerId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Events", "Group_Id", "dbo.Groups");
            DropForeignKey("dbo.AspNetUsers", "Group_Id", "dbo.Groups");
            DropIndex("dbo.Groups", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Groups", new[] { "OwnerId" });
            DropIndex("dbo.AspNetUsers", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "Group_Id" });
            DropIndex("dbo.Events", new[] { "Group_Id" });
            AlterColumn("dbo.Events", "Title", c => c.String());
            DropColumn("dbo.AspNetUsers", "ApplicationUser_Id");
            DropColumn("dbo.AspNetUsers", "Group_Id");
            DropColumn("dbo.Events", "Group_Id");
            DropColumn("dbo.Events", "Discriminator");
            DropColumn("dbo.Events", "Visibility");
            DropTable("dbo.Groups");
        }
    }
}
