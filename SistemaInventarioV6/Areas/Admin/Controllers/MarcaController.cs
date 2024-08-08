using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SistemaInventario.AccesoDatos.Repositorio.IRepositorio;
using SistemaInventario.Modelos;

namespace SistemaInventarioV6.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MarcaController : Controller
    {
        private readonly IUnidadTrabajo _unidadTrabajo;

        public MarcaController(IUnidadTrabajo unidadTrabajo)
        {
            _unidadTrabajo = unidadTrabajo;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Upsert(int? id)
        {
            Marca marca = new Marca();
            
            if( id == null )
            {
                marca.Estado = true;
                return View(marca);
            }
            marca = await _unidadTrabajo.Marca.Obtener(id.GetValueOrDefault());
            if(marca == null)
            {
                return NotFound();
            }
            return View(marca);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(Marca marca)
        {
            if(ModelState.IsValid)
            {
                if(marca.Id == 0)
                {
                    await _unidadTrabajo.Marca.Agregar(marca);
                }
                else
                {
                    _unidadTrabajo.Marca.Actualizar(marca);
                }
                await _unidadTrabajo.Guardar();
                return RedirectToAction(nameof(Index));
            }
            return View(marca);
        }

        #region API
        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            var todos = await _unidadTrabajo.Marca.ObtenerTodos();
            return Json(new { data = todos});
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var marcaDB = await _unidadTrabajo.Marca.Obtener(id);
            if(marcaDB == null)
            {
                return Json(new { success = false, message = "Error al borrar marca" });
            }
            _unidadTrabajo.Marca.Remover(marcaDB);
            await _unidadTrabajo.Guardar();
            return Json(new { success = true, message = "marca borada exitosamente" });
        }
        #endregion
    }
}
