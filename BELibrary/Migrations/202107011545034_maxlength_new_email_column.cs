namespace BELibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class maxlength_new_email_column : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Account", "Email", c => c.String(maxLength: 256));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Account", "Email", c => c.String(maxLength: 15));
        }
    }
}
