namespace workjop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddJobsTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ApplyForJobs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        message = c.String(),
                        applyData = c.DateTime(nullable: false),
                        jobId = c.Int(nullable: false),
                        userId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Jobs", t => t.jobId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.userId)
                .Index(t => t.jobId)
                .Index(t => t.userId);
            
            CreateTable(
                "dbo.Jobs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        JopTitle = c.String(),
                        JobContent = c.String(),
                        JobImage = c.String(),
                        CategoryID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategoryID, cascadeDelete: true)
                .Index(t => t.CategoryID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ApplyForJobs", "userId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ApplyForJobs", "jobId", "dbo.Jobs");
            DropForeignKey("dbo.Jobs", "CategoryID", "dbo.Categories");
            DropIndex("dbo.Jobs", new[] { "CategoryID" });
            DropIndex("dbo.ApplyForJobs", new[] { "userId" });
            DropIndex("dbo.ApplyForJobs", new[] { "jobId" });
            DropTable("dbo.Jobs");
            DropTable("dbo.ApplyForJobs");
        }
    }
}
