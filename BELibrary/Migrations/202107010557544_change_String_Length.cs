namespace BELibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change_String_Length : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Organization", "Name", c => c.String(maxLength: 250));
            AlterColumn("dbo.Organization", "Founded_year", c => c.String(maxLength: 250));
            AlterColumn("dbo.Organization", "Business_type", c => c.String(maxLength: 250));
            AlterColumn("dbo.Organization", "Ownership_type", c => c.String(maxLength: 250));
            AlterColumn("dbo.Organization", "Scale", c => c.String(maxLength: 250));
            AlterColumn("dbo.Organization", "Number_of_Staff", c => c.String(maxLength: 250));
            AlterColumn("dbo.Organization", "Number_of_AI_Staff", c => c.String(maxLength: 250));
            AlterColumn("dbo.Organization", "Website", c => c.String(maxLength: 250));
            AlterColumn("dbo.Organization", "Logo", c => c.String(maxLength: 250));
            AlterColumn("dbo.Organization", "Telephone", c => c.String(maxLength: 250));
            AlterColumn("dbo.Organization", "Email", c => c.String(maxLength: 250));
            AlterColumn("dbo.Organization", "Country", c => c.String(maxLength: 250));
            AlterColumn("dbo.Organization", "Organization_summary", c => c.String());
            AlterColumn("dbo.Organization", "Development_plan", c => c.String());
            AlterColumn("dbo.Organization", "Interested_topics", c => c.String());
            AlterColumn("dbo.Scientist", "Name", c => c.String(maxLength: 250));
            AlterColumn("dbo.Scientist", "Affiliation", c => c.String(maxLength: 500));
            AlterColumn("dbo.Scientist", "Academic_position", c => c.String(maxLength: 250));
            AlterColumn("dbo.Scientist", "Academic_rank", c => c.String(maxLength: 250));
            AlterColumn("dbo.Scientist", "Position", c => c.String(maxLength: 250));
            AlterColumn("dbo.Scientist", "Email", c => c.String(maxLength: 250));
            AlterColumn("dbo.Scientist", "Citedby", c => c.String(maxLength: 250));
            AlterColumn("dbo.Scientist", "I10Index", c => c.String(maxLength: 250));
            AlterColumn("dbo.Scientist", "HIndex", c => c.String(maxLength: 250));
            AlterColumn("dbo.Scientist", "Hindex5y", c => c.String(maxLength: 250));
            AlterColumn("dbo.Scientist", "I10index5y", c => c.String(maxLength: 250));
            AlterColumn("dbo.Scientist", "Citedby5y", c => c.String(maxLength: 250));
            AlterColumn("dbo.Scientist", "Email_domain", c => c.String(maxLength: 250));
            AlterColumn("dbo.Scientist", "Nationality", c => c.String(maxLength: 250));
            AlterColumn("dbo.Scientist", "Bio_sketch", c => c.String());
            AlterColumn("dbo.Scientist", "Working_plan", c => c.String());
            AlterColumn("dbo.Scientist", "Interested_topics", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Scientist", "Interested_topics", c => c.String(maxLength: 500));
            AlterColumn("dbo.Scientist", "Working_plan", c => c.String(maxLength: 2500));
            AlterColumn("dbo.Scientist", "Bio_sketch", c => c.String(maxLength: 2500));
            AlterColumn("dbo.Scientist", "Nationality", c => c.String());
            AlterColumn("dbo.Scientist", "Email_domain", c => c.String());
            AlterColumn("dbo.Scientist", "Citedby5y", c => c.String());
            AlterColumn("dbo.Scientist", "I10index5y", c => c.String());
            AlterColumn("dbo.Scientist", "Hindex5y", c => c.String());
            AlterColumn("dbo.Scientist", "HIndex", c => c.String());
            AlterColumn("dbo.Scientist", "I10Index", c => c.String());
            AlterColumn("dbo.Scientist", "Citedby", c => c.String());
            AlterColumn("dbo.Scientist", "Email", c => c.String());
            AlterColumn("dbo.Scientist", "Position", c => c.String());
            AlterColumn("dbo.Scientist", "Academic_rank", c => c.String());
            AlterColumn("dbo.Scientist", "Academic_position", c => c.String());
            AlterColumn("dbo.Scientist", "Affiliation", c => c.String());
            AlterColumn("dbo.Scientist", "Name", c => c.String());
            AlterColumn("dbo.Organization", "Interested_topics", c => c.String(maxLength: 500));
            AlterColumn("dbo.Organization", "Development_plan", c => c.String(maxLength: 2500));
            AlterColumn("dbo.Organization", "Organization_summary", c => c.String(maxLength: 2500));
            AlterColumn("dbo.Organization", "Country", c => c.String());
            AlterColumn("dbo.Organization", "Email", c => c.String());
            AlterColumn("dbo.Organization", "Telephone", c => c.String());
            AlterColumn("dbo.Organization", "Logo", c => c.String());
            AlterColumn("dbo.Organization", "Website", c => c.String());
            AlterColumn("dbo.Organization", "Number_of_AI_Staff", c => c.String());
            AlterColumn("dbo.Organization", "Number_of_Staff", c => c.String());
            AlterColumn("dbo.Organization", "Scale", c => c.String());
            AlterColumn("dbo.Organization", "Ownership_type", c => c.String());
            AlterColumn("dbo.Organization", "Business_type", c => c.String());
            AlterColumn("dbo.Organization", "Founded_year", c => c.String());
            AlterColumn("dbo.Organization", "Name", c => c.String());
        }
    }
}
