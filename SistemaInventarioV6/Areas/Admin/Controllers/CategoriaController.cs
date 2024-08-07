using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SistemaInventario.AccesoDatos.Repositorio.IRepositorio;
using SistemaInventario.Modelos;

namespace SistemaInventarioV6.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriaController : Controller
    {
        private readonly IUnidadTrabajo _unidadTrabajo;

        public CategoriaController(IUnidadTrabajo unidadTrabajo)
        {
            _unidadTrabajo = unidadTrabajo;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Upsert(int? id)
        {
            Categoria categoria = new Categoria();
            
            if( id == null )
            {
                categoria.Estado = true;
                return View(categoria);
            }
            categoria = await _unidadTrabajo.Categoria.Obtener(id.GetValueOrDefault());
            if(categoria == null)
            {
                return NotFound();
            }
            return View(categoria);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(Categoria categoria)
        {
            if(ModelState.IsValid)
            {
                if(categoria.Id == 0)
                {
                    await _unidadTrabajo.Categoria.Agregar(categoria);
                }
                else
                {
                    _unidadTrabajo.Categoria.Actualizar(categoria);
                }
                await _unidadTrabajo.Guardar();
                return RedirectToAction(nameof(Index));
            }
            return View(categoria);
        }

        #region API
        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            var todos = await _unidadTrabajo.Categoria.ObtenerTodos();
            return Json(new { data = todos});
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var categoriaDB = await _unidadTrabajo.Categoria.Obtener(id);
            if(categoriaDB == null)
            {
                return Json(new { success = false, message = "Error al borrar categoria" });
            }
            _unidadTrabajo.Categoria.Remover(categoriaDB);
            await _unidadTrabajo.Guardar();
            return Json(new { success = true, message = "categoria borada exitosamente" });
        }
        #endregion
    }
}
