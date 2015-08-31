namespace ePro.DB.eprocontext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialcreate8 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ComplianceItems", "SubItemTo", c => c.Int(nullable: true));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ComplianceItems", "SubItemTo", c => c.Int());
        }
    }
}
