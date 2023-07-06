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
    public class TecnicoController : Controller
    {
        private PortalContext db = new PortalContext();

        // GET: Tecnico
        public ActionResult Index()
        {
            return View(db.Tecnicos.ToList());
        }

        // GET: Tecnico/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tecnico tecnico = db.Tecnicos.Find(id);
            if (tecnico == null)
            {
                return HttpNotFound();
            }
            return View(tecnico);
        }

        // GET: Tecnico/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tecnico/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TecnicoID,Nome,Especialidade,Email")] Tecnico tecnico)
        {
            if (ModelState.IsValid)
            {
                db.Tecnicos.Add(tecnico);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tecnico);
        }

        // GET: Tecnico/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tecnico tecnico = db.Tecnicos.Find(id);
            if (tecnico == null)
            {
                return HttpNotFound();
            }
            return View(tecnico);
        }

        // POST: Tecnico/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TecnicoID,Nome,Especialidade,Email")] Tecnico tecnico)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tecnico).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tecnico);
        }

        // GET: Tecnico/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tecnico tecnico = db.Tecnicos.Find(id);
            if (tecnico == null)
            {
                return HttpNotFound();
            }
            return View(tecnico);
        }

        // POST: Tecnico/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tecnico tecnico = db.Tecnicos.Find(id);
            db.Tecnicos.Remove(tecnico);
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
        public ActionResult ImprimirTecnicos()
        {
            List<Tecnico> tecnico = new List<Tecnico>();
            tecnico = db.Tecnicos.ToList();

            LocalReport viewer = new LocalReport();
            viewer.DataSources.Clear();
            viewer.ReportPath = Server.MapPath("~/Reports/ListagemDeTecnicos.rdl");

            ReportDataSource rptDataSource = new ReportDataSource("DsTecnicos", tecnico);
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
