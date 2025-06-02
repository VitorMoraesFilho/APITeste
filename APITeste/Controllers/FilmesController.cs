using APITeste.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APITeste.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilmesController : ControllerBase
    {
        private static List<FIlmes> _filmes = new();
        private static int _proximoId = 1;

        // get
        [HttpGet]
        public IActionResult ListarTodos()
        {
            return Ok(_filmes);
        }

        // post
        [HttpPost]
        public IActionResult Inserir([FromBody] FIlmes filme)
        {
            filme.Id = _proximoId++;
            _filmes.Add(filme);
            return CreatedAtAction(nameof(ListarTodos), new { id = filme.Id }, filme);
        }

        // put
        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, [FromBody] FIlmes filmeAtualizado)
        {
            var existente = _filmes.FirstOrDefault(f => f.Id == id);
            if (existente == null) return NotFound();

            existente.Titulo = filmeAtualizado.Titulo;
            existente.Genero = filmeAtualizado.Genero;
            existente.AnoLancamento = filmeAtualizado.AnoLancamento;
            existente.Diretor = filmeAtualizado.Diretor;
            existente.Nota = filmeAtualizado.Nota;
            Console.WriteLine();
            return Ok(existente);
        }

        // delete
        [HttpDelete("{id}")]
        public IActionResult Remover(int id)
        {
            var filme = _filmes.FirstOrDefault(f => f.Id == id);
            if (filme == null) return NotFound();

            _filmes.Remove(filme);
            return NoContent();
        }
    }
}
