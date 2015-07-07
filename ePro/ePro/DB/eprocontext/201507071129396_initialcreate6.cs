namespace ePro.DB.eprocontext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialcreate6 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Compliances", "Order", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Compliances", "Order");
        }
    }
}
