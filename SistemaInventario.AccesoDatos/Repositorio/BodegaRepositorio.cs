using SistemaInventario.AccesoDatos.Data;
using SistemaInventario.AccesoDatos.Repositorio.IRepositorio;
using SistemaInventario.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaInventario.AccesoDatos.Repositorio
{
    public class BodegaRepositorio : Repositorio<Bodega>, IBodegaRepository
    {
        private readonly ApplicationDbContext _db;

        public BodegaRepositorio(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Actualizar(Bodega bodega)
        {
            var bodegaDB = _db.Bodegas.FirstOrDefault(b=> b.id == bodega.id);
            if(bodega != null)
            {
                bodegaDB.Nombre = bodega.Nombre;
                bodegaDB.Descripcion = bodega.Descripcion;
                bodegaDB.Estado = bodega.Estado;
                _db.SaveChanges();
            }
        }
    }
}
