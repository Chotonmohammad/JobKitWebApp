namespace JobKitWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v9 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Users", "uk_freelancer_email");
            AlterColumn("dbo.Cities", "CityName", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.FreelancerFeedbacks", "FreelancerReply", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.JobTypes", "JobTypeName", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Users", "UserName", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Users", "UserPhone", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Users", "UserEmail", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Users", "UserPassword", c => c.String(nullable: false));
            AlterColumn("dbo.UserFeedbacks", "UserReply", c => c.String(nullable: false));
            CreateIndex("dbo.Users", "UserEmail", unique: true, name: "uk_freelancer_email");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Users", "uk_freelancer_email");
            AlterColumn("dbo.UserFeedbacks", "UserReply", c => c.String());
            AlterColumn("dbo.Users", "UserPassword", c => c.String());
            AlterColumn("dbo.Users", "UserEmail", c => c.String(maxLength: 50));
            AlterColumn("dbo.Users", "UserPhone", c => c.String());
            AlterColumn("dbo.Users", "UserName", c => c.String());
            AlterColumn("dbo.JobTypes", "JobTypeName", c => c.String());
            AlterColumn("dbo.FreelancerFeedbacks", "FreelancerReply", c => c.String());
            AlterColumn("dbo.Cities", "CityName", c => c.String());
            CreateIndex("dbo.Users", "UserEmail", unique: true, name: "uk_freelancer_email");
        }
    }
}
