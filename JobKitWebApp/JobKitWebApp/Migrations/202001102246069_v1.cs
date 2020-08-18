namespace JobKitWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ApplyJobs",
                c => new
                    {
                        ApplyJobId = c.Int(nullable: false, identity: true),
                        JobId = c.Int(nullable: false),
                        FreelancerId = c.Int(nullable: false),
                        CoverMessage = c.String(),
                        DateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ApplyJobId)
                .ForeignKey("dbo.Freelancers", t => t.FreelancerId, cascadeDelete: true)
                .ForeignKey("dbo.Jobs", t => t.JobId, cascadeDelete: true)
                .Index(t => t.JobId)
                .Index(t => t.FreelancerId);
            
            CreateTable(
                "dbo.Freelancers",
                c => new
                    {
                        FreelancerId = c.Int(nullable: false, identity: true),
                        FreelancerName = c.String(),
                        FreelancerTitle = c.String(),
                        FreelancerIntroduction = c.String(),
                        FreelancerCategoryId = c.Int(),
                        CityId = c.Int(),
                        TotalConnect = c.Int(nullable: false),
                        Address = c.String(),
                        Phone = c.String(),
                        Email = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.FreelancerId)
                .ForeignKey("dbo.Cities", t => t.CityId)
                .ForeignKey("dbo.FreelancerCategories", t => t.FreelancerCategoryId)
                .Index(t => t.FreelancerCategoryId)
                .Index(t => t.CityId);
            
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        CityId = c.Int(nullable: false, identity: true),
                        CityName = c.String(),
                    })
                .PrimaryKey(t => t.CityId);
            
            CreateTable(
                "dbo.Connects",
                c => new
                    {
                        ConnectId = c.Int(nullable: false, identity: true),
                        ConnectPriceId = c.Int(nullable: false),
                        FreelancerId = c.Int(nullable: false),
                        Refernce = c.String(),
                        PaymentStatus = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ConnectId)
                .ForeignKey("dbo.ConnectPricings", t => t.ConnectPriceId, cascadeDelete: true)
                .ForeignKey("dbo.Freelancers", t => t.FreelancerId, cascadeDelete: true)
                .Index(t => t.ConnectPriceId)
                .Index(t => t.FreelancerId);
            
            CreateTable(
                "dbo.ConnectPricings",
                c => new
                    {
                        ConnectPricingId = c.Int(nullable: false, identity: true),
                        NumberOfConnect = c.Int(nullable: false),
                        Price = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.ConnectPricingId);
            
            CreateTable(
                "dbo.FreelancerCategories",
                c => new
                    {
                        FreelancerCategoryId = c.Int(nullable: false, identity: true),
                        FreelancerCategoryName = c.String(),
                    })
                .PrimaryKey(t => t.FreelancerCategoryId);
            
            CreateTable(
                "dbo.FreelancerFeedbacks",
                c => new
                    {
                        FreelancerFeedbackId = c.Int(nullable: false, identity: true),
                        FreelancerReply = c.String(),
                        JobApplyId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.FreelancerFeedbackId)
                .ForeignKey("dbo.ApplyJobs", t => t.JobApplyId, cascadeDelete: true)
                .Index(t => t.JobApplyId);
            
            CreateTable(
                "dbo.Jobs",
                c => new
                    {
                        JobId = c.Int(nullable: false, identity: true),
                        JobTitle = c.String(),
                        JobDescription = c.String(),
                        JobTypeId = c.Int(nullable: false),
                        FreelancerCategoryId = c.Int(nullable: false),
                        CityId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.JobId)
                .ForeignKey("dbo.Cities", t => t.CityId, cascadeDelete: true)
                .ForeignKey("dbo.FreelancerCategories", t => t.FreelancerCategoryId, cascadeDelete: true)
                .ForeignKey("dbo.JobTypes", t => t.JobTypeId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.JobTypeId)
                .Index(t => t.FreelancerCategoryId)
                .Index(t => t.CityId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.JobTypes",
                c => new
                    {
                        JobTypeId = c.Int(nullable: false, identity: true),
                        JobTypeName = c.String(),
                    })
                .PrimaryKey(t => t.JobTypeId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        UserEmail = c.String(),
                        UserPassword = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.UserFeedbacks",
                c => new
                    {
                        UserFeedbackId = c.Int(nullable: false, identity: true),
                        UserReply = c.String(),
                        JobApplyId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserFeedbackId)
                .ForeignKey("dbo.ApplyJobs", t => t.JobApplyId, cascadeDelete: true)
                .Index(t => t.JobApplyId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserFeedbacks", "JobApplyId", "dbo.ApplyJobs");
            DropForeignKey("dbo.ApplyJobs", "JobId", "dbo.Jobs");
            DropForeignKey("dbo.Jobs", "UserId", "dbo.Users");
            DropForeignKey("dbo.Jobs", "JobTypeId", "dbo.JobTypes");
            DropForeignKey("dbo.Jobs", "FreelancerCategoryId", "dbo.FreelancerCategories");
            DropForeignKey("dbo.Jobs", "CityId", "dbo.Cities");
            DropForeignKey("dbo.FreelancerFeedbacks", "JobApplyId", "dbo.ApplyJobs");
            DropForeignKey("dbo.Freelancers", "FreelancerCategoryId", "dbo.FreelancerCategories");
            DropForeignKey("dbo.Connects", "FreelancerId", "dbo.Freelancers");
            DropForeignKey("dbo.Connects", "ConnectPriceId", "dbo.ConnectPricings");
            DropForeignKey("dbo.Freelancers", "CityId", "dbo.Cities");
            DropForeignKey("dbo.ApplyJobs", "FreelancerId", "dbo.Freelancers");
            DropIndex("dbo.UserFeedbacks", new[] { "JobApplyId" });
            DropIndex("dbo.Jobs", new[] { "UserId" });
            DropIndex("dbo.Jobs", new[] { "CityId" });
            DropIndex("dbo.Jobs", new[] { "FreelancerCategoryId" });
            DropIndex("dbo.Jobs", new[] { "JobTypeId" });
            DropIndex("dbo.FreelancerFeedbacks", new[] { "JobApplyId" });
            DropIndex("dbo.Connects", new[] { "FreelancerId" });
            DropIndex("dbo.Connects", new[] { "ConnectPriceId" });
            DropIndex("dbo.Freelancers", new[] { "CityId" });
            DropIndex("dbo.Freelancers", new[] { "FreelancerCategoryId" });
            DropIndex("dbo.ApplyJobs", new[] { "FreelancerId" });
            DropIndex("dbo.ApplyJobs", new[] { "JobId" });
            DropTable("dbo.UserFeedbacks");
            DropTable("dbo.Users");
            DropTable("dbo.JobTypes");
            DropTable("dbo.Jobs");
            DropTable("dbo.FreelancerFeedbacks");
            DropTable("dbo.FreelancerCategories");
            DropTable("dbo.ConnectPricings");
            DropTable("dbo.Connects");
            DropTable("dbo.Cities");
            DropTable("dbo.Freelancers");
            DropTable("dbo.ApplyJobs");
        }
    }
}
