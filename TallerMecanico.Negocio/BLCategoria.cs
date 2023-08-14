using TallerMecanico.Datos.Contrato;
using TallerMecanico.Entidades;
using TallerMecanico.Datos.Implementacion;
using TallerMecanico.Datos;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace TallerMecanico.Negocio
{
    public class BLCategoria : Controller
    {
        private readonly ILogger<BLCategoria> _logger;
        private readonly IGenericRepository<Categoria> _categoriaRepository;
        public BLCategoria(ILogger<BLCategoria> logger,
         IGenericRepository<Categoria> categoriaRepository)
        {
            _logger = logger;
            _categoriaRepository = categoriaRepository;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            List<Categoria> _listar = await _categoriaRepository.Listar();
            return StatusCode(StatusCodes.Status200OK, _listar);
        }
        [HttpGet]
        public async Task<IActionResult> TraerPorId(int id)
        {
            Categoria _TraerPorId = await _categoriaRepository.TraerPorId(id);
            if (_TraerPorId.Equals(true))
                return StatusCode(StatusCodes.Status200OK, new { valor = _TraerPorId, msg = "ok" });
            else
                return StatusCode(StatusCodes.Status500InternalServerError, new { valor = _TraerPorId, msg = "errror" });

        }

        [HttpDelete]
        public async Task<IActionResult> Eliminar(int id)
        {
            bool _resultado = await _categoriaRepository.Eliminar(id);

            if (_resultado)
                return StatusCode(StatusCodes.Status200OK, new { valor = _resultado, msg = "ok" });
            else
                return StatusCode(StatusCodes.Status500InternalServerError, new { valor = _resultado, msg = "errror" });
        }

        public IActionResult Privacy()
        {
            return View();
        }
        /*
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        */
        /*
        public List<Categoria> Listar()
        {
            DAOCategoria daCategoria = new DAOCategoria();
            return daCategoria.Listar();
        }

        public Categoria TraerPorId(int Id)
        {
            DAOCategoria daCategoria = new DAOCategoria();
            return daCategoria.TraerPorId(Id);
        }

        public int Insertar(Categoria Categoria)
        {
            DAOCategoria daCategoria = new DAOCategoria();
            return daCategoria.Insertar(Categoria);
        }

        public int Actualizar(Categoria Categoria)
        {
            DAOCategoria daCategoria = new DAOCategoria();
            return daCategoria.Actualizar(Categoria);
        }

        public int Eliminar(int Id)
        {
            DAOCategoria daCategoria = new DAOCategoria();
            return daCategoria.Eliminar(Id);
        }
        */

    }
}
