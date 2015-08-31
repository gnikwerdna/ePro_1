namespace ePro.DB.eprocontext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialcreate17 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Compliances", "ComplianceitemsID", "dbo.ComplianceItems");
            DropIndex("dbo.Compliances", new[] { "ComplianceitemsID" });
            DropColumn("dbo.Compliances", "ComplianceitemsID");
            AddColumn("dbo.Compliances", "ComplianceitemsID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            //AddColumn("dbo.Compliances", "ComplianceitemsID", c => c.Int(nullable: false));
        }
    }
}
