using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SistemaInventario.AccesoDatos.Repositorio.IRepositorio;
using SistemaInventario.Modelos;

namespace SistemaInventarioV6.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BodegaController : Controller
    {
        private readonly IUnidadTrabajo _unidadTrabajo;

        public BodegaController(IUnidadTrabajo unidadTrabajo)
        {
            _unidadTrabajo = unidadTrabajo;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Upsert(int? id)
        {
            Bodega bodega = new Bodega();
            
            if( id == null )
            {
                bodega.Estado = true;
                return View(bodega);
            }
            bodega = await _unidadTrabajo.Bodega.Obtener(id.GetValueOrDefault());
            if(bodega == null)
            {
                return NotFound();
            }
            return View(bodega);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(Bodega bodega)
        {
            if(ModelState.IsValid)
            {
                if(bodega.id == 0)
                {
                    await _unidadTrabajo.Bodega.Agregar(bodega);
                }
                else
                {
                    _unidadTrabajo.Bodega.Actualizar(bodega);
                }
                await _unidadTrabajo.Guardar();
                return RedirectToAction(nameof(Index));
            }
            return View(bodega);
        }

        #region API
        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            var todos = await _unidadTrabajo.Bodega.ObtenerTodos();
            return Json(new { data = todos});
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var bodegaDB = await _unidadTrabajo.Bodega.Obtener(id);
            if(bodegaDB == null)
            {
                return Json(new { success = false, message = "Error al borrar Bodega" });
            }
            _unidadTrabajo.Bodega.Remover(bodegaDB);
            await _unidadTrabajo.Guardar();
            return Json(new { success = true, message = "Bodega borada exitosamente" });
        }
        #endregion
    }
}
