using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RedSismica.Data;
using RedSismica.Models;
using System;
using System.Linq;
using System.Collections.Generic;

namespace RedSismica.Controllers
{
    public class OrdenInspeccionController : Controller
    {
        private readonly RedSismicaContext _context;

        public OrdenInspeccionController(RedSismicaContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var ordenes = _context.OrdenesInspeccion
                .Include(o => o.EstacionSismologica)
                    .ThenInclude(e => e.Sismografo)
                .Include(o => o.Empleado) 
                .Where(o => !o.EstaCerrada)
                .ToList();

            return View(ordenes);
        }

        public IActionResult ConfirmarCierre(int id)
        {
            var orden = _context.OrdenesInspeccion
                .Include(o => o.EstacionSismologica)
                    .ThenInclude(e => e.Sismografo)
                .FirstOrDefault(o => o.Id == id);

            if (orden == null) return NotFound();

            ViewBag.Motivos = _context.TiposMotivoBaja.ToList();
            return View(orden);
        }

        [HttpPost]
        public IActionResult ConfirmarCierre(int id, string observacion, int[] motivoIds, string[] comentarios)
        {
            var orden = _context.OrdenesInspeccion
                .Include(o => o.EstacionSismologica)
                    .ThenInclude(e => e.Sismografo)
                .Include(o => o.MotivosBaja)
                .FirstOrDefault(o => o.Id == id);

            if (orden == null) return NotFound();

            // Validación de datos
            if (string.IsNullOrWhiteSpace(observacion) || motivoIds.Length == 0 || comentarios.Length != motivoIds.Length)
            {
                ModelState.AddModelError("", "Debe ingresar una observación y al menos un motivo con su comentario.");
                ViewBag.Motivos = _context.TiposMotivoBaja.ToList();
                return View(orden);
            }

            orden.ObservacionCierre = observacion;
            orden.EstaCerrada = true;
            orden.FechaCierre = DateTime.Now;

            // Motivos
            var motivos = motivoIds.Select((mId, i) => new MotivoBajaSismografo
            {
                TipoMotivoBajaId = mId,
                Comentario = comentarios[i],
                OrdenInspeccionId = orden.Id
            }).ToList();

            orden.MotivosBaja = motivos;

            // Cambiar estado del sismógrafo
            var sismografo = orden.EstacionSismologica.Sismografo;
            sismografo.Estado = "Fuera de Servicio";

            // Registrar cambio de estado
            var cambioEstado = new CambioEstadoSismografo
            {
                SismografoId = sismografo.Id,
                Estado = "Fuera de Servicio",
                FechaHoraCambio = DateTime.Now,
                EmpleadoId = GetEmpleadoIdActual()
            };

            _context.CambiosEstadoSismografo.Add(cambioEstado);

            // Guardar cambios
            _context.SaveChanges();

            // Notificar (simulado)
            NotificarCambioEstado(sismografo.Id, cambioEstado);

            return RedirectToAction("Index");
        }

        private int GetEmpleadoIdActual()
        {
            // Simula que el usuario actual es el empleado 1
            return 1;
        }

        private void NotificarCambioEstado(int sismografoId, CambioEstadoSismografo cambio)
        {
            // Simulación de notificación (puedes usar logs, emails, etc.)
            Console.WriteLine($"[NOTIFICACIÓN] Sismógrafo {sismografoId} marcado como '{cambio.Estado}' el {cambio.FechaHoraCambio}.");
        }
    
     // Acción para la pantalla extra
        public IActionResult SeleccionarAccion()
        {
            return View();
        }
    }
}

