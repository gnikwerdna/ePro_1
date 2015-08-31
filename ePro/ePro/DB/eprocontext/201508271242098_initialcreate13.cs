namespace ePro.DB.eprocontext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialcreate13 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Compliances", "ComplianceitemsID", "dbo.ComplianceItems");
            DropIndex("dbo.Compliances", new[] { "ComplianceitemsID" });
            AddColumn("dbo.Compliances", "ComplianceItems_ComplianceItemsID", c => c.Int());
            AddColumn("dbo.Compliances", "ComplianceItems_ComplianceItemsID1", c => c.Int());
            AddColumn("dbo.Compliances", "ComplianceItem_ComplianceItemsID", c => c.Int());
            CreateIndex("dbo.Compliances", "ComplianceItems_ComplianceItemsID");
            CreateIndex("dbo.Compliances", "ComplianceItems_ComplianceItemsID1");
            CreateIndex("dbo.Compliances", "ComplianceItem_ComplianceItemsID");
            AddForeignKey("dbo.Compliances", "ComplianceItems_ComplianceItemsID", "dbo.ComplianceItems", "ComplianceItemsID");
            AddForeignKey("dbo.Compliances", "ComplianceItems_ComplianceItemsID1", "dbo.ComplianceItems", "ComplianceItemsID");
            AddForeignKey("dbo.Compliances", "ComplianceItem_ComplianceItemsID", "dbo.ComplianceItems", "ComplianceItemsID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Compliances", "ComplianceItem_ComplianceItemsID", "dbo.ComplianceItems");
            DropForeignKey("dbo.Compliances", "ComplianceItems_ComplianceItemsID1", "dbo.ComplianceItems");
            DropForeignKey("dbo.Compliances", "ComplianceItems_ComplianceItemsID", "dbo.ComplianceItems");
            DropIndex("dbo.Compliances", new[] { "ComplianceItem_ComplianceItemsID" });
            DropIndex("dbo.Compliances", new[] { "ComplianceItems_ComplianceItemsID1" });
            DropIndex("dbo.Compliances", new[] { "ComplianceItems_ComplianceItemsID" });
            DropColumn("dbo.Compliances", "ComplianceItem_ComplianceItemsID");
            DropColumn("dbo.Compliances", "ComplianceItems_ComplianceItemsID1");
            DropColumn("dbo.Compliances", "ComplianceItems_ComplianceItemsID");
            CreateIndex("dbo.Compliances", "ComplianceitemsID");
            AddForeignKey("dbo.Compliances", "ComplianceitemsID", "dbo.ComplianceItems", "ComplianceItemsID", cascadeDelete: true);
        }
    }
}
