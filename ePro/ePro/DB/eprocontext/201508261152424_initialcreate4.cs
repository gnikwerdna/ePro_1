namespace ePro.DB.eprocontext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialcreate4 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.ComplianceItems", "SubItemTo");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ComplianceItems", "SubItemTo", c => c.Int(nullable: false));
        }
    }
}
