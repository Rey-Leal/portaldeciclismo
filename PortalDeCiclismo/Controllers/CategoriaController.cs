using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PortalDeCiclismo.DAL;
using PortalDeCiclismo.Models;
using Microsoft.Reporting.WebForms;

namespace PortalDeCiclismo.Controllers
{
    public class CategoriaController : Controller
    {
        private PortalContext db = new PortalContext();

        // GET: Categoria
        public ActionResult Index()
        {
            return View(db.Categorias.ToList());
        }

        // GET: Categoria/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categoria categoria = db.Categorias.Find(id);
            if (categoria == null)
            {
                return HttpNotFound();
            }
            return View(categoria);
        }

        // GET: Categoria/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Categoria/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CategoriaID,Titulo,Descricao")] Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                db.Categorias.Add(categoria);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(categoria);
        }

        // GET: Categoria/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categoria categoria = db.Categorias.Find(id);
            if (categoria == null)
            {
                return HttpNotFound();
            }
            return View(categoria);
        }

        // POST: Categoria/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CategoriaID,Titulo,Descricao")] Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                db.Entry(categoria).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(categoria);
        }

        // GET: Categoria/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categoria categoria = db.Categorias.Find(id);
            if (categoria == null)
            {
                return HttpNotFound();
            }
            return View(categoria);
        }

        // POST: Categoria/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Categoria categoria = db.Categorias.Find(id);
            db.Categorias.Remove(categoria);
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

        [HttpPost]
        public ActionResult ImprimirCategorias()
        {
            List<Categoria> categoria = new List<Categoria>();
            categoria = db.Categorias.ToList();

            LocalReport viewer = new LocalReport();
            viewer.DataSources.Clear();
            viewer.ReportPath = Server.MapPath("~/Reports/ListagemDeCategorias.rdl");

            ReportDataSource rptDataSource = new ReportDataSource("DsCategorias", categoria);
            viewer.DataSources.Add(rptDataSource);

            //Exportação para PDF
            const string ReportType = "PDF";
            string mimeType;
            string encoding;
            string fileNameExtension;

            string deviceInfo =
                "<DeviceInfo>" +
                "  <OutputFormat>PDF</OutputFormat>" +
                "</DeviceInfo>";

            Warning[] warnings;
            string[] streams;
            byte[] renderedBytes;

            renderedBytes = viewer.Render(
                ReportType,
                deviceInfo,
                        out mimeType,
                        out encoding,
                        out fileNameExtension,
                        out streams,
                        out warnings);

            return File(renderedBytes, mimeType);
        }
    }
}
