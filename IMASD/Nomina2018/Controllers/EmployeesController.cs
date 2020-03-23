using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Nomina2018.Models;

namespace Nomina2018.Controllers
{
    public class EmployeesController : Controller
    {
        private Models.Database db = new Models.Database();

        // GET: Employees
        public ActionResult Index(int? pDepto)
        {
            List<Departamento> departamentos = db.Departamento.ToList();
            Departamento todos = new Departamento();
            todos.idDepto = 0;
            todos.nombre = "- Todos los Departamentos";
            departamentos.Insert(0, todos);
            ViewBag.ListaDepartamentos = departamentos;
            ViewBag.Filtro = pDepto ?? 0;

            List<Empleado> empleados = db.Empleado.ToList();
            if (ViewBag.Filtro > 0)
            {
                empleados = empleados.Where(row => row.idDepartamento.Equals(pDepto)).ToList();
            }

            return View(empleados);
        }

        // GET: Employees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empleado empleado = db.Empleado.Find(id);
            if (empleado == null)
            {
                return HttpNotFound();
            }

            List<Tabulador> tabulador = db.Tabulador.ToList();
            tabulador = tabulador.Where(row => row.idEmpleado.Equals(id)).ToList();
            ViewBag.TabuladorEmpleado = tabulador;

            return View(empleado);
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            List<Departamento> departamentos = db.Departamento.ToList();
            ViewBag.Departamentos = departamentos;
            return View();
        }

        // POST: Employees/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idEmpleado,codigo,nombre,apellidos,direccion,telefono,sueldo,idDepartamento,eliminado")] Empleado empleado)
        {
            if (ModelState.IsValid)
            {
                db.Empleado.Add(empleado);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            List<Departamento> departamentos = db.Departamento.ToList();
            ViewBag.Departamentos = departamentos;
            return View(empleado);
        }

        // GET: Employees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empleado empleado = db.Empleado.Find(id);
            if (empleado == null)
            {
                return HttpNotFound();
            }

            List<Departamento> departamentos = db.Departamento.ToList();
            ViewBag.Departamentos = departamentos;

            return View(empleado);
        }

        // POST: Employees/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idEmpleado,codigo,nombre,apellidos,direccion,telefono,sueldo,idDepartamento,eliminado")] Empleado empleado)
        {
            if (ModelState.IsValid)
            {
                db.Entry(empleado).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            List<Departamento> departamentos = db.Departamento.ToList();
            ViewBag.Departamentos = departamentos;

            return View(empleado);
        }

        // GET: Employees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empleado empleado = db.Empleado.Find(id);
            if (empleado == null)
            {
                return HttpNotFound();
            }
            return View(empleado);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Empleado empleado = db.Empleado.Find(id);
            db.Empleado.Remove(empleado);
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
