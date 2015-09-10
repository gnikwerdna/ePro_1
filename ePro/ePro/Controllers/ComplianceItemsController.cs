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
using ePro.ViewModels;
using PagedList;

namespace ePro.Controllers
{
    public class ComplianceItemsController : Controller
    {
        private eProContext db = new eProContext();

        // GET: ComplianceItems
        public ActionResult Index(int? page)
        {
            int pageSize = 14;
            int pageNumber = (page ?? 1);
            var viewModel = new ComplianceItems();
            var productSubItem = from a in db.ComplianceItems
                                 join s in db.ComplianceItemSubItems on a.ComplianceItemsID equals s.SubItemTo
                                 orderby a.ItemName
                                 // join p in db.ProductCompliance on s.SubItemTo equals p.ComplianceItemsID  
                                 //select new SubItemsViewModel { SubItemTo = s.SubItemTo, ItemName = a.ItemName, ComplianceItemsID = s.ComplianceItemID, Checked = p.Checked };
                                 select new SubItemsViewModel { SubItemTo = s.SubItemTo, ItemName = a.ItemName, ComplianceItemsID = s.ComplianceItemID };
            if (productSubItem.Count() > 1)
            {
                ViewBag.productSubItem = productSubItem.ToList();
            }
            decimal totalPages = ((decimal)(productSubItem.Count() / (decimal)pageSize));
            ViewBag.TotalPages = Math.Ceiling(totalPages);
            //viewModel.Products = viewModel.Products.ToPagedList(pageNumber, pageSize);

            ViewBag.OnePageOfProducts = productSubItem.ToPagedList(pageNumber, pageSize);
            ViewBag.PageNumber = pageNumber;
            return View(db.ComplianceItems.ToList());
        }

        // GET: ComplianceItems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ComplianceItems complianceItems = db.ComplianceItems.Find(id);
            if (complianceItems == null)
            {
                return HttpNotFound();
            }
            return View(complianceItems);
        }

        // GET: ComplianceItems/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ComplianceItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ComplianceItemsID,ItemName,EndItem")] ComplianceItems complianceItems)
        {
            if (ModelState.IsValid)
            {
                db.ComplianceItems.Add(complianceItems);
                //db.ComplianceItems.Add()
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(complianceItems);
        }

        // GET: ComplianceItems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ComplianceItems complianceItems = db.ComplianceItems.Find(id);


            var ddcontent = from p in db.ComplianceItems orderby p.ItemName select new { p.ComplianceItemsID, p.ItemName };
            var x = ddcontent.ToList().Select(c => new SelectListItem { Text = c.ItemName, Value = c.ComplianceItemsID.ToString() }).ToList();
            ViewBag.ComplianceIts = x;


            if (complianceItems == null)
            {
                return HttpNotFound();
            }
            return View(complianceItems);
        }

        // POST: ComplianceItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ComplianceItemsID,ItemName,EndItem,SubItemTo")] ComplianceItems complianceItems, int SubItemTo, int ComplianceItemsID)
        {
            if (ModelState.IsValid)
            {

                AddRelCompItems(ComplianceItemsID, SubItemTo);

                db.Entry(complianceItems).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(complianceItems);
        }
        public void AddRelCompItems(int CompItemId, int SubItemId)
        {

            var newSubitem = new ComplianceItemSubItem();
            newSubitem.ComplianceItemID = CompItemId;
            newSubitem.SubItemTo = SubItemId;
            db.ComplianceItemSubItems.Add(newSubitem);
            db.SaveChanges();


        }
        public ActionResult Remove(int itemid, int subitemid)
        {

            List<ComplianceItemSubItem> list = (from sub in db.ComplianceItemSubItems where sub.ComplianceItemID == itemid && sub.SubItemTo == subitemid select sub).ToList();
            foreach (ComplianceItemSubItem its in list)
            {
                db.ComplianceItemSubItems.Remove(its);

            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        // GET: ComplianceItems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ComplianceItems complianceItems = db.ComplianceItems.Find(id);
            if (complianceItems == null)
            {
                return HttpNotFound();
            }
            return View(complianceItems);
        }

        // POST: ComplianceItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ComplianceItems complianceItems = db.ComplianceItems.Find(id);
            db.ComplianceItems.Remove(complianceItems);
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
