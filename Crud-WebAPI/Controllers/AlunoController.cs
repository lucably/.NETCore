using System;
using System.Threading.Tasks;
using Crud_WebAPI.Data;
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
    }
}