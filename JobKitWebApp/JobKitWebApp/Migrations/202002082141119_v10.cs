namespace JobKitWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v10 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Freelancers", "EmailVerified", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Freelancers", "EmailVerified");
        }
    }
}
