namespace JobKitWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v15 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ApplyJobConversations",
                c => new
                    {
                        ApplyJobConversationId = c.Int(nullable: false, identity: true),
                        Reply = c.String(nullable: false),
                        JobApplyId = c.Int(nullable: false),
                        ConversationTypeFlag = c.Int(nullable: false),
                        DateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ApplyJobConversationId)
                .ForeignKey("dbo.ApplyJobs", t => t.JobApplyId)
                .Index(t => t.JobApplyId);
            
            AddColumn("dbo.Jobs", "WorkProgess", c => c.Int(nullable: false));
            AlterColumn("dbo.FreelancerFeedbacks", "FreelancerReply", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ApplyJobConversations", "JobApplyId", "dbo.ApplyJobs");
            DropIndex("dbo.ApplyJobConversations", new[] { "JobApplyId" });
            AlterColumn("dbo.FreelancerFeedbacks", "FreelancerReply", c => c.String(nullable: false, maxLength: 50));
            DropColumn("dbo.Jobs", "WorkProgess");
            DropTable("dbo.ApplyJobConversations");
        }
    }
}
