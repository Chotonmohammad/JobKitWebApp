namespace JobKitWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v8 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Jobs", "JobTitle", c => c.String(nullable: false));
            AlterColumn("dbo.Jobs", "JobDescription", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Jobs", "JobDescription", c => c.String());
            AlterColumn("dbo.Jobs", "JobTitle", c => c.String());
        }
    }
}
