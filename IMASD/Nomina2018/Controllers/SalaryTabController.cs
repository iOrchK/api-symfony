using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Nomina2018.Models;

namespace Nomina2018.Controllers
{
    public class SalaryTabController : Controller
    {
        private Models.Database db = new Models.Database();

        // GET: SalaryTab
        public ActionResult Index(int? pEmp, int? pDepto)
        {
            List<Departamento> departamentos = db.Departamento.ToList();
            List<Empleado> empleados = db.Empleado.ToList();
            Departamento todos = new Departamento();
            Empleado todosEmp = new Empleado();
            todos.idDepto = 0;
            todos.nombre = "- Todos los Departamentos";
            todosEmp.idEmpleado = 0;
            todosEmp.nombre = "- Todos los Empleados";
            departamentos.Insert(0, todos);
            empleados.Insert(0, todosEmp);

            ViewBag.ListaDepartamentos = departamentos;
            ViewBag.ListaEmpleados = empleados;
            ViewBag.FiltroEmp = pEmp ?? 0;
            ViewBag.FiltroDepto = pDepto ?? 0;

            List<Tabulador> tabulador = db.Tabulador.OrderBy(item => item.inicioPeriodo).ToList();
            if (ViewBag.FiltroEmp > 0 && ViewBag.FiltroDepto == 0)
            {
                tabulador = tabulador.Where(row => row.idEmpleado.Equals(ViewBag.FiltroEmp)).ToList();
            }
            else if (ViewBag.FiltroEmp == 0 && ViewBag.FiltroDepto > 0)
            {
                tabulador = tabulador.Where(row => row.Empleado.idDepartamento.Equals(ViewBag.FiltroDepto)).ToList();
            }
            else if (ViewBag.FiltroEmp > 0 && ViewBag.FiltroDepto > 0)
            {
                tabulador = tabulador.Where(row => row.idEmpleado.Equals(ViewBag.FiltroEmp) && row.Empleado.idDepartamento.Equals(ViewBag.FiltroDepto)).ToList();
            }

            return View(tabulador);
        }

        // GET: SalaryTab/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tabulador tabulador = db.Tabulador.Find(id);
            if (tabulador == null)
            {
                return HttpNotFound();
            }
            return View(tabulador);
        }

        // GET: SalaryTab/Create
        public ActionResult Create()
        {
            List<Empleado> empleados = db.Empleado.ToList();
            ViewBag.Empleados = empleados;
            Tabulador tabulador = new Tabulador();
            tabulador.idEmpleado = empleados.Select(y => y.idEmpleado).FirstOrDefault();
            return View(tabulador);
        }

        // POST: SalaryTab/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idTabulador,idEmpleado,inicioPeriodo,finPeriodo,sueldo,compensacion,descuento,percepcion,pagado")] Tabulador tabulador)
        {
            if (ModelState.IsValid)
            {
                tabulador.Empleado = db.Empleado.Find(tabulador.idEmpleado);
                db.Tabulador.Add(tabulador);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            List<Empleado> empleados = db.Empleado.ToList();
            ViewBag.Empleados = empleados;

            return View(tabulador);
        }

        // GET: SalaryTab/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tabulador tabulador = db.Tabulador.Find(id);
            if (tabulador == null)
            {
                return HttpNotFound();
            }

            List<Empleado> empleados = db.Empleado.ToList();
            ViewBag.Empleados = empleados;

            return View(tabulador);
        }

        // POST: SalaryTab/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idTabulador,idEmpleado,inicioPeriodo,finPeriodo,sueldo,compensacion,descuento,percepcion,pagado")] Tabulador tabulador)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tabulador).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            List<Empleado> empleados = db.Empleado.ToList();
            ViewBag.Empleados = empleados;

            return View(tabulador);
        }

        // GET: SalaryTab/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tabulador tabulador = db.Tabulador.Find(id);
            if (tabulador == null)
            {
                return HttpNotFound();
            }
            return View(tabulador);
        }

        // POST: SalaryTab/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tabulador tabulador = db.Tabulador.Find(id);
            db.Tabulador.Remove(tabulador);
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
