using System;
using System.Threading.Tasks;
using Crud_WebAPI.Data;
using Crud_WebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace Crud_WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlunoController : ControllerBase
    {

        //Para n ter que chamar toda hora this.repo, vc pode colocar _repo
        private readonly IRepository _repo;
        public AlunoController(IRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]

        //Task serve para criar threading fazer metodos asincronos
        public async Task<IActionResult> Get()
        {
            try
            {
                //Se deixar marcado como true, ele faz os Joins da tabela, deixando marcado false ele retorna apenas os alunos.
                var result = await _repo.GetAllAlunosAsync(true);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex.Message}");
            }
        }


        [HttpGet("{AlunoId}")]
        //Task serve para criar threading fazer metodos asincronos
        public async Task<IActionResult> GetByAlunoId(int AlunoId)
        {
            try
            {
                var result = await _repo.GetAlunoAsyncById(AlunoId, true);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex.Message}");
            }
        }

        [HttpGet("ByDisciplina/{disciplinaId}")]
        //Task serve para criar threading fazer metodos asincronos
        public async Task<IActionResult> GetByDisciplinaId(int disciplinaId)
        {
            try
            {
                var result = await _repo.GetAlunosAsyncByDisciplinaId(disciplinaId, true);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> post(Aluno model)
        {
            try
            {
                //Pegnado o modelo que foi cadastrado no DataContext
               _repo.Add(model);

               //Verificando se conseguiu add tudo certo.
               if(await _repo.SaveChangesAsync())
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

        [HttpPut("{alunoId}")]
        public async Task<IActionResult> put(int alunoId, Aluno model)
        {
            try
            {
                //Buscando os alunos e passando false para buscar somente os dados dos alunos.
               var aluno = await _repo.GetAlunoAsyncById(alunoId, false);
               if(aluno == null)  return NotFound();

               _repo.Update(model);

               //Verificando se conseguiu add tudo certo.
               if(await _repo.SaveChangesAsync())
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

        [HttpDelete("{alunoId}")]
        public async Task<IActionResult> delete(int alunoId)
        {
            try
            {
        
               var aluno = await _repo.GetAlunoAsyncById(alunoId, false);
               if(aluno == null)  return NotFound();

               _repo.Delete(aluno);

               //Verificando se conseguiu add tudo certo.
               if(await _repo.SaveChangesAsync())
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