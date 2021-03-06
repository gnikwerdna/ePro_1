﻿using System;
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
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Configuration;
using PagedList;
using ePro.Helpers;



namespace ePro.Controllers
{
    public class ProductsController : Controller
    {
       
        private eProContext db = new eProContext();
        private int CheckRelProductComplianceForms(int id) 
        {
            string connectionString = ConfigurationManager.ConnectionStrings["cmdstrings"].ToString();
            int intreccheck = 0;
             using (SqlConnection connection =new SqlConnection(connectionString))
             using (SqlCommand cmdchk = new SqlCommand("Select counselect count(*) from [ProductComplianceForms] where [Product_ProductListingID]="+id,connection))
             {
                  cmdchk.Connection.Open();
                  intreccheck = (int)cmdchk.ExecuteScalar();
                  cmdchk.Connection.Close();
             }
             return intreccheck;

        }
       private void AddComplianceProduct(int? productid, int? complianceitemid, int checkedvalue)
        {
           string connectionString = ConfigurationManager.ConnectionStrings["cmdstrings"].ToString();
           int intreccheck=0;
           
       
            using (SqlConnection connection =new SqlConnection(connectionString))
            using (SqlCommand cmdchk = new SqlCommand("Select count(*) from ProductCompliances where ComplianceItemsID =" + complianceitemid + " and ProductListingID=" + productid, connection))
            {
                cmdchk.Connection.Open();
                intreccheck = (int)cmdchk.ExecuteScalar();
                cmdchk.Connection.Close();

            }
            using (SqlConnection connection2 = new SqlConnection(connectionString))
                if (intreccheck == 0)
                {
                    using (SqlCommand command = new SqlCommand("", connection2))
                    {



                        command.Connection.Open();
                        command.CommandText = "insert into ProductCompliances (ComplianceItemsID,ProductListingID,Checked) values (" + complianceitemid + "," + productid + "," + checkedvalue + ")";

                        command.ExecuteNonQuery();
                        command.Connection.Close();

                    }
                }
                else
                {
                    using (SqlCommand command = new SqlCommand("", connection2))
                    {



                        command.Connection.Open();
                        command.CommandText = "Delete from  ProductCompliances where ComplianceItemsID =" + complianceitemid + " and ProductListingID=" + productid;

                        command.ExecuteNonQuery();
                        command.Connection.Close();

                    }

                }


        }




        // GET: Products
       public ActionResult Index(string Compliance_Sorting_Order, string Search_Data, int? id, int? complianceformID, int? compid, int? page)
        {

            int pageSize = 14;
            int pageNumber = (page ?? 1);

            ViewBag.CurrentSort = Compliance_Sorting_Order;
           ViewBag.SortingCompliance = String.IsNullOrEmpty(Compliance_Sorting_Order) ? "ComplianceForm" : "";
           // ViewBag.SortingCompliance = Compliance_Sorting_Order == "ComplianceForm";
            var viewModel = new ProductIndexData();
            
            if (compid!=null)
            {
                int? updprodid = id;
                int? upcompid = compid;
                AddComplianceProduct(updprodid, upcompid, 1);
            }
            switch (Compliance_Sorting_Order)
            {
                case "ComplianceForm":

                  viewModel.Products = db.Products
                .Include(i => i.ComplianceForms.Select(c => c.ComplianceCategory))
               .OrderByDescending(i => i.ComplianceForms.Count);
                    break;
                                  
                default:
                    viewModel.Products = db.Products.Where(i => i.ComplianceForms.Count >=1)
                   
                    .Include(i => i.ComplianceForms.Select(c => c.ComplianceCategory))
                   
                     .OrderBy(i => i.ProductName);
                    break;

            }
            if (Search_Data != null)
            {
                viewModel.Products = db.Products.Where(i => i.ProductName == Search_Data) ;

            }
            
           // viewModel.Products = db.Products 
          //      .Include(i => i.ComplianceForms.Select(c => c.ComplianceCategory))
          //      .OrderBy(i => i.ProductName);
            if (id != null)
            {
                ViewBag.ProductID = id.Value;
                viewModel.ComplianceForms = viewModel.Products.Where(
                    i => i.ProductListingID == id.Value).Single().ComplianceForms ;
            }

            if (complianceformID != null)
            {
                ViewBag.complianceformID = complianceformID.Value;
                // Lazy loading
                //viewModel.Enrollments = viewModel.Courses.Where(
                //    x => x.CourseID == courseID).Single().Enrollments;
                // Explicit loading
                var selectedcomplianceform = viewModel.ComplianceForms.Where(x => x.ComplianceFormID == complianceformID).Single();
                db.Entry(selectedcomplianceform).Collection(x => x.Compliances).Load();
                foreach (Compliance compliance in selectedcomplianceform.Compliances)
                {
                    db.Entry(compliance).Reference(x => x.ComplianceItem).Load();
                }

                viewModel.Compliances = selectedcomplianceform.Compliances;
            }
            var productcomp = (from p in db.ProductCompliance where p.ProductListingID == id select p);
           ViewBag.productcp = productcomp.ToList();
          
            var proditems = from a in db.ProductCompliance select a;
            ViewBag.ProdItems = new SelectList(proditems, "ProductListingID", "ProductName");


            decimal totalPages = ((decimal)(viewModel.Products.Count() / (decimal)pageSize));
            ViewBag.TotalPages = Math.Ceiling(totalPages);
            viewModel.Products = viewModel.Products.ToPagedList(pageNumber, pageSize);
            ViewBag.OnePageOfProducts = viewModel.Products;
            ViewBag.PageNumber = pageNumber;
          
            return View(viewModel); 
           
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            var associatedfiles = (from p in db.Files where p.ProductListingID == id select p);
            ViewBag.filelist = associatedfiles.ToList();
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            var product = new Product();
            product.ComplianceForms = new List<ComplianceForm>();
            PopulateAssignedComplianceFormData(product);
            return View();
        }

        
        private void PopulateAssignedComplianceFormData(Product product)
        {
            var allcomplianceforms = db.ComplianceForms;
           
                var ProductComplianceForms = new HashSet<int>(product.ComplianceForms.Select(c => c.ComplianceFormID));
           
            var viewModel = new List<AssignedComplianceFormData>();
            foreach (var complianceform in allcomplianceforms)
            {
                viewModel.Add(new AssignedComplianceFormData
                {
                    ComplianceFormID = complianceform.ComplianceFormID,
                    Title= complianceform.Name,
                    Assigned = ProductComplianceForms.Contains(complianceform.ComplianceFormID)
                });
            }
            ViewBag.Complinances = viewModel;
        }
        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductListingID,AddedDate,ProductName,Source")] Product product, string[] selectedComplianceForms,HttpPostedFileBase upload)
        {
            if (selectedComplianceForms != null)
            {
                product.ComplianceForms = new List<ComplianceForm>();
                foreach (var complianceforms in selectedComplianceForms)
                {
                    var complianceformToAdd = db.ComplianceForms.Find(int.Parse(complianceforms));
                    product.ComplianceForms.Add(complianceformToAdd);
                }
            }
            if (ModelState.IsValid)
            {
                if (upload != null && upload.ContentLength >0)
                {
                    var avatar = new File
                    {
                        FileName = System.IO.Path.GetFileName(upload.FileName),
                        FileType = FileType.Avatar,
                        ContentType = upload.ContentType
                    };
                    using (var reader = new System.IO.BinaryReader(upload.InputStream))
                    {
                        avatar.Content = reader.ReadBytes(upload.ContentLength);
                    }
                    product.Files = new List<File> { avatar };

                    
                }
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("../FullProductList/Index");
            }
            PopulateAssignedComplianceFormData(product);
            return View(product);
        }
        private void UpdateProduct(string[] selectedComplainceForms, Product ProductToUpdate)
        {
            if (selectedComplainceForms == null)
            {
                ProductToUpdate.ComplianceForms = new List<ComplianceForm>();
                return;
            }

            var selectedComplianceFormsHS = new HashSet<string>(selectedComplainceForms);
            var ProductComplianceform = new HashSet<int>
                (ProductToUpdate.ComplianceForms.Select(c => c.ComplianceFormID));
            foreach (var complianceform in db.ComplianceForms)
            {
                if (selectedComplianceFormsHS.Contains(complianceform.ComplianceFormID.ToString()))
                {
                    if (!ProductComplianceform.Contains(complianceform.ComplianceFormID))
                    {
                        ProductToUpdate.ComplianceForms.Add(complianceform);
                    }
                }
                else
                {
                    if (ProductComplianceform.Contains(complianceform.ComplianceFormID))
                    {
                        ProductToUpdate.ComplianceForms.Remove(complianceform);
                    }
                }
            }
        }
       

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products
               
                .Include(i => i.ComplianceForms)
                .Where(i => i.ProductListingID == id)
                .SingleOrDefault();
            PopulateAssignedComplianceFormData(product);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
       // public ActionResult Edit(int? id, [Bind(Include = "ProductListingID,AddedDate,ProductName,Source")] Product product, string[] selectedComplinanceform, HttpPostedFileBase upload)
       public ActionResult Edit(int? id, string[] selectedComplinanceform,HttpPostedFileBase upload)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var ProductToUpdate = db.Products

               .Include(i => i.ComplianceForms)
               .Where(i => i.ProductListingID == id)
               .Single();

            if (TryUpdateModel(ProductToUpdate, "",
               new string[] { "ProductListingID,AddedDate,ProductName,Source" }))
            {
                try
                {
                    //if (String.IsNullOrWhiteSpace(instructorToUpdate.OfficeAssignment.Location))
                    //   {
                    //       instructorToUpdate.OfficeAssignment = null;
                    //   }
                    if (upload != null && upload.ContentLength > 0)
                    {
                        var avatar = new File
                        {
                            FileName = System.IO.Path.GetFileName(upload.FileName),
                            FileType = FileType.Avatar,
                            ContentType = upload.ContentType
                        };
                        using (var reader = new System.IO.BinaryReader(upload.InputStream))
                        {
                            avatar.Content = reader.ReadBytes(upload.ContentLength);
                        }
                        ProductToUpdate.Files = new List<File> { avatar };


                    }
                    UpdateProduct(selectedComplinanceform, ProductToUpdate);

                    db.SaveChanges();

                    return RedirectToAction("../FullProductList/Index");
                }
                catch (RetryLimitExceededException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
               
            }
            PopulateAssignedComplianceFormData(ProductToUpdate);
            return View(ProductToUpdate);
        }
        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        //multiple file uploader
       // Get:
      [HttpGet]
      public ActionResult Uploader(int id) 
        {
            Product product = db.Products.Find(id);
            ViewBag.productid = id;
            return View(product); 
         }



       [HttpPost]
       [ValidateAntiForgeryToken]
      public ActionResult Uploader(int? id)
        {

            foreach(string uploads in Request.Files)
            {
                if (!Request.Files[uploads].HasFile()) continue;
                int ProductListingID = id.GetValueOrDefault(0);
                if (ProductListingID != 0)
                {
                    int FileType = 1;
                    string mimeType = Request.Files[uploads].ContentType;
                    System.IO.Stream fileStream = Request.Files[uploads].InputStream;
                    string fileName = System.IO.Path.GetFileName(Request.Files[uploads].FileName);
                    int fileLength = Request.Files[uploads].ContentLength;
                    byte[] fileData = new byte[fileLength];
                    fileStream.Read(fileData, 0, fileLength);
                    string connect = ConfigurationManager.ConnectionStrings["cmdstrings"].ToString();
                    using (var conn = new SqlConnection(connect))
                    {
                        var qry = "INSERT INTO Files (Content, ContentType, FileName,ProductListingID,FileType) VALUES (@FileContent, @MimeType, @FileName,@ProductListingID,@FileType)";
                        var cmd = new SqlCommand(qry, conn);
                        cmd.Parameters.AddWithValue("@FileContent", fileData);
                        cmd.Parameters.AddWithValue("@MimeType", mimeType);
                        cmd.Parameters.AddWithValue("@FileName", fileName);
                        cmd.Parameters.AddWithValue("@ProductListingID", ProductListingID);
                        cmd.Parameters.AddWithValue("@FileType", FileType);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }

            }

            return RedirectToAction("../FullProductList/Index");
        }

       [Audit]
       [Authorize]
       public FileContentResult GetFile(int id)
       {
           SqlDataReader rdr; byte[] fileContent = null;
           string mimeType = ""; string fileName = "";
           string connect = ConfigurationManager.ConnectionStrings["cmdstrings"].ToString();

           using (var conn = new SqlConnection(connect))
           {
               var qry = "SELECT Content, ContentType, FileName FROM Files WHERE FileId = @ID";
               var cmd = new SqlCommand(qry, conn);
               cmd.Parameters.AddWithValue("@ID", id);
               conn.Open();
               rdr = cmd.ExecuteReader();
               if (rdr.HasRows)
               {
                   rdr.Read();
                   fileContent = (byte[])rdr["Content"];
                   mimeType = rdr["ContentType"].ToString();
                   fileName = rdr["FileName"].ToString();
               }
           }
           return File(fileContent, mimeType, fileName);
       }
       public ActionResult DeleteFile(int id)
       {
          
           string connect = ConfigurationManager.ConnectionStrings["cmdstrings"].ToString();

           using (var conn = new SqlConnection(connect))
           {
               var qry = "Delete FROM Files WHERE FileId = @ID";
               var cmd = new SqlCommand(qry, conn);
               cmd.Parameters.AddWithValue("@ID", id);
               conn.Open();
               cmd.ExecuteReader();
              
           }
           return RedirectToAction("../FullProductList/Index");
       }
        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
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
