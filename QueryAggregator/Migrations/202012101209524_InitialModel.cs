namespace QueryAggregator.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Queries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        QueryString = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Links",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Url = c.String(nullable: false),
                        Title = c.String(nullable: false),
                        Description = c.String(),
                        Query_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Queries", t => t.Query_Id)
                .Index(t => t.Query_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Links", "Query_Id", "dbo.Queries");
            DropIndex("dbo.Links", new[] { "Query_Id" });
            DropTable("dbo.Links");
            DropTable("dbo.Queries");
        }
    }
}
