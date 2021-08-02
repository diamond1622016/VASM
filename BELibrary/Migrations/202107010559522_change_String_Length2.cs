namespace BELibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change_String_Length2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Scientist", "Scholar_id", c => c.String(maxLength: 250));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Scientist", "Scholar_id", c => c.String());
        }
    }
}
