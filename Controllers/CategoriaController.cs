using API_Farmacia.Infra;
using API_Farmacia.Models;
using API_Farmacia.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace API_Farmacia.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriaController : ControllerBase
    {
        private readonly Context _context; 
        private readonly IMemoryCache _cache;
        public CategoriaController(Context context, IMemoryCache cache)
        {
            _context = context;
            _cache = cache;

        }

        [HttpGet("Cache")]
        public ActionResult<string> GetCache()
        {
            var cacheEntry = _cache.GetOrCreate("MeuCacheKey", entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(10);
                entry.SetPriority(CacheItemPriority.High);

                return OperacaoDeLongaDuracao();
            });
            return cacheEntry;
        }

        private string OperacaoDeLongaDuracao()
        {
            Thread.Sleep(5000);
            return "Operação de longa duração concluída !";
        }

        [HttpGet]
        public ActionResult<List<CategoriaRemedio>> Get()
        {
            CategoriasDAO dao = new CategoriasDAO(_context);
            try
            {
                var cacheEntry = _cache.GetOrCreate("MeuCacheCategorias", entry =>
                {
                    entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(10);
                    entry.SetPriority(CacheItemPriority.High);

                    var lista = dao.Index();
                    return Ok(lista);
                });
                return cacheEntry;
            }
            catch (Exception ex) {

                return BadRequest("Erro");
            }
        }

        [HttpPost]
        public ActionResult<string> Post([FromBody] CategoriaRemedio item)
        {

            CategoriasDAO dao = new CategoriasDAO(_context); 
            try
            {
                dao.Insert(item);
                return Ok("Cadastrado com sucesso");
            }
            catch (Exception ex)
            {

                return BadRequest("Erro");
            }
        }

        [HttpPut]
        public ActionResult<string>  Put([FromBody] CategoriaRemedio item)
        {
            CategoriasDAO dao = new CategoriasDAO(_context);
            try
            {
                dao.Update(item);
                return Ok("Atualizado com sucesso");
            }
            catch (Exception ex)
            {

                return BadRequest("Erro");
            }
        }

        [HttpDelete("{ID}")]
        public ActionResult<string> Delete(int ID)
        {
            CategoriasDAO dao = new CategoriasDAO(_context);

            try
            {
                dao.Delete(ID);
                return Ok("Deletado com sucesso");
            }
            catch (Exception ex)
            {

                return BadRequest("Erro");
            }
        }
    }
}
