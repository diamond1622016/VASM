namespace BELibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_new_typeOrg : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Organization", "Type", c => c.String(maxLength: 5));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Organization", "Type");
        }
    }
}
