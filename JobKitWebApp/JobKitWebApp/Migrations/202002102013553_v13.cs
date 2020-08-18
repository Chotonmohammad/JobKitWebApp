namespace JobKitWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v13 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ApplyJobs", "FreelancerId", "dbo.Freelancers");
            DropForeignKey("dbo.FreelancerFeedbacks", "JobApplyId", "dbo.ApplyJobs");
            DropForeignKey("dbo.ApplyJobs", "JobId", "dbo.Jobs");
            DropForeignKey("dbo.UserFeedbacks", "JobApplyId", "dbo.ApplyJobs");
            DropForeignKey("dbo.Connects", "FreelancerId", "dbo.Freelancers");
            DropForeignKey("dbo.Connects", "ConnectPriceId", "dbo.ConnectPricings");
            DropForeignKey("dbo.Jobs", "CityId", "dbo.Cities");
            DropForeignKey("dbo.Jobs", "FreelancerCategoryId", "dbo.FreelancerCategories");
            DropForeignKey("dbo.Jobs", "JobTypeId", "dbo.JobTypes");
            DropForeignKey("dbo.Jobs", "UserId", "dbo.Users");
            AddForeignKey("dbo.ApplyJobs", "FreelancerId", "dbo.Freelancers", "FreelancerId");
            AddForeignKey("dbo.FreelancerFeedbacks", "JobApplyId", "dbo.ApplyJobs", "ApplyJobId");
            AddForeignKey("dbo.ApplyJobs", "JobId", "dbo.Jobs", "JobId");
            AddForeignKey("dbo.UserFeedbacks", "JobApplyId", "dbo.ApplyJobs", "ApplyJobId");
            AddForeignKey("dbo.Connects", "FreelancerId", "dbo.Freelancers", "FreelancerId");
            AddForeignKey("dbo.Connects", "ConnectPriceId", "dbo.ConnectPricings", "ConnectPricingId");
            AddForeignKey("dbo.Jobs", "CityId", "dbo.Cities", "CityId");
            AddForeignKey("dbo.Jobs", "FreelancerCategoryId", "dbo.FreelancerCategories", "FreelancerCategoryId");
            AddForeignKey("dbo.Jobs", "JobTypeId", "dbo.JobTypes", "JobTypeId");
            AddForeignKey("dbo.Jobs", "UserId", "dbo.Users", "UserId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Jobs", "UserId", "dbo.Users");
            DropForeignKey("dbo.Jobs", "JobTypeId", "dbo.JobTypes");
            DropForeignKey("dbo.Jobs", "FreelancerCategoryId", "dbo.FreelancerCategories");
            DropForeignKey("dbo.Jobs", "CityId", "dbo.Cities");
            DropForeignKey("dbo.Connects", "ConnectPriceId", "dbo.ConnectPricings");
            DropForeignKey("dbo.Connects", "FreelancerId", "dbo.Freelancers");
            DropForeignKey("dbo.UserFeedbacks", "JobApplyId", "dbo.ApplyJobs");
            DropForeignKey("dbo.ApplyJobs", "JobId", "dbo.Jobs");
            DropForeignKey("dbo.FreelancerFeedbacks", "JobApplyId", "dbo.ApplyJobs");
            DropForeignKey("dbo.ApplyJobs", "FreelancerId", "dbo.Freelancers");
            AddForeignKey("dbo.Jobs", "UserId", "dbo.Users", "UserId", cascadeDelete: true);
            AddForeignKey("dbo.Jobs", "JobTypeId", "dbo.JobTypes", "JobTypeId", cascadeDelete: true);
            AddForeignKey("dbo.Jobs", "FreelancerCategoryId", "dbo.FreelancerCategories", "FreelancerCategoryId", cascadeDelete: true);
            AddForeignKey("dbo.Jobs", "CityId", "dbo.Cities", "CityId", cascadeDelete: true);
            AddForeignKey("dbo.Connects", "ConnectPriceId", "dbo.ConnectPricings", "ConnectPricingId", cascadeDelete: true);
            AddForeignKey("dbo.Connects", "FreelancerId", "dbo.Freelancers", "FreelancerId", cascadeDelete: true);
            AddForeignKey("dbo.UserFeedbacks", "JobApplyId", "dbo.ApplyJobs", "ApplyJobId", cascadeDelete: true);
            AddForeignKey("dbo.ApplyJobs", "JobId", "dbo.Jobs", "JobId", cascadeDelete: true);
            AddForeignKey("dbo.FreelancerFeedbacks", "JobApplyId", "dbo.ApplyJobs", "ApplyJobId", cascadeDelete: true);
            AddForeignKey("dbo.ApplyJobs", "FreelancerId", "dbo.Freelancers", "FreelancerId", cascadeDelete: true);
        }
    }
}
