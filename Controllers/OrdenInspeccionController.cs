using Microsoft.AspNetCore.Mvc;
using RedSismica.Data;
using RedSismica.Models;
using System.Linq;
using System;

namespace RedSismica.Controllers
{
    public class OrdenInspeccionController : Controller
    {
        private readonly GestorCierreInspeccion _gestor;

        public OrdenInspeccionController(RedSismicaContext context)
        {
            // Inyección del contexto y creación del gestor
            _gestor = new GestorCierreInspeccion(context, new Sesion());
        }

        public IActionResult SeleccionarAccion()
        {
            // Acción para seleccionar la acción a realizar
            return View();
        }

        // Acción para mostrar las órdenes completamente realizadas
        public IActionResult Index()
        {
            try
            {
                var ordenes = _gestor.buscarOrdenes();
                return View(ordenes);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View("Error");
            }
        }

        // Acción GET para confirmar el cierre de una orden
        public IActionResult ConfirmarCierre(int id)
        {
            var orden = _gestor.tomarSeleccionOrden(id);
            if (orden == null)
            {
                ViewBag.ErrorMessage = "No se encontró la orden seleccionada.";
                return View("Error");
            }

            ViewBag.Motivos = _gestor.buscarMotivosFueraServicio();
            return View(orden);
        }

        // Acción POST para procesar el cierre de una orden
        [HttpPost]
        public IActionResult ConfirmarCierre(int id, string observacion, int[] motivoIds, string[] comentarios)
        {
            var orden = _gestor.tomarSeleccionOrden(id);
            if (orden == null)
            {
                ViewBag.ErrorMessage = "No se encontró la orden seleccionada.";
                return View("Error");
            }

            // Validar datos ingresados
            if (string.IsNullOrWhiteSpace(observacion) || motivoIds.Length == 0 || comentarios.Length != motivoIds.Length)
            {
                ModelState.AddModelError("", "Debe ingresar una observación y al menos un motivo con su comentario.");
                ViewBag.Motivos = _gestor.buscarMotivosFueraServicio();
                return View(orden);
            }

            try
            {
                // Delegar la lógica al gestor
                _gestor.tomarObservacion(orden, observacion);
                for (int i = 0; i < motivoIds.Length; i++)
                {
                    _gestor.tomarMotivoFueraServicio(orden, motivoIds[i], comentarios[i]);
                }
                _gestor.cerrarOrden(orden);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View("Error");
            }
        }
    }
}