namespace BELibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_New_Column_OrganizationId_And_FK : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Scientist", "OrganizationId", c => c.Guid());
            CreateIndex("dbo.Scientist", "OrganizationId");
            AddForeignKey("dbo.Scientist", "OrganizationId", "dbo.Organization", "Id");
            DropColumn("dbo.Scientist", "OrgId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Scientist", "OrgId", c => c.Guid());
            DropForeignKey("dbo.Scientist", "OrganizationId", "dbo.Organization");
            DropIndex("dbo.Scientist", new[] { "OrganizationId" });
            DropColumn("dbo.Scientist", "OrganizationId");
        }
    }
}
