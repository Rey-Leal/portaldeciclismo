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
    public class AtletaController : Controller
    {
        private PortalContext db = new PortalContext();

        // GET: Atleta
        public ActionResult Index()
        {
            int tipo;
            int idUsuario;
            System.Web.HttpCookie cookieTipo = Request.Cookies["tipo"];
            System.Web.HttpCookie cookieIdUsuario = Request.Cookies["IdUsuario"];

            if (cookieTipo != null && cookieIdUsuario != null)
            {
                tipo = Convert.ToInt16(cookieTipo.Value);
                idUsuario = Convert.ToInt16(cookieIdUsuario.Value);

                //Atleta
                if (tipo == 1)
                {
                    var listaDeAtletas = db.Atletas.Where(d => d.ID == idUsuario);
                    return View(listaDeAtletas.ToList());
                }
                //Tecnico
                else
                {
                    return View(db.Atletas.ToList());
                }
            }
            else
            {
                return View("Error");
            }
        }

        // GET: Atleta/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Atleta atleta = db.Atletas.Find(id);
            if (atleta == null)
            {
                return HttpNotFound();
            }
            return View(atleta);
        }

        // GET: Atleta/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Atleta/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Nome,Apelido,Email,DataDeNascimento,Peso,Altura,Sexo")] Atleta atleta)
        {
            if (ModelState.IsValid)
            {
                db.Atletas.Add(atleta);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(atleta);
        }

        // GET: Atleta/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Atleta atleta = db.Atletas.Find(id);
            if (atleta == null)
            {
                return HttpNotFound();
            }
            return View(atleta);
        }

        // POST: Atleta/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Nome,Apelido,Email,DataDeNascimento,Peso,Altura,Sexo")] Atleta atleta)
        {
            if (ModelState.IsValid)
            {
                db.Entry(atleta).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(atleta);
        }

        // GET: Atleta/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Atleta atleta = db.Atletas.Find(id);
            if (atleta == null)
            {
                return HttpNotFound();
            }
            return View(atleta);
        }

        // POST: Atleta/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Atleta atleta = db.Atletas.Find(id);
            db.Atletas.Remove(atleta);
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
        public ActionResult ImprimirAtletas()
        {
            List<Atleta> atleta = new List<Atleta>();
            atleta = db.Atletas.ToList();

            LocalReport viewer = new LocalReport();
            viewer.DataSources.Clear();
            viewer.ReportPath = Server.MapPath("~/Reports/ListagemDeAtletas.rdl");

            ReportDataSource rptDataSource = new ReportDataSource("DsAtletas", atleta);
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
