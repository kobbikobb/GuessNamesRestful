namespace GuessNamesRestful.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Guesses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        UserName = c.String(),
                        GuessedName = c.String(),
                        Score = c.Int(nullable: false),
                        Name_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Names", t => t.Name_Id)
                .Index(t => t.Name_Id);
            
            CreateTable(
                "dbo.Names",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstNameHash = c.String(),
                        FullNameHash = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Guesses", "Name_Id", "dbo.Names");
            DropIndex("dbo.Guesses", new[] { "Name_Id" });
            DropTable("dbo.Names");
            DropTable("dbo.Guesses");
        }
    }
}
