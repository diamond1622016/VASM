namespace BELibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_new_Type : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Scientist", "Type", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Scientist", "Type");
        }
    }
}
