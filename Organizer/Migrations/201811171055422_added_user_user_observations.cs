namespace Organizer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_user_user_observations : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.AspNetUsers", new[] { "ApplicationUser_Id" });
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
            
            DropColumn("dbo.AspNetUsers", "ApplicationUser_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "ApplicationUser_Id", c => c.String(maxLength: 128));
            DropForeignKey("dbo.UserToUserObservation", "ObservedUserRefId", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserToUserObservation", "ObservingUserRefId", "dbo.AspNetUsers");
            DropIndex("dbo.UserToUserObservation", new[] { "ObservedUserRefId" });
            DropIndex("dbo.UserToUserObservation", new[] { "ObservingUserRefId" });
            DropTable("dbo.UserToUserObservation");
            CreateIndex("dbo.AspNetUsers", "ApplicationUser_Id");
            AddForeignKey("dbo.AspNetUsers", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
