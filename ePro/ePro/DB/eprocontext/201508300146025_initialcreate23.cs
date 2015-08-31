namespace ePro.DB.eprocontext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialcreate23 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ComplianceItemsComplianceItems", "ComplianceItems_ComplianceItemsID", "dbo.ComplianceItems");
            DropForeignKey("dbo.ComplianceItemsComplianceItems", "ComplianceItems_ComplianceItemsID1", "dbo.ComplianceItems");
            DropIndex("dbo.ComplianceItemsComplianceItems", new[] { "ComplianceItems_ComplianceItemsID" });
            DropIndex("dbo.ComplianceItemsComplianceItems", new[] { "ComplianceItems_ComplianceItemsID1" });
            CreateIndex("dbo.ComplianceItems", "SubItemTo");
            AddForeignKey("dbo.ComplianceItems", "SubItemTo", "dbo.ComplianceItems", "ComplianceItemsID");
            DropTable("dbo.ComplianceItemsComplianceItems");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ComplianceItemsComplianceItems",
                c => new
                    {
                        ComplianceItems_ComplianceItemsID = c.Int(nullable: false),
                        ComplianceItems_SubItemTo = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ComplianceItems_ComplianceItemsID, t.ComplianceItems_SubItemTo });
            
            DropForeignKey("dbo.ComplianceItems", "SubItemTo", "dbo.ComplianceItems");
            DropIndex("dbo.ComplianceItems", new[] { "SubItemTo" });
            CreateIndex("dbo.ComplianceItemsComplianceItems", "ComplianceItems_SubItemTo");
            CreateIndex("dbo.ComplianceItemsComplianceItems", "ComplianceItems_ComplianceItemsID");
            AddForeignKey("dbo.ComplianceItemsComplianceItems", "ComplianceItems_SubItemTo", "dbo.ComplianceItems", "ComplianceItemsID");
            AddForeignKey("dbo.ComplianceItemsComplianceItems", "ComplianceItems_ComplianceItemsID", "dbo.ComplianceItems", "ComplianceItemsID");
        }
    }
}
