﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaInventario.AccesoDatos.Repositorio.IRepositorio
{
    public interface IUnidadTrabajo : IDisposable
    {
        IBodegaRepository Bodega {  get; }
        ICategoriaRepository Categoria { get; }
        IMarcaRepository Marca { get; }
        Task Guardar();

    }
}
