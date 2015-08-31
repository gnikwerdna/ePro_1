namespace ePro.DB.eprocontext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialcreate12 : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.ComplianceItems", "SubItemTo");
            AddForeignKey("dbo.ComplianceItems", "SubItemTo", "dbo.ComplianceItems", "ComplianceItemsID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ComplianceItems", "SubItemTo", "dbo.ComplianceItems");
            DropIndex("dbo.ComplianceItems", new[] { "SubItemTo" });
        }
    }
}
