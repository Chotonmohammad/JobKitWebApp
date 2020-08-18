namespace JobKitWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v101 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "EmailVerifiedFlag", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "EmailVerifiedFlag");
        }
    }
}
