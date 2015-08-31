namespace ePro.DB.eprocontext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialcreate18 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ComplianceItems", "SubItemTo", "dbo.ComplianceItems");
            DropIndex("dbo.ComplianceItems", new[] { "SubItemTo" });
            CreateTable(
                "dbo.ComplianceItemsComplianceItems",
                c => new
                    {
                        ComplianceItems_ComplianceItemsID = c.Int(nullable: false),
                        ComplianceItems_ComplianceItemsID1 = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ComplianceItems_ComplianceItemsID, t.ComplianceItems_ComplianceItemsID1 })
                .ForeignKey("dbo.ComplianceItems", t => t.ComplianceItems_ComplianceItemsID)
                .ForeignKey("dbo.ComplianceItems", t => t.ComplianceItems_ComplianceItemsID1)
                .Index(t => t.ComplianceItems_ComplianceItemsID)
                .Index(t => t.ComplianceItems_ComplianceItemsID1);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ComplianceItemsComplianceItems", "ComplianceItems_ComplianceItemsID1", "dbo.ComplianceItems");
            DropForeignKey("dbo.ComplianceItemsComplianceItems", "ComplianceItems_ComplianceItemsID", "dbo.ComplianceItems");
            DropIndex("dbo.ComplianceItemsComplianceItems", new[] { "ComplianceItems_ComplianceItemsID1" });
            DropIndex("dbo.ComplianceItemsComplianceItems", new[] { "ComplianceItems_ComplianceItemsID" });
            DropTable("dbo.ComplianceItemsComplianceItems");
            CreateIndex("dbo.ComplianceItems", "SubItemTo");
            AddForeignKey("dbo.ComplianceItems", "SubItemTo", "dbo.ComplianceItems", "ComplianceItemsID");
        }
    }
}
