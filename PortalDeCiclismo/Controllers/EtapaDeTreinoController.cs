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

namespace PortalDeCiclismo.Controllers
{
    public class EtapaDeTreinoController : Controller
    {
        private PortalContext db = new PortalContext();

        // GET: EtapaDeTreino
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
                    PortalContext db = new PortalContext();
                    var etapasDeTreinos = db.EtapasDeTreino.Include(t => t.Treino).Include(t => t.Treino.Atleta)
                                            .Where(t => t.Treino.AtletaID == idUsuario);
                    return View(etapasDeTreinos.ToList());
                }
                //Tecnico
                else
                {
                    PortalContext db = new PortalContext();
                    var etapasDeTreinos = db.EtapasDeTreino.Include(t => t.Treino).Include(t => t.Treino.Tecnico)
                                            .Where(t => t.Treino.TecnicoID == idUsuario);
                    return View(etapasDeTreinos.ToList());
                }
            }
            else
            {
                return View("Error");
            }
        }

        [HttpPost]
        public ActionResult EtapaDeUmTreino(int? id)
        {
            PortalContext db = new PortalContext();
            var etapasDeTreinos = db.EtapasDeTreino.Include(t => t.Treino)
                                    .Where(t => t.Treino.TreinoID == id);
            return View(etapasDeTreinos.ToList());
        }

        // GET: EtapaDeTreino/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EtapaDeTreino etapaDeTreino = db.EtapasDeTreino.Find(id);
            if (etapaDeTreino == null)
            {
                return HttpNotFound();
            }
            return View(etapaDeTreino);
        }

        // GET: EtapaDeTreino/Create
        public ActionResult Create()
        {
            //ViewBag.TreinoID = new SelectList(db.Treinos, "TreinoID", "Observacao");
            ViewBag.TreinoID = new SelectList(db.Treinos, "TreinoID", "TreinoID");
            return View();
        }

        // POST: EtapaDeTreino/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EtapaDeTreinoID,TreinoID,Descricao")] EtapaDeTreino etapaDeTreino)
        {
            if (ModelState.IsValid)
            {
                db.EtapasDeTreino.Add(etapaDeTreino);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TreinoID = new SelectList(db.Treinos, "TreinoID", "Observacao", etapaDeTreino.TreinoID);
            return View(etapaDeTreino);
        }

        // GET: EtapaDeTreino/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EtapaDeTreino etapaDeTreino = db.EtapasDeTreino.Find(id);
            if (etapaDeTreino == null)
            {
                return HttpNotFound();
            }
            ViewBag.TreinoID = new SelectList(db.Treinos, "TreinoID", "TreinoID", etapaDeTreino.TreinoID);
            return View(etapaDeTreino);
        }

        // POST: EtapaDeTreino/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EtapaDeTreinoID,TreinoID,Descricao")] EtapaDeTreino etapaDeTreino)
        {
            if (ModelState.IsValid)
            {
                db.Entry(etapaDeTreino).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TreinoID = new SelectList(db.Treinos, "TreinoID", "Observacao", etapaDeTreino.TreinoID);
            return View(etapaDeTreino);
        }

        // GET: EtapaDeTreino/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EtapaDeTreino etapaDeTreino = db.EtapasDeTreino.Find(id);
            if (etapaDeTreino == null)
            {
                return HttpNotFound();
            }
            return View(etapaDeTreino);
        }

        // POST: EtapaDeTreino/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EtapaDeTreino etapaDeTreino = db.EtapasDeTreino.Find(id);
            db.EtapasDeTreino.Remove(etapaDeTreino);
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
