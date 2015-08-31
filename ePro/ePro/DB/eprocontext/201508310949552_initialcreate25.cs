namespace ePro.DB.eprocontext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialcreate25 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ComplianceItemsComplianceItems", "ComplianceItems_ComplianceItemsID", "dbo.ComplianceItems");
            DropForeignKey("dbo.ComplianceItemsComplianceItems", "ComplianceItems_ComplianceItemsID1", "dbo.ComplianceItems");
            DropIndex("dbo.ComplianceItemsComplianceItems", new[] { "ComplianceItems_ComplianceItemsID" });
            DropIndex("dbo.ComplianceItemsComplianceItems", new[] { "ComplianceItems_ComplianceItemsID1" });
            CreateTable(
                "dbo.ComplianceItemSubItems",
                c => new
                    {
                        ComplianceItemID = c.Int(nullable: false),
                        SubItemTo = c.Int(nullable: false),
                        ComplianceItems_ComplianceItemsID = c.Int(),
                        SubItem_ComplianceItemsID = c.Int(),
                        ComplianceItems_ComplianceItemsID1 = c.Int(),
                    })
                .PrimaryKey(t => new { t.ComplianceItemID, t.SubItemTo })
                .ForeignKey("dbo.ComplianceItems", t => t.ComplianceItems_ComplianceItemsID)
                .ForeignKey("dbo.ComplianceItems", t => t.SubItem_ComplianceItemsID)
                .ForeignKey("dbo.ComplianceItems", t => t.SubItemTo, cascadeDelete: true)
                .ForeignKey("dbo.ComplianceItems", t => t.ComplianceItems_ComplianceItemsID1)
                .Index(t => t.SubItemTo)
                .Index(t => t.ComplianceItems_ComplianceItemsID)
                .Index(t => t.SubItem_ComplianceItemsID)
                .Index(t => t.ComplianceItems_ComplianceItemsID1);
            
            DropTable("dbo.ComplianceItemsComplianceItems");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ComplianceItemsComplianceItems",
                c => new
                    {
                        ComplianceItems_ComplianceItemsID = c.Int(nullable: false),
                        ComplianceItems_ComplianceItemsID1 = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ComplianceItems_ComplianceItemsID, t.ComplianceItems_ComplianceItemsID1 });
            
            DropForeignKey("dbo.ComplianceItemSubItems", "ComplianceItems_ComplianceItemsID1", "dbo.ComplianceItems");
            DropForeignKey("dbo.ComplianceItemSubItems", "SubItemTo", "dbo.ComplianceItems");
            DropForeignKey("dbo.ComplianceItemSubItems", "SubItem_ComplianceItemsID", "dbo.ComplianceItems");
            DropForeignKey("dbo.ComplianceItemSubItems", "ComplianceItems_ComplianceItemsID", "dbo.ComplianceItems");
            DropIndex("dbo.ComplianceItemSubItems", new[] { "ComplianceItems_ComplianceItemsID1" });
            DropIndex("dbo.ComplianceItemSubItems", new[] { "SubItem_ComplianceItemsID" });
            DropIndex("dbo.ComplianceItemSubItems", new[] { "ComplianceItems_ComplianceItemsID" });
            DropIndex("dbo.ComplianceItemSubItems", new[] { "SubItemTo" });
            DropTable("dbo.ComplianceItemSubItems");
            CreateIndex("dbo.ComplianceItemsComplianceItems", "ComplianceItems_ComplianceItemsID1");
            CreateIndex("dbo.ComplianceItemsComplianceItems", "ComplianceItems_ComplianceItemsID");
            AddForeignKey("dbo.ComplianceItemsComplianceItems", "ComplianceItems_ComplianceItemsID1", "dbo.ComplianceItems", "ComplianceItemsID");
            AddForeignKey("dbo.ComplianceItemsComplianceItems", "ComplianceItems_ComplianceItemsID", "dbo.ComplianceItems", "ComplianceItemsID");
        }
    }
}
