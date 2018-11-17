namespace Organizer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixed_many_to_many : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Events", newName: "GroupEvents");
            DropForeignKey("dbo.AspNetUsers", "Group_Id", "dbo.Groups");
            DropForeignKey("dbo.AspNetUsers", "Group_Id1", "dbo.Groups");
            DropForeignKey("dbo.Groups", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Groups", "ApplicationUser_Id1", "dbo.AspNetUsers");
            DropIndex("dbo.GroupEvents", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "Group_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "Group_Id1" });
            DropIndex("dbo.Groups", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Groups", new[] { "ApplicationUser_Id1" });
            CreateTable(
                "dbo.UserEvents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Visibility = c.Boolean(nullable: false),
                        Title = c.String(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.UserGroupAdministration",
                c => new
                    {
                        UserRefId = c.String(nullable: false, maxLength: 128),
                        GroupRefId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserRefId, t.GroupRefId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserRefId, cascadeDelete: true)
                .ForeignKey("dbo.Groups", t => t.GroupRefId, cascadeDelete: true)
                .Index(t => t.UserRefId)
                .Index(t => t.GroupRefId);
            
            CreateTable(
                "dbo.UserGroupObservation",
                c => new
                    {
                        UserRefId = c.String(nullable: false, maxLength: 128),
                        GroupRefId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserRefId, t.GroupRefId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserRefId, cascadeDelete: true)
                .ForeignKey("dbo.Groups", t => t.GroupRefId, cascadeDelete: true)
                .Index(t => t.UserRefId)
                .Index(t => t.GroupRefId);
            
            DropColumn("dbo.GroupEvents", "Visibility");
            DropColumn("dbo.GroupEvents", "Discriminator");
            DropColumn("dbo.GroupEvents", "ApplicationUser_Id");
            DropColumn("dbo.AspNetUsers", "Group_Id");
            DropColumn("dbo.AspNetUsers", "Group_Id1");
            DropColumn("dbo.Groups", "ApplicationUser_Id");
            DropColumn("dbo.Groups", "ApplicationUser_Id1");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Groups", "ApplicationUser_Id1", c => c.String(maxLength: 128));
            AddColumn("dbo.Groups", "ApplicationUser_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.AspNetUsers", "Group_Id1", c => c.Int());
            AddColumn("dbo.AspNetUsers", "Group_Id", c => c.Int());
            AddColumn("dbo.GroupEvents", "ApplicationUser_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.GroupEvents", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.GroupEvents", "Visibility", c => c.Boolean());
            DropForeignKey("dbo.UserGroupObservation", "GroupRefId", "dbo.Groups");
            DropForeignKey("dbo.UserGroupObservation", "UserRefId", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserGroupAdministration", "GroupRefId", "dbo.Groups");
            DropForeignKey("dbo.UserGroupAdministration", "UserRefId", "dbo.AspNetUsers");
            DropIndex("dbo.UserGroupObservation", new[] { "GroupRefId" });
            DropIndex("dbo.UserGroupObservation", new[] { "UserRefId" });
            DropIndex("dbo.UserGroupAdministration", new[] { "GroupRefId" });
            DropIndex("dbo.UserGroupAdministration", new[] { "UserRefId" });
            DropIndex("dbo.UserEvents", new[] { "ApplicationUser_Id" });
            DropTable("dbo.UserGroupObservation");
            DropTable("dbo.UserGroupAdministration");
            DropTable("dbo.UserEvents");
            CreateIndex("dbo.Groups", "ApplicationUser_Id1");
            CreateIndex("dbo.Groups", "ApplicationUser_Id");
            CreateIndex("dbo.AspNetUsers", "Group_Id1");
            CreateIndex("dbo.AspNetUsers", "Group_Id");
            CreateIndex("dbo.GroupEvents", "ApplicationUser_Id");
            AddForeignKey("dbo.Groups", "ApplicationUser_Id1", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Groups", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.AspNetUsers", "Group_Id1", "dbo.Groups", "Id");
            AddForeignKey("dbo.AspNetUsers", "Group_Id", "dbo.Groups", "Id");
            RenameTable(name: "dbo.GroupEvents", newName: "Events");
        }
    }
}
