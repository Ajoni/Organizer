namespace Organizer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_many_to_many : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Groups", "ApplicationUser_Id", "dbo.AspNetUsers");
            AddColumn("dbo.AspNetUsers", "Group_Id1", c => c.Int());
            AddColumn("dbo.Groups", "ApplicationUser_Id1", c => c.String(maxLength: 128));
            CreateIndex("dbo.AspNetUsers", "Group_Id1");
            CreateIndex("dbo.Groups", "ApplicationUser_Id1");
            AddForeignKey("dbo.AspNetUsers", "Group_Id1", "dbo.Groups", "Id");
            AddForeignKey("dbo.Groups", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Groups", "ApplicationUser_Id1", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Groups", "ApplicationUser_Id1", "dbo.AspNetUsers");
            DropForeignKey("dbo.Groups", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "Group_Id1", "dbo.Groups");
            DropIndex("dbo.Groups", new[] { "ApplicationUser_Id1" });
            DropIndex("dbo.AspNetUsers", new[] { "Group_Id1" });
            DropColumn("dbo.Groups", "ApplicationUser_Id1");
            DropColumn("dbo.AspNetUsers", "Group_Id1");
            AddForeignKey("dbo.Groups", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
