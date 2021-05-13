using System;
using System.Threading.Tasks;
using Crud_WebAPI.Data;
using Crud_WebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace Crud_WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfessorController : ControllerBase
    {
        private readonly IRepository _repo;
        public ProfessorController(IRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var result = _repo.GetAllProfessoresAsync(true);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex.Message}");
            }
        }

        [HttpGet("{ProfessorId}")]
        //Task serve para criar threading fazer metodos asincronos
        public async Task<IActionResult> GetByProfessorId(int ProfessorId)
        {
            try
            {
                var result = await _repo.GetProfessorAsyncById(ProfessorId, true);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex.Message}");
            }
        }

        [HttpGet("ByDisciplina/{disciplinaId}")]
        //Task serve para criar threading fazer metodos asincronos
        public async Task<IActionResult> GetByProfessoresAsyncByAlunoId(int alunoId)
        { 
            try
            {
                var result = await _repo.GetProfessoresAsyncByAlunoId(alunoId, true);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> post(Professor model)
        {
            try
            {
                //Pegnado o modelo que foi cadastrado no DataContext
                _repo.Add(model);

                //Verificando se conseguiu add tudo certo.
                if (await _repo.SaveChangesAsync())
                {
                    return Ok(model);
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex.Message}");
            }

            // Se n for nenhum erro, podemos lançar um badRequest.
            return BadRequest();
        }

        [HttpPut("{professorId}")]
        public async Task<IActionResult> put(int professorId, Professor model)
        {
            try
            {
                //Buscando os professores e passando false para buscar somente os dados dos professores.
                var professor = await _repo.GetProfessorAsyncById(professorId, false);
                if (professor == null) return NotFound();

                _repo.Update(model);

                //Verificando se conseguiu add tudo certo.
                if (await _repo.SaveChangesAsync())
                {
                    return Ok(model);
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex.Message}");
            }

            // Se n for nenhum erro, podemos lançar um badRequest.
            return BadRequest();
        }

        [HttpDelete("{professorId}")]
        public async Task<IActionResult> delete(int professorId)
        {
            try
            {

                var professor = await _repo.GetProfessorAsyncById(professorId, false);
                if (professor == null) return NotFound();

                _repo.Delete(professor);

                //Verificando se conseguiu add tudo certo.
                if (await _repo.SaveChangesAsync())
                {
                    return Ok("Usuário Deletado!");
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex.Message}");
            }

            // Se n for nenhum erro, podemos lançar um badRequest.
            return BadRequest();
        }
    }
}