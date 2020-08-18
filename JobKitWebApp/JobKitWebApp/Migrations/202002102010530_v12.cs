namespace JobKitWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v12 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Users", "uk_freelancer_email");
            AddColumn("dbo.Users", "Name", c => c.String(nullable: false, maxLength: 50));
            AddColumn("dbo.Users", "Phone", c => c.String(nullable: false, maxLength: 50));
            AddColumn("dbo.Users", "Email", c => c.String(nullable: false, maxLength: 50));
            AddColumn("dbo.Users", "Password", c => c.String(nullable: false));
            CreateIndex("dbo.Users", "Email", unique: true, name: "uk_user_email");
            DropColumn("dbo.Users", "UserName");
            DropColumn("dbo.Users", "UserPhone");
            DropColumn("dbo.Users", "UserEmail");
            DropColumn("dbo.Users", "UserPassword");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "UserPassword", c => c.String(nullable: false));
            AddColumn("dbo.Users", "UserEmail", c => c.String(nullable: false, maxLength: 50));
            AddColumn("dbo.Users", "UserPhone", c => c.String(nullable: false, maxLength: 50));
            AddColumn("dbo.Users", "UserName", c => c.String(nullable: false, maxLength: 50));
            DropIndex("dbo.Users", "uk_user_email");
            DropColumn("dbo.Users", "Password");
            DropColumn("dbo.Users", "Email");
            DropColumn("dbo.Users", "Phone");
            DropColumn("dbo.Users", "Name");
            CreateIndex("dbo.Users", "UserEmail", unique: true, name: "uk_freelancer_email");
        }
    }
}
