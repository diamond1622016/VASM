namespace BELibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_fk_account_tbale : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Account", "ScientistId", c => c.Guid());
            CreateIndex("dbo.Account", "ScientistId");
            AddForeignKey("dbo.Account", "ScientistId", "dbo.Scientist", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Account", "ScientistId", "dbo.Scientist");
            DropIndex("dbo.Account", new[] { "ScientistId" });
            DropColumn("dbo.Account", "ScientistId");
        }
    }
}
