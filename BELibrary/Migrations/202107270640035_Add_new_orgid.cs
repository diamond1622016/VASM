namespace BELibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_new_orgid : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Scientist", "OrgId", c => c.Guid());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Scientist", "OrgId");
        }
    }
}
