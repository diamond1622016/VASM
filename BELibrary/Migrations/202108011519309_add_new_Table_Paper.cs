namespace BELibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_new_Table_Paper : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Paper",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Author_pub_id = c.String(maxLength: 20),
                        Title = c.String(),
                        Pub_year = c.String(maxLength: 20),
                        Authors = c.String(),
                        Journal = c.String(maxLength: 500),
                        Volume = c.String(maxLength: 20),
                        Number = c.String(maxLength: 20),
                        Pages = c.String(maxLength: 20),
                        Publisher = c.String(maxLength: 250),
                        Note = c.String(),
                        Cites_per_year = c.String(),
                        Num_citations = c.String(maxLength: 20),
                        Paper_link = c.String(),
                        ScientistId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Scientist", t => t.ScientistId)
                .Index(t => t.ScientistId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Paper", "ScientistId", "dbo.Scientist");
            DropIndex("dbo.Paper", new[] { "ScientistId" });
            DropTable("dbo.Paper");
        }
    }
}
