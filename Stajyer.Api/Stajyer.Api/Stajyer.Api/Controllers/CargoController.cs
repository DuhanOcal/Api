using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stajyer.Api.Data.Context;
using Stajyer.Api.Data.Entities;

namespace Stajyer.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CargoController : ControllerBase
    {
        private readonly StajyerDbContext _context;

        public CargoController(StajyerDbContext stajyerDbContext) 
        {
            _context = stajyerDbContext;
        }

        /// <summary>
        /// bir nesne oluşturup nesneme conteximdeki tabloyu seçip içindeki tüm verileri listeleyip nesneye aktarıyorum
        /// nesneyi de ok dönerek kullanıcıya dönüyorum
        /// </summary>
        /// <returns></returns>

        [HttpGet("GetAllCargo")]

        public async Task<IActionResult> GetAllCargo() 
        {
            var response =await _context.Cargo.ToListAsync();
            return Ok(response);
        }
    }
}
