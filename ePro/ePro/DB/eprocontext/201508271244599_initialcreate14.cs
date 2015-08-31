namespace ePro.DB.eprocontext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialcreate14 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Compliances", "ComplianceItems_ComplianceItemsID", "dbo.ComplianceItems");
            DropForeignKey("dbo.Compliances", "ComplianceItems_ComplianceItemsID1", "dbo.ComplianceItems");
            DropForeignKey("dbo.Compliances", "ComplianceItem_ComplianceItemsID", "dbo.ComplianceItems");
            DropIndex("dbo.Compliances", new[] { "ComplianceItems_ComplianceItemsID" });
            DropIndex("dbo.Compliances", new[] { "ComplianceItems_ComplianceItemsID1" });
            DropIndex("dbo.Compliances", new[] { "ComplianceItem_ComplianceItemsID" });
            DropColumn("dbo.Compliances", "ComplianceitemsID");
            RenameColumn(table: "dbo.Compliances", name: "ComplianceItem_ComplianceItemsID", newName: "ComplianceitemsID");
            AlterColumn("dbo.Compliances", "ComplianceitemsID", c => c.Int(nullable: true));
            CreateIndex("dbo.Compliances", "ComplianceitemsID");
            AddForeignKey("dbo.Compliances", "ComplianceitemsID", "dbo.ComplianceItems", "ComplianceItemsID", cascadeDelete: true);
            DropColumn("dbo.Compliances", "ComplianceItems_ComplianceItemsID");
            DropColumn("dbo.Compliances", "ComplianceItems_ComplianceItemsID1");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Compliances", "ComplianceItems_ComplianceItemsID1", c => c.Int());
            AddColumn("dbo.Compliances", "ComplianceItems_ComplianceItemsID", c => c.Int());
            DropForeignKey("dbo.Compliances", "ComplianceitemsID", "dbo.ComplianceItems");
            DropIndex("dbo.Compliances", new[] { "ComplianceitemsID" });
            AlterColumn("dbo.Compliances", "ComplianceitemsID", c => c.Int());
            RenameColumn(table: "dbo.Compliances", name: "ComplianceitemsID", newName: "ComplianceItem_ComplianceItemsID");
            AddColumn("dbo.Compliances", "ComplianceitemsID", c => c.Int(nullable: true));
            CreateIndex("dbo.Compliances", "ComplianceItem_ComplianceItemsID");
            CreateIndex("dbo.Compliances", "ComplianceItems_ComplianceItemsID1");
            CreateIndex("dbo.Compliances", "ComplianceItems_ComplianceItemsID");
            AddForeignKey("dbo.Compliances", "ComplianceItem_ComplianceItemsID", "dbo.ComplianceItems", "ComplianceItemsID");
            AddForeignKey("dbo.Compliances", "ComplianceItems_ComplianceItemsID1", "dbo.ComplianceItems", "ComplianceItemsID");
            AddForeignKey("dbo.Compliances", "ComplianceItems_ComplianceItemsID", "dbo.ComplianceItems", "ComplianceItemsID");
        }
    }
}
