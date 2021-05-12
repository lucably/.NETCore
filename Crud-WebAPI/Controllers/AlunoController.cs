using System;
using Microsoft.AspNetCore.Mvc;

namespace Crud_WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlunoController : ControllerBase
    {
         [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok("Lucas Aluno");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex.Message}");
            }
        }
    }
}