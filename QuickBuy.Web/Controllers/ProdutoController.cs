using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuickBuy.Dominio.Contratos;
using QuickBuy.Dominio.Entidades;
using System;
using System.IO;
using System.Linq;

namespace QuickBuy.Web.Controllers
{
    [Route("api/[Controller]")]
    public class ProdutoController : Controller
    {
        private readonly IProdutoRepositorio _produtoRepositorio;
        private IHttpContextAccessor _httpContextAcessor;
        private IHostingEnvironment _hostingEnviroment;

        public ProdutoController(IProdutoRepositorio produtoRepositorio, 
                                 IHttpContextAccessor httpContextAcessor,
                                 IHostingEnvironment hostingEnviroment)
        {
            _produtoRepositorio = produtoRepositorio;
            _httpContextAcessor = httpContextAcessor;
            _hostingEnviroment = hostingEnviroment;
        }

        [HttpGet]
        public IActionResult Get() 
        {
            try
            {
                return Json(_produtoRepositorio.ObterTodos());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
                
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody]Produto produto)
        {
            try
            {
                produto.Validate();

                if (!produto.EhValido)
                    return BadRequest(produto.ObterMensagensValidacao());

                if (produto.Id > 0)
                    _produtoRepositorio.Atualizar(produto);
                else
                    _produtoRepositorio.Adicionar(produto);
                
                return Created("api/produto", produto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());

            }
        }
        [HttpPost("Deletar")]
        public IActionResult Deletar([FromBody] Produto produto)
        {
            try
            {
                //produto recebido frombody, deve ter a propriedade Id > 0
                _produtoRepositorio.Remover(produto);
                return Json(_produtoRepositorio.ObterTodos());
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPost("EnviarArquivo")]
        public IActionResult EnviarArquivo() {
            try
            {
                var formFile = _httpContextAcessor.HttpContext.Request.Form.Files["arquivoEnviado"];
                var nomeArquivo = formFile.FileName;
                var extensao = nomeArquivo.Split(".").Last();
                string novoNomeArquivo = GerarNovoNomeArquivo(nomeArquivo, extensao);
                var pastaArquivos = _hostingEnviroment.WebRootPath + "\\arquivos\\";
                var nomeCompleto = pastaArquivos + novoNomeArquivo;

                using (var streamArquivo = new FileStream(nomeCompleto, FileMode.Create))
                {
                    formFile.CopyTo(streamArquivo);
                }

                return Json(novoNomeArquivo);

            }
            catch (Exception ex)
            {

                return BadRequest(ex.ToString());
            }
        }

        private static string GerarNovoNomeArquivo(string nomeArquivo, string extensao)
        {
            var arrayNomeCompacto = Path.GetFileNameWithoutExtension(nomeArquivo).Take(10).ToArray();
            var novoNomeArquivo = new string(arrayNomeCompacto).Replace(" ", "-");
            novoNomeArquivo = $"{novoNomeArquivo}_{DateTime.Now.Year}{DateTime.Now.Month}{DateTime.Now.Day}{DateTime.Now.Minute}{DateTime.Now.Second}.{extensao}";
            return novoNomeArquivo;
        }
    }
}
