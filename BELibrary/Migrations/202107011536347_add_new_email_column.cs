namespace BELibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_new_email_column : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Account", "Email", c => c.String(maxLength: 256));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Account", "Email");
        }
    }
}
