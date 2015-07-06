namespace ePro.DB.eprocontext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialcreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Compliances",
                c => new
                    {
                        ComplinanceID = c.Int(nullable: false, identity: true),
                        ComplianceFormID = c.Int(nullable: false),
                        ComplianceitemsID = c.Int(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ComplinanceID)
                .ForeignKey("dbo.ComplianceForms", t => t.ComplianceFormID, cascadeDelete: true)
                .ForeignKey("dbo.ComplianceItems", t => t.ComplianceitemsID, cascadeDelete: true)
                .Index(t => t.ComplianceFormID)
                .Index(t => t.ComplianceitemsID);
            
            CreateTable(
                "dbo.ComplianceForms",
                c => new
                    {
                        ComplianceFormID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                        ComplianceCategoryID = c.Int(),
                    })
                .PrimaryKey(t => t.ComplianceFormID)
                .ForeignKey("dbo.ComplianceCategories", t => t.ComplianceCategoryID)
                .Index(t => t.ComplianceCategoryID);
            
            CreateTable(
                "dbo.ComplianceCategories",
                c => new
                    {
                        ComplianceCategoryID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ComplianceCategoryID);
            
            CreateTable(
                "dbo.ProductListings",
                c => new
                    {
                        ProductListingID = c.Int(nullable: false, identity: true),
                        ProductName = c.String(nullable: false),
                        Source = c.String(),
                        ItemCode = c.String(),
                        Group = c.String(),
                        ABCClass = c.String(),
                        Status = c.String(),
                        ControlCode = c.String(),
                        Cond = c.String(),
                        Indicator = c.String(),
                        CyclicCode = c.String(),
                        UserGroup = c.String(),
                        UserGroup1 = c.String(),
                        ItemDescription = c.String(),
                        Description2 = c.String(),
                        Description3 = c.String(),
                        Unit = c.String(),
                        Weight = c.Int(nullable: false),
                        Pack = c.String(),
                        PackQty = c.Int(nullable: false),
                        Volume = c.Int(nullable: false),
                        ConversionFactor = c.Int(nullable: false),
                        AltUnitDesc = c.String(),
                        ItemGTIN = c.String(),
                        ModVAT = c.String(),
                        Trace = c.String(),
                        Storage = c.String(),
                        StandardCost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ReplacementCost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SalesCost = c.String(),
                        DutyPaidCost = c.String(),
                        InfoCost = c.String(),
                        ShelfLifeDays = c.String(),
                        WarrantyTypeFlag = c.String(),
                        DateLastChange = c.String(),
                        Per = c.String(),
                        ReorderPolicy = c.String(),
                        ReorderReview = c.String(),
                        ReorderBuyer = c.String(),
                        CreationDate = c.String(),
                        MovementCode = c.String(),
                        SalesType = c.String(),
                        SalesTaxPaidRate = c.String(),
                        SortCode = c.String(),
                        ExciseQty = c.String(),
                        UnSpscCode = c.String(),
                        AnalysisCode1 = c.String(),
                        AnalysisCode2 = c.String(),
                        AnalysisCode3 = c.String(),
                        SpareAnalysisCode = c.String(),
                        stkTallyCode = c.String(),
                        Brand = c.String(),
                        OriginFlag = c.String(),
                        OriginSource = c.String(),
                        PriceProtection = c.String(),
                        StkSpare1 = c.String(),
                        StkSpare2 = c.String(),
                        StkUserOnlyDate1 = c.String(),
                        StkUserOnlyDate2 = c.String(),
                        StkUserOnlyAlpha201 = c.String(),
                        StkUserOnlyAlpha202 = c.String(),
                        StkUserOnlyAlpha41 = c.String(),
                        StkUserOnlyAlpha42 = c.String(),
                        StkUserOnlyAlpha43 = c.String(),
                        StkUserOnlyAlpha44 = c.String(),
                        StkUserOnlyNum1 = c.Int(nullable: false),
                        StkUserOnlyNum2 = c.Int(nullable: false),
                        StkUserOnlyNum3 = c.Int(nullable: false),
                        StkUserOnlyNum4 = c.Int(nullable: false),
                        AddedDate = c.DateTime(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.ProductListingID);
            
            CreateTable(
                "dbo.ComplianceItems",
                c => new
                    {
                        ComplianceItemsID = c.Int(nullable: false, identity: true),
                        ItemName = c.String(),
                    })
                .PrimaryKey(t => t.ComplianceItemsID);
            
            CreateTable(
                "dbo.ProductCompliances",
                c => new
                    {
                        ProductListingID = c.Int(nullable: false),
                        ComplianceItemsID = c.Int(nullable: false),
                        ProductComplianceID = c.Int(nullable: false),
                        Checked = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProductListingID, t.ComplianceItemsID })
                .ForeignKey("dbo.ComplianceItems", t => t.ComplianceItemsID, cascadeDelete: true)
                .ForeignKey("dbo.ProductListings", t => t.ProductListingID, cascadeDelete: true)
                .Index(t => t.ProductListingID)
                .Index(t => t.ComplianceItemsID);
            
            CreateTable(
                "dbo.ProductComplianceForms",
                c => new
                    {
                        Product_ProductListingID = c.Int(nullable: false),
                        ComplianceForm_ComplianceFormID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Product_ProductListingID, t.ComplianceForm_ComplianceFormID })
                .ForeignKey("dbo.ProductListings", t => t.Product_ProductListingID, cascadeDelete: true)
                .ForeignKey("dbo.ComplianceForms", t => t.ComplianceForm_ComplianceFormID, cascadeDelete: true)
                .Index(t => t.Product_ProductListingID)
                .Index(t => t.ComplianceForm_ComplianceFormID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductCompliances", "ProductListingID", "dbo.ProductListings");
            DropForeignKey("dbo.ProductCompliances", "ComplianceItemsID", "dbo.ComplianceItems");
            DropForeignKey("dbo.Compliances", "ComplianceitemsID", "dbo.ComplianceItems");
            DropForeignKey("dbo.ProductComplianceForms", "ComplianceForm_ComplianceFormID", "dbo.ComplianceForms");
            DropForeignKey("dbo.ProductComplianceForms", "Product_ProductListingID", "dbo.ProductListings");
            DropForeignKey("dbo.Compliances", "ComplianceFormID", "dbo.ComplianceForms");
            DropForeignKey("dbo.ComplianceForms", "ComplianceCategoryID", "dbo.ComplianceCategories");
            DropIndex("dbo.ProductComplianceForms", new[] { "ComplianceForm_ComplianceFormID" });
            DropIndex("dbo.ProductComplianceForms", new[] { "Product_ProductListingID" });
            DropIndex("dbo.ProductCompliances", new[] { "ComplianceItemsID" });
            DropIndex("dbo.ProductCompliances", new[] { "ProductListingID" });
            DropIndex("dbo.ComplianceForms", new[] { "ComplianceCategoryID" });
            DropIndex("dbo.Compliances", new[] { "ComplianceitemsID" });
            DropIndex("dbo.Compliances", new[] { "ComplianceFormID" });
            DropTable("dbo.ProductComplianceForms");
            DropTable("dbo.ProductCompliances");
            DropTable("dbo.ComplianceItems");
            DropTable("dbo.ProductListings");
            DropTable("dbo.ComplianceCategories");
            DropTable("dbo.ComplianceForms");
            DropTable("dbo.Compliances");
        }
    }
}
