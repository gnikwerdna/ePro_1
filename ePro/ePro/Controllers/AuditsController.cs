using ePro.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ePro.Models;
using System.Net;
using System.Data.Entity;

namespace ePro.Controllers
{
    public class AuditsController : Controller
    {
        private eProContext db = new eProContext();

        // GET: Audits
        public ActionResult Index()
        {
            return View(db.AuditRecords.ToList());
        }
        

         [Audit] 
         public ActionResult NormalExample() 
         { 
             return Content("Audit Fired!"); 
         } 
 

         public ActionResult PostingExample() 
         { 
             return View(); 
         } 
          
         [Audit] 
         [HttpPost] 
         public ActionResult PostingExample(PostingModel model) 
         { 
             return Content("Submitted!"); 
         } 
 

         public ActionResult AuditRecords() 
         {
             var audits = new eProContext().AuditRecords; 
             return View(audits); 
         } 







        // GET: Audits/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Audit audit = db.AuditRecords.Find(id);
            if (audit == null)
            {
                return HttpNotFound();
            }
            return View(audit);
        }

        // GET: Audits/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Audits/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AuditID,UserName,IPAddress,AreaAccessed,Timestamp")] Audit audit)
        {
            if (ModelState.IsValid)
            {
                audit.AuditID = Guid.NewGuid();
                db.AuditRecords.Add(audit);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(audit);
        }

        // GET: Audits/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Audit audit = db.AuditRecords.Find(id);
            if (audit == null)
            {
                return HttpNotFound();
            }
            return View(audit);
        }

        // POST: Audits/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AuditID,UserName,IPAddress,AreaAccessed,Timestamp")] Audit audit)
        {
            if (ModelState.IsValid)
            {
                db.Entry(audit).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(audit);
        }

        // GET: Audits/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Audit audit = db.AuditRecords.Find(id);
            if (audit == null)
            {
                return HttpNotFound();
            }
            return View(audit);
        }

        // POST: Audits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Audit audit = db.AuditRecords.Find(id);
            db.AuditRecords.Remove(audit);
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

    public class AuditAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //Stores the Request in an Accessible object 
            var request = filterContext.HttpContext.Request;


            //Generate an audit 
            Audit audit = new Audit()
            {
                AuditID = Guid.NewGuid(),
                IPAddress = request.ServerVariables["HTTP_X_FORWARDED_FOR"] ?? request.UserHostAddress,
                URLAccessed = request.RawUrl,
                TimeAccessed = DateTime.UtcNow,
                UserName = (request.IsAuthenticated) ? filterContext.HttpContext.User.Identity.Name : "Anonymous",
            };


            //Stores the Audit in the Database 
            eProContext context = new eProContext();
            context.AuditRecords.Add(audit);
            context.SaveChanges();


            base.OnActionExecuting(filterContext);
        }
    }

   }
