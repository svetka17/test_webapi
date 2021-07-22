namespace TestWebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DateIntervals",
                c => new
                    {
                        DateIntervalId = c.Int(nullable: false, identity: true),
                        DateFrom = c.DateTime(nullable: false),
                        DateTo = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.DateIntervalId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.DateIntervals");
        }
    }
}
