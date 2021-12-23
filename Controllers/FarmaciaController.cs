using API_Farmacia.Infra;
using API_Farmacia.Models;
using API_Farmacia.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Farmacia.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FarmaciaController : ControllerBase
    {
        private readonly Context _context;
        private readonly IMemoryCache _cache;

        public FarmaciaController(Context context, IMemoryCache cache)
        {
            _context = context;
            _cache = cache;

        }

        [HttpGet]
        public ActionResult<List<Farmacia>> Get()
        {
            FarmaciaDAO dao = new FarmaciaDAO(_context);
            try
            {
                var lista = dao.Index();
                return Ok(lista);
            }
            catch (Exception ex) {

                return BadRequest("Erro");
            }
        }

        [HttpPost]
        public ActionResult<string> Post([FromBody] Farmacia item)
        {

            FarmaciaDAO dao = new FarmaciaDAO(_context); 
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
        public ActionResult<string>  Put([FromBody] Farmacia item)
        {
            FarmaciaDAO dao = new FarmaciaDAO(_context);
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
            FarmaciaDAO dao = new FarmaciaDAO(_context);

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
