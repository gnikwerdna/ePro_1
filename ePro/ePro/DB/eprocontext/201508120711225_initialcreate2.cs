namespace ePro.DB.eprocontext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialcreate2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Audits",
                c => new
                    {
                        AuditID = c.Guid(nullable: false),
                        IPAddress = c.String(),
                        UserName = c.String(),
                        URLAccessed = c.String(),
                        TimeAccessed = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.AuditID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Audits");
        }
    }
}
