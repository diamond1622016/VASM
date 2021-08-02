namespace BELibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_New_Table_Scientist : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Scientist", "Hindex5y", c => c.String());
            AddColumn("dbo.Scientist", "I10index5y", c => c.String());
            AddColumn("dbo.Scientist", "Citedby5y", c => c.String());
            AddColumn("dbo.Scientist", "Email_domain", c => c.String());
            AlterColumn("dbo.Scientist", "Scholar_id", c => c.String());
            AlterColumn("dbo.Scientist", "Citedby", c => c.String());
            AlterColumn("dbo.Scientist", "I10Index", c => c.String());
            AlterColumn("dbo.Scientist", "HIndex", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Scientist", "HIndex", c => c.Int(nullable: false));
            AlterColumn("dbo.Scientist", "I10Index", c => c.Int(nullable: false));
            AlterColumn("dbo.Scientist", "Citedby", c => c.Int(nullable: false));
            AlterColumn("dbo.Scientist", "Scholar_id", c => c.Int(nullable: false));
            DropColumn("dbo.Scientist", "Email_domain");
            DropColumn("dbo.Scientist", "Citedby5y");
            DropColumn("dbo.Scientist", "I10index5y");
            DropColumn("dbo.Scientist", "Hindex5y");
        }
    }
}
