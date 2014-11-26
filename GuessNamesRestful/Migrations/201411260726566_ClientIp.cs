namespace GuessNamesRestful.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ClientIp : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Guesses", "ClientIp", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Guesses", "ClientIp");
        }
    }
}
