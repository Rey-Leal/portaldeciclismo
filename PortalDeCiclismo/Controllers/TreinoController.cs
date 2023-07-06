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
    public class TreinoController : Controller
    {
        private PortalContext db = new PortalContext();

        // GET: Treino
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
                    var treinos = db.Treinos.Where(d => d.AtletaID == idUsuario).ToList();
                    return View(treinos.ToList());

                }
                //Tecnico
                else
                {
                    var treinos = db.Treinos.Include(t => t.Atleta).Include(t => t.Categoria).Include(t => t.Tecnico);
                    return View(treinos.ToList());
                }
            }
            else
            {
                return View("Error");
            }
        }

        // GET: Treino por Tecnico        
        public ActionResult TreinoPorTecnico()
        {
            int idUsuario;
            System.Web.HttpCookie cookieIdUsuario = Request.Cookies["IdUsuario"];

            if (cookieIdUsuario != null)
            {
                idUsuario = Convert.ToInt16(cookieIdUsuario.Value);
                var treinos = db.Treinos.Where(t => t.TecnicoID == idUsuario);
                return View(treinos.ToList());
            }
            else
            {
                return View("Error");
            }
        }

        // GET: Treino/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Treino treino = db.Treinos.Find(id);
            if (treino == null)
            {
                return HttpNotFound();
            }
            return View(treino);
        }

        // GET: Treino/Create
        public ActionResult Create()
        {
            ViewBag.AtletaID = new SelectList(db.Atletas, "ID", "Nome");
            ViewBag.CategoriaID = new SelectList(db.Categorias, "CategoriaID", "Titulo");
            ViewBag.TecnicoID = new SelectList(db.Tecnicos, "TecnicoID", "Nome");
            return View();
        }

        // POST: Treino/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TreinoID,AtletaID,CategoriaID,TecnicoID,Desempenho,Observacao")] Treino treino)
        {
            if (ModelState.IsValid)
            {
                db.Treinos.Add(treino);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AtletaID = new SelectList(db.Atletas, "ID", "Nome", treino.AtletaID);
            ViewBag.CategoriaID = new SelectList(db.Categorias, "CategoriaID", "Titulo", treino.CategoriaID);
            ViewBag.TecnicoID = new SelectList(db.Tecnicos, "TecnicoID", "Nome", treino.TecnicoID);
            return View(treino);
        }

        // GET: Treino/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Treino treino = db.Treinos.Find(id);
            if (treino == null)
            {
                return HttpNotFound();
            }
            ViewBag.AtletaID = new SelectList(db.Atletas, "ID", "Nome", treino.AtletaID);
            ViewBag.CategoriaID = new SelectList(db.Categorias, "CategoriaID", "Titulo", treino.CategoriaID);
            ViewBag.TecnicoID = new SelectList(db.Tecnicos, "TecnicoID", "Nome", treino.TecnicoID);
            return View(treino);
        }

        // POST: Treino/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TreinoID,AtletaID,CategoriaID,TecnicoID,Desempenho,Observacao")] Treino treino)
        {
            if (ModelState.IsValid)
            {
                db.Entry(treino).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AtletaID = new SelectList(db.Atletas, "ID", "Nome", treino.AtletaID);
            ViewBag.CategoriaID = new SelectList(db.Categorias, "CategoriaID", "Titulo", treino.CategoriaID);
            ViewBag.TecnicoID = new SelectList(db.Tecnicos, "TecnicoID", "Nome", treino.TecnicoID);
            return View(treino);
        }

        // GET: Treino/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Treino treino = db.Treinos.Find(id);
            if (treino == null)
            {
                return HttpNotFound();
            }
            return View(treino);
        }

        // POST: Treino/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Treino treino = db.Treinos.Find(id);
            db.Treinos.Remove(treino);
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
        public ActionResult ImprimirTreinos()
        {
            List<Treino> treino = new List<Treino>();
            treino = db.Treinos.ToList();

            LocalReport viewer = new LocalReport();
            viewer.DataSources.Clear();
            viewer.ReportPath = Server.MapPath("~/Reports/ListagemDeTreinos.rdl");

            ReportDataSource rptDataSource = new ReportDataSource("DsTreinos", treino);
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
