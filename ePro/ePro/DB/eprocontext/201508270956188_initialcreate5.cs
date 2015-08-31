namespace ePro.DB.eprocontext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialcreate5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ComplianceItems", "SubItemTo", c => c.Int());
            AddColumn("dbo.ComplianceItems", "Discriminator", c => c.String(nullable: false, maxLength: 128));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ComplianceItems", "Discriminator");
            DropColumn("dbo.ComplianceItems", "SubItemTo");
        }
    }
}
