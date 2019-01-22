namespace Organizer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_files_model : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserFiles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Bytes = c.Binary(),
                        Name = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserFiles", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.UserFiles", new[] { "ApplicationUser_Id" });
            DropTable("dbo.UserFiles");
        }
    }
}
