namespace BELibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Column_To_Scientist_Table : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Scientist", "Academic_position", c => c.String());
            AddColumn("dbo.Scientist", "Academic_rank", c => c.String());
            AddColumn("dbo.Scientist", "Position", c => c.String());
            AddColumn("dbo.Scientist", "Bio_sketch", c => c.String());
            AddColumn("dbo.Scientist", "Working_plan", c => c.String());
            AddColumn("dbo.Scientist", "Interested_topics", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Scientist", "Interested_topics");
            DropColumn("dbo.Scientist", "Working_plan");
            DropColumn("dbo.Scientist", "Bio_sketch");
            DropColumn("dbo.Scientist", "Position");
            DropColumn("dbo.Scientist", "Academic_rank");
            DropColumn("dbo.Scientist", "Academic_position");
        }
    }
}
