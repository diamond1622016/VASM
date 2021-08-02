namespace BELibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_New_Table_Organization_ok : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Organization",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        Founded_year = c.String(),
                        Areas_of_AI = c.String(),
                        Business_type = c.String(),
                        Ownership_type = c.String(),
                        Scale = c.String(),
                        Number_of_Staff = c.String(),
                        Number_of_AI_Staff = c.String(),
                        Headquarter_Address = c.String(),
                        Website = c.String(),
                        Logo = c.String(),
                        Telephone = c.String(),
                        Email = c.String(),
                        Country = c.String(),
                        Organization_summary = c.String(maxLength: 2500),
                        Selected_AI_products = c.String(),
                        Customers_and_partners = c.String(),
                        Development_plan = c.String(maxLength: 2500),
                        Interested_topics = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.Id);
            
            AlterColumn("dbo.Scientist", "Bio_sketch", c => c.String(maxLength: 2500));
            AlterColumn("dbo.Scientist", "Working_plan", c => c.String(maxLength: 2500));
            AlterColumn("dbo.Scientist", "Interested_topics", c => c.String(maxLength: 500));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Scientist", "Interested_topics", c => c.String());
            AlterColumn("dbo.Scientist", "Working_plan", c => c.String());
            AlterColumn("dbo.Scientist", "Bio_sketch", c => c.String());
            DropTable("dbo.Organization");
        }
    }
}
