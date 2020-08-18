namespace JobKitWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v11 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Freelancers", "VerifiedFlag", c => c.Int(nullable: false));
            AddColumn("dbo.Users", "UserPhone", c => c.String());
            DropColumn("dbo.Freelancers", "EmailVerifiedFlag");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Freelancers", "EmailVerifiedFlag", c => c.Int(nullable: false));
            DropColumn("dbo.Users", "UserPhone");
            DropColumn("dbo.Freelancers", "VerifiedFlag");
        }
    }
}
