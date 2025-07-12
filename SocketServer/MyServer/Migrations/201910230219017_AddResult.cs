namespace MyServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddResult : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Result",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        TotalCount = c.Int(nullable: false),
                        WinCount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Result");
        }
    }
}
