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

namespace ePro.Controllers
{
    public class FullProductListController : Controller
    {
        private eProContext db = new eProContext();

        // GET: FullProductList
        public ActionResult Index()
        {
            return View(db.ProductListings.ToList());
        }

        // GET: FullProductList/Details/5
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
            return View(productListing);
        }

        // GET: FullProductList/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FullProductList/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
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
        public ActionResult Edit([Bind(Include = "ProductListingID,ProductName,Source,ItemCode,Group,ABCClass,Status,ControlCode,Cond,Indicator,CyclicCode,UserGroup,UserGroup1,ItemDescription,Description2,Description3,Unit,Weight,Pack,PackQty,Volume,ConversionFactor,AltUnitDesc,ItemGTIN,ModVAT,Trace,Storage,StandardCost,ReplacementCost,SalesCost,DutyPaidCost,InfoCost,ShelfLifeDays,WarrantyTypeFlag,DateLastChange,Per,ReorderPolicy,ReorderReview,ReorderBuyer,CreationDate,MovementCode,SalesType,SalesTaxPaidRate,SortCode,ExciseQty,UnSpscCode,AnalysisCode1,AnalysisCode2,AnalysisCode3,SpareAnalysisCode,stkTallyCode,Brand,OriginFlag,OriginSource,PriceProtection,StkSpare1,StkSpare2,StkUserOnlyDate1,StkUserOnlyDate2,StkUserOnlyAlpha201,StkUserOnlyAlpha202,StkUserOnlyAlpha41,StkUserOnlyAlpha42,StkUserOnlyAlpha43,StkUserOnlyAlpha44,StkUserOnlyNum1,StkUserOnlyNum2,StkUserOnlyNum3,StkUserOnlyNum4")] ProductListing productListing)
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
