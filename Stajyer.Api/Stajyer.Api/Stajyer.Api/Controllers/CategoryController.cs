using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stajyer.Api.Data.Context;
using Stajyer.Api.Data.Entities;
using Stajyer.Api.Model.Request.Comments;
using Stajyer.Api.Model.Request.Product;
using Stajyer.Api.Model.Response.Seller;

namespace Stajyer.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly StajyerDbContext _context;

        public CategoryController(StajyerDbContext stajyerDbContext)
        {
            _context = stajyerDbContext;
        }

        /// <summary>
        /// bir nesne oluşturup nesneme conteximdeki tabloyu seçip içindeki tüm verileri listeleyip nesneye aktarıyorum
        /// nesneyi de ok dönerek kullanıcıya dönüyorum
        /// </summary>
        /// <returns></returns>

        [HttpGet("GeAlltCategory")]
        public async Task<IActionResult> GetAllCategory()
        {
            var response = await _context.Category.ToListAsync();
            return Ok(response);
        }

    }
}
