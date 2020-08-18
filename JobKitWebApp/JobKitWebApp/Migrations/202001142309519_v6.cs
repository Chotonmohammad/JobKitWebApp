namespace JobKitWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v6 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.ApplyJobs", new[] { "JobId" });
            DropIndex("dbo.ApplyJobs", new[] { "FreelancerId" });
            AddColumn("dbo.Jobs", "Budget", c => c.Double(nullable: false));
            AlterColumn("dbo.Users", "UserEmail", c => c.String(maxLength: 50));
            CreateIndex("dbo.ApplyJobs", new[] { "JobId", "FreelancerId" }, unique: true, name: "uk_jobId_freelancerId");
            CreateIndex("dbo.Users", "UserEmail", unique: true, name: "uk_freelancer_email");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Users", "uk_freelancer_email");
            DropIndex("dbo.ApplyJobs", "uk_jobId_freelancerId");
            AlterColumn("dbo.Users", "UserEmail", c => c.String());
            DropColumn("dbo.Jobs", "Budget");
            CreateIndex("dbo.ApplyJobs", "FreelancerId");
            CreateIndex("dbo.ApplyJobs", "JobId");
        }
    }
}
