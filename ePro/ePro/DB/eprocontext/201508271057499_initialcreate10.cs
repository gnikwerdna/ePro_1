namespace ePro.DB.eprocontext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialcreate10 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ComplianceItems", "ComplianceItems_ComplianceItemsID", c => c.Int());
            CreateIndex("dbo.ComplianceItems", "ComplianceItems_ComplianceItemsID");
            AddForeignKey("dbo.ComplianceItems", "ComplianceItems_ComplianceItemsID", "dbo.ComplianceItems", "ComplianceItemsID");
            DropColumn("dbo.ComplianceItems", "Discriminator");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ComplianceItems", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            DropForeignKey("dbo.ComplianceItems", "ComplianceItems_ComplianceItemsID", "dbo.ComplianceItems");
            DropIndex("dbo.ComplianceItems", new[] { "ComplianceItems_ComplianceItemsID" });
            DropColumn("dbo.ComplianceItems", "ComplianceItems_ComplianceItemsID");
        }
    }
}
