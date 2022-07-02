using Microsoft.AspNetCore.Mvc;
using QuickBuy.Dominio.Contratos;
using QuickBuy.Dominio.Entidades;
using System;

namespace QuickBuy.Web.Controllers
{
    [Route("api/[Controller]")]
    public class PedidoController : Controller
    {
        private readonly IPedidoRepositorio _pedidoRepository;
        public PedidoController(IPedidoRepositorio pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        }
        [HttpPost]
        public IActionResult Post([FromBody] Pedido pedido) {
            try
            {
                _pedidoRepository.Adicionar(pedido);
                return Ok(pedido.Id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

}
