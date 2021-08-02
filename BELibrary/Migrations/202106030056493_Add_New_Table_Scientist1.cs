namespace BELibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_New_Table_Scientist1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Scientist", "Nationality", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Scientist", "Nationality");
        }
    }
}
