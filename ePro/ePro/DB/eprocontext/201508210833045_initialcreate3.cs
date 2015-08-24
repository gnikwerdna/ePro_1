namespace ePro.DB.eprocontext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialcreate3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ComplianceItems", "SubItemTo", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ComplianceItems", "SubItemTo");
        }
    }
}
