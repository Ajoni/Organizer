namespace Organizer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GroupEvents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        Group_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Groups", t => t.Group_Id)
                .Index(t => t.Group_Id);
            
            CreateTable(
                "dbo.Groups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OwnerId = c.String(maxLength: 128),
                        Title = c.String(nullable: false),
                        Tags = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.OwnerId)
                .Index(t => t.OwnerId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        Surname = c.String(),
                        Description = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
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
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
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
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
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
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
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
            
            CreateTable(
                "dbo.UserToUserObservation",
                c => new
                    {
                        ObservingUserRefId = c.String(nullable: false, maxLength: 128),
                        ObservedUserRefId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.ObservingUserRefId, t.ObservedUserRefId })
                .ForeignKey("dbo.AspNetUsers", t => t.ObservingUserRefId)
                .ForeignKey("dbo.AspNetUsers", t => t.ObservedUserRefId)
                .Index(t => t.ObservingUserRefId)
                .Index(t => t.ObservedUserRefId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Groups", "OwnerId", "dbo.AspNetUsers");
            DropForeignKey("dbo.GroupEvents", "Group_Id", "dbo.Groups");
            DropForeignKey("dbo.UserToUserObservation", "ObservedUserRefId", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserToUserObservation", "ObservingUserRefId", "dbo.AspNetUsers");
            DropForeignKey("dbo.TODOItems", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Notes", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserGroupObservation", "GroupRefId", "dbo.Groups");
            DropForeignKey("dbo.UserGroupObservation", "UserRefId", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserEvents", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserGroupAdministration", "GroupRefId", "dbo.Groups");
            DropForeignKey("dbo.UserGroupAdministration", "UserRefId", "dbo.AspNetUsers");
            DropIndex("dbo.UserToUserObservation", new[] { "ObservedUserRefId" });
            DropIndex("dbo.UserToUserObservation", new[] { "ObservingUserRefId" });
            DropIndex("dbo.UserGroupObservation", new[] { "GroupRefId" });
            DropIndex("dbo.UserGroupObservation", new[] { "UserRefId" });
            DropIndex("dbo.UserGroupAdministration", new[] { "GroupRefId" });
            DropIndex("dbo.UserGroupAdministration", new[] { "UserRefId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.TODOItems", new[] { "UserId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.Notes", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.UserEvents", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Groups", new[] { "OwnerId" });
            DropIndex("dbo.GroupEvents", new[] { "Group_Id" });
            DropTable("dbo.UserToUserObservation");
            DropTable("dbo.UserGroupObservation");
            DropTable("dbo.UserGroupAdministration");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.TODOItems");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.Notes");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.UserEvents");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Groups");
            DropTable("dbo.GroupEvents");
        }
    }
}
