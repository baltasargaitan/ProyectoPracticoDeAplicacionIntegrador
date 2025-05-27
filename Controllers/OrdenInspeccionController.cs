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

            ViewBag.Motivos = _context.TiposMotivoBaja.ToList();
            return View(orden);
        }

        [HttpPost]
        public IActionResult ConfirmarCierre(int id, string observacion, int[] motivoIds, string[] comentarios)
        {
            var orden = _context.OrdenesInspeccion
                .Include(o => o.EstacionSismologica)
                    .ThenInclude(e => e.Sismografo)
                .FirstOrDefault(o => o.Id == id);

            orden.ObservacionCierre = observacion;
            orden.EstaCerrada = true;
            orden.FechaCierre = DateTime.Now;

            orden.MotivosBaja = motivoIds.Select((mId, i) => new MotivoBajaSismografo
            {
                TipoMotivoBajaId = mId,
                Comentario = comentarios[i]
            }).ToList();

            orden.EstacionSismologica.Sismografo.Estado = "Fuera de Servicio";

            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}