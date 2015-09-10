using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ePro.DB;
using ePro.Models;
using PagedList;



namespace ePro.Controllers
{
    public class FullProductListController : Controller
    {
        private eProContext db = new eProContext();

        // GET: FullProductList
        [Audit]  
        [Authorize]
        public ActionResult Index(string PrdCode, string PrdGrp, string ProdSts, string Sorting_Order, string Search_Data, int? Page_No)
        {
            ViewBag.SortingName = String.IsNullOrEmpty(Sorting_Order) ? "Name_Description" : "";
            ViewBag.SortingGroup = Sorting_Order == "Product Group";
            

            //----Drop search--------------------------------
            var ProdCodeLst = new List<string>();
            var ProdCodeQry = from d in db.ProductListings
                           orderby d.ItemCode
                           select d.ItemCode;
            ProdCodeLst.AddRange(ProdCodeQry.Distinct());
            ViewBag.PrdCode = new SelectList(ProdCodeLst);
            //------------------------------------------------
            var ProdGrpLst = new List<string>();
            var ProdGrpQry = from d in db.ProductListings
                              orderby d.Group
                             select d.Group;
            ProdGrpLst.AddRange(ProdGrpQry.Distinct());
            ViewBag.PrdGrp = new SelectList(ProdGrpLst);
           //-------------------------------------------------
            var ProdStsLst = new List<string>();
            var ProdStsQry = from d in db.ProductListings
                             orderby d.Status
                             select d.Status;
            ProdStsLst.AddRange(ProdStsQry.Distinct());
            ViewBag.ProdSts = new SelectList(ProdStsLst);

            //----End drop down search------------------------
           var prodlist = from prods in db.ProductListings select prods; 
            {
                 
               if (!string.IsNullOrEmpty(PrdCode))
                 {
                      prodlist = prodlist.Where(prd => prd.ItemCode.ToUpper() == PrdCode);
                 }

               if (!string.IsNullOrEmpty(PrdGrp))
                 {
                     prodlist = prodlist.Where(prd => prd.Group.ToUpper()==PrdGrp);
                 }
               if (!string.IsNullOrEmpty(ProdSts))
               {
                   prodlist = prodlist.Where(prd => prd.Status.ToUpper() == ProdSts);
               }

  
             } 
             switch(Sorting_Order) 
             { 
                 case "Name_Description": 
                     prodlist = prodlist.OrderBy(prods => prods.ProductName); 
                     break; 
                 case "Group":
                    prodlist = prodlist.OrderBy(prods => prods.Group);
                    break; 
                 default: 
                      prodlist = prodlist.OrderByDescending(prods => prods.ProductListingID); 
                      break; 
  
             }
             int Size_Of_Page = 14;
             int No_Of_Page = (Page_No ?? 1);
           
          
             return View(prodlist.ToPagedList(No_Of_Page, Size_Of_Page)); 
           
             //return View(prodlist.ToList()); 
             //return View(db.ProductListings.ToList());
        }
        [Audit]
        [Authorize] 
        public ActionResult FileDownload(int fileid)
        {
            byte[] fileData;
            String fileName;
            var record = from p in db.Files
                         where p.ProductListingID == fileid
                         select p;
            fileData = (byte[])record.First().Content.ToArray();
            fileName = record.First().FileName;

            return File(fileData, "text", fileName);


        }

        // GET: FullProductList/Details/5
        [Audit]
        [Authorize] 
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductListing productListing = db.ProductListings.Find(id);
            if (productListing == null)
            {
                return HttpNotFound();
            }
            var associatedfiles = (from p in db.Files where p.ProductListingID == id select p);
            ViewBag.filelist = associatedfiles.ToList();
            return View(productListing);
        }

        // GET: FullProductList/Create
        [Audit]
        [Authorize] 
        public ActionResult Create()
        {
            return View();
        }

        // POST: FullProductList/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Audit]
        [Authorize] 
        public ActionResult Create([Bind(Include = "ProductListingID,ProductName,Source,ItemCode,Group,ABCClass,Status,ControlCode,Cond,Indicator,CyclicCode,UserGroup,UserGroup1,ItemDescription,Description2,Description3,Unit,Weight,Pack,PackQty,Volume,ConversionFactor,AltUnitDesc,ItemGTIN,ModVAT,Trace,Storage,StandardCost,ReplacementCost,SalesCost,DutyPaidCost,InfoCost,ShelfLifeDays,WarrantyTypeFlag,DateLastChange,Per,ReorderPolicy,ReorderReview,ReorderBuyer,CreationDate,MovementCode,SalesType,SalesTaxPaidRate,SortCode,ExciseQty,UnSpscCode,AnalysisCode1,AnalysisCode2,AnalysisCode3,SpareAnalysisCode,stkTallyCode,Brand,OriginFlag,OriginSource,PriceProtection,StkSpare1,StkSpare2,StkUserOnlyDate1,StkUserOnlyDate2,StkUserOnlyAlpha201,StkUserOnlyAlpha202,StkUserOnlyAlpha41,StkUserOnlyAlpha42,StkUserOnlyAlpha43,StkUserOnlyAlpha44,StkUserOnlyNum1,StkUserOnlyNum2,StkUserOnlyNum3,StkUserOnlyNum4")] ProductListing productListing)
        {
            if (ModelState.IsValid)
            {
                db.ProductListings.Add(productListing);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(productListing);
        }

        // GET: FullProductList/Edit/5
        [Audit]
        [Authorize] 
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductListing productListing = db.ProductListings.Find(id);
            if (productListing == null)
            {
                return HttpNotFound();
            }
            return View(productListing);
        }

        // POST: FullProductList/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Audit]
        [Authorize] 
        public ActionResult Edit([Bind(Include = "ProductListingID,ProductName,Source,ItemCode,Group,ABCClass,Status,ControlCode,Cond,Indicator,CyclicCode,UserGroup,UserGroup1,ItemDescription,Description2,Description3,Unit,Weight,Pack,PackQty,Volume,ConversionFactor,AltUnitDesc,ItemGTIN,ModVAT,Trace,Storage,StandardCost,ReplacementCost,SalesCost,DutyPaidCost,InfoCost,ShelfLifeDays,WarrantyTypeFlag,DateLastChange,Per,ReorderPolicy,ReorderReview,ReorderBuyer,CreationDate,MovementCode,SalesType,SalesTaxPaidRate,SortCode,ExciseQty,UnSpscCode,AnalysisCode1,AnalysisCode2,AnalysisCode3,SpareAnalysisCode,stkTallyCode,Brand,OriginFlag,OriginSource,PriceProtection,StkSpare1,StkSpare2,StkUserOnlyDate1,StkUserOnlyDate2,StkUserOnlyAlpha201,StkUserOnlyAlpha202,StkUserOnlyAlpha41,StkUserOnlyAlpha42,StkUserOnlyAlpha43,StkUserOnlyAlpha44,StkUserOnlyNum1,StkUserOnlyNum2,StkUserOnlyNum3,StkUserOnlyNum4")] ProductListing productListing, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productListing).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(productListing);
        }

        // GET: FullProductList/Delete/5
        [Audit]
        [Authorize] 
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductListing productListing = db.ProductListings.Find(id);
            if (productListing == null)
            {
                return HttpNotFound();
            }
            return View(productListing);
        }

        // POST: FullProductList/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Audit]
        [Authorize] 
        public ActionResult DeleteConfirmed(int id)
        {
            ProductListing productListing = db.ProductListings.Find(id);
            db.ProductListings.Remove(productListing);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
