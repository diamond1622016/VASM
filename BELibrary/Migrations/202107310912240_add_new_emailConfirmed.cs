namespace BELibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_new_emailConfirmed : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Account", "EmailConfirmed", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Account", "EmailConfirmed");
        }
    }
}
