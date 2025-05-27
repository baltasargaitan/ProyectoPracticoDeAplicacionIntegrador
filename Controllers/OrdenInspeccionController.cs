using Microsoft.AspNetCore.Mvc;
using RedSismica.Data;
using RedSismica.Models;
using System.Linq;

namespace RedSismica.Controllers
{
    public class OrdenInspeccionController : Controller
    {
        private readonly RedSismicaContext _context;
        private readonly GestorCierreInspeccion _gestor;

        public OrdenInspeccionController(RedSismicaContext context)
        {
            _context = context;
            _gestor = new GestorCierreInspeccion(_context);
        }

        public IActionResult Index()
        {
            int empleadoId = GetEmpleadoIdActual();
            var ordenes = _gestor.getOrdenesCompletamenteRealizadas(empleadoId);
            return View(ordenes);
        }

        public IActionResult ConfirmarCierre(int id)
        {
            var orden = _gestor.tomarSeleccionOrden(id);
            if (orden == null) return NotFound();

            ViewBag.Motivos = _context.TiposMotivoBaja.ToList();
            return View(orden);
        }

        [HttpPost]
        public IActionResult ConfirmarCierre(int id, string observacion, int[] motivoIds, string[] comentarios)
        {
            var orden = _gestor.tomarSeleccionOrden(id);
            if (orden == null) return NotFound();

            if (string.IsNullOrWhiteSpace(observacion) || motivoIds.Length == 0 || comentarios.Length != motivoIds.Length)
            {
                ModelState.AddModelError("", "Debe ingresar una observación y al menos un motivo con su comentario.");
                ViewBag.Motivos = _context.TiposMotivoBaja.ToList();
                return View(orden);
            }

            _gestor.tomarObservacionCierre(orden, observacion);
            _gestor.tomarMotivosFueraDeServicio(orden, motivoIds, comentarios);
            _gestor.cerrarOrden(orden, GetEmpleadoIdActual());

            return RedirectToAction("Index");
        }

        private int GetEmpleadoIdActual()
        {
            // Simula que el usuario actual es el empleado 1
            return 1;
        }
    }
}