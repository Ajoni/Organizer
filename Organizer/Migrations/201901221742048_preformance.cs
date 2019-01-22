namespace Organizer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class preformance : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "TodosDoneInTime", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "TodosTotal", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "TodosTotal");
            DropColumn("dbo.AspNetUsers", "TodosDoneInTime");
        }
    }
}
