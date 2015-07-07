namespace ePro.DB.eprocontext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialcreate5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ComplianceItems", "EndItem", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ComplianceItems", "EndItem");
        }
    }
}
