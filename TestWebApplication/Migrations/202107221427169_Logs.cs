namespace TestWebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Logs : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AppLogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateLog = c.DateTime(nullable: false),
                        Message = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.AppLogs");
        }
    }
}
