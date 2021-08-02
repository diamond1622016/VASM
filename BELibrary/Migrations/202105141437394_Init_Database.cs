namespace BELibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init_Database : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Account",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FullName = c.String(maxLength: 50),
                        Phone = c.String(maxLength: 15),
                        UserName = c.String(maxLength: 50),
                        LinkAvatar = c.String(maxLength: 250),
                        Gender = c.Boolean(nullable: false),
                        Password = c.String(maxLength: 250),
                        Role = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Scientist",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Scholar_id = c.Int(nullable: false),
                        Name = c.String(),
                        Interests = c.String(),
                        Url_picture = c.String(maxLength: 250),
                        Affiliation = c.String(),
                        Email = c.String(),
                        Cites_per_year = c.String(),
                        Citedby = c.Int(nullable: false),
                        I10Index = c.Int(nullable: false),
                        HIndex = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Scientist");
            DropTable("dbo.Account");
        }
    }
}
