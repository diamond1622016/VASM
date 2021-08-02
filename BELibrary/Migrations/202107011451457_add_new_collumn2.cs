namespace BELibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_new_collumn2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Scientist", "PaperList", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Scientist", "PaperList");
        }
    }
}
