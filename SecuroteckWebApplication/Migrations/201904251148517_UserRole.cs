namespace SecuroteckWebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserRole : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Logs",
                c => new
                    {
                        LogID = c.String(nullable: false, maxLength: 128),
                        LogString = c.String(),
                        LogDateTime = c.DateTime(nullable: false),
                        User_ApiKey = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.LogID)
                .ForeignKey("dbo.Users", t => t.User_ApiKey)
                .Index(t => t.User_ApiKey);
            
            AlterColumn("dbo.Users", "UserRole", c => c.String());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Logs", "User_ApiKey", "dbo.Users");
            DropIndex("dbo.Logs", new[] { "User_ApiKey" });
            AlterColumn("dbo.Users", "UserRole", c => c.Int(nullable: false));
            DropTable("dbo.Logs");
        }
    }
}
