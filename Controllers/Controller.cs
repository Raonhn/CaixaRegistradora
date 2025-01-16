using Microsoft.AspNetCore.Mvc;
using CaixaRegistradora.Classes;
using CaixaRegistradora.Services;
using CaixaRegistradora.Errors;

namespace CaixaRegistradora.Controllers{
    [ApiController, Route("/")]
    public class Controller: ControllerBase{
        private readonly Service service;

        public Controller(Service service){
            this.service = service;
        }

        [HttpPost("add")]
        public IActionResult AddNota([FromBody] Nota nota){
            try{
                return Created($"Caixa", new { Message = "Dinheiro adicionado com sucesso", Dinheiro = service.AddNota(nota) });
            } catch(HttpException ex){
                return BadRequest(ex);
            }
        }

        [HttpGet("vercaixa")]
        public IActionResult VerCaixa(){
            try{
                return Ok(new { Message = "Visualizando caixa", Dinheiro = service.VerCaixa()});
            } catch {
                return NoContent();
            }
        }

        [HttpPost("transacao")]
        public IActionResult Transacao([FromBody] Transacao transacao){
            try{
                return Ok(new { Message = "Transacao realizada com sucesso", Dinheiro = service.calculaTroco(transacao)});
            } catch(HttpException ex){
                return StatusCode(ex.Code, ex);
            }
        }
    }
}