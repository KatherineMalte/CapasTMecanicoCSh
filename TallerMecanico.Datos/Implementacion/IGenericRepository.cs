using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TallerMecanico.Datos.Implementacion
{
    public interface IGenericRepository<T> where T : class
    {
        Task<List<T>> Listar();
        Task<bool> Insertar(T modelo);
        Task<T> TraerPorId(int id);
        Task<bool> Actualizar(T modelo);
        Task<bool> Eliminar(int id);
    }
}
