namespace ePro.DB.eprocontext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialcreate1 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.ProductCompliances");
            AlterColumn("dbo.ProductCompliances", "ProductComplianceID", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.ProductCompliances", "ProductComplianceID");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.ProductCompliances");
            AlterColumn("dbo.ProductCompliances", "ProductComplianceID", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.ProductCompliances", new[] { "ProductListingID", "ComplianceItemsID" });
        }
    }
}
