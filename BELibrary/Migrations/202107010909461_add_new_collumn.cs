namespace BELibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_new_collumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Scientist", "Academic_pos", c => c.String(maxLength: 250));
            AddColumn("dbo.Scientist", "Qualification", c => c.String(maxLength: 250));
            AddColumn("dbo.Scientist", "AFF_Desc", c => c.String());
            AddColumn("dbo.Scientist", "Paper_num", c => c.String(maxLength: 250));
            AddColumn("dbo.Scientist", "Personal_web_link", c => c.String(maxLength: 250));
            DropColumn("dbo.Scientist", "Academic_position");
            DropColumn("dbo.Scientist", "Academic_rank");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Scientist", "Academic_rank", c => c.String(maxLength: 250));
            AddColumn("dbo.Scientist", "Academic_position", c => c.String(maxLength: 250));
            DropColumn("dbo.Scientist", "Personal_web_link");
            DropColumn("dbo.Scientist", "Paper_num");
            DropColumn("dbo.Scientist", "AFF_Desc");
            DropColumn("dbo.Scientist", "Qualification");
            DropColumn("dbo.Scientist", "Academic_pos");
        }
    }
}
