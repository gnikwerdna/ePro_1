namespace ePro.DB.eprocontext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialcreate11 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ComplianceItems", "ComplianceItems_ComplianceItemsID", "dbo.ComplianceItems");
            DropIndex("dbo.ComplianceItems", new[] { "ComplianceItems_ComplianceItemsID" });
            AlterColumn("dbo.ComplianceItems", "SubItemTo", c => c.Int(nullable: true));
            DropColumn("dbo.ComplianceItems", "ComplianceItems_ComplianceItemsID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ComplianceItems", "ComplianceItems_ComplianceItemsID", c => c.Int());
            AlterColumn("dbo.ComplianceItems", "SubItemTo", c => c.Int());
            CreateIndex("dbo.ComplianceItems", "ComplianceItems_ComplianceItemsID");
            AddForeignKey("dbo.ComplianceItems", "ComplianceItems_ComplianceItemsID", "dbo.ComplianceItems", "ComplianceItemsID");
        }
    }
}
