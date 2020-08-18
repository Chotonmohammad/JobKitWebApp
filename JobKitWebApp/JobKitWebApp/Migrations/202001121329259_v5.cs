namespace JobKitWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v5 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Freelancers", "Email", c => c.String(maxLength: 100));
            CreateIndex("dbo.Freelancers", "Email", unique: true, name: "uk_freelancer_email");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Freelancers", "uk_freelancer_email");
            AlterColumn("dbo.Freelancers", "Email", c => c.String());
        }
    }
}
