using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stajyer.Api.Data.Context;
using Stajyer.Api.Data.Entities;
using Stajyer.Api.Model.Request.Order;
using Stajyer.Api.Model.Response;
using Stajyer.Api.Model.Response.Order;

namespace Stajyer.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly StajyerDbContext _context;

        public OrderController(StajyerDbContext stajyerDbContext)
        {
            _context = stajyerDbContext;
        }

        /// <summary>
        /// bir nesne oluşturup nesneme conteximdeki tabloyu seçip içindeki tüm verileri listeleyip nesneye aktarıyorum
        /// nesneyi de ok dönerek kullanıcıya dönüyorum
        /// </summary>
        /// <returns></returns>

        [HttpGet("GetAllOrder")]
        public async Task<IActionResult> GetAllOrder()
        {

            var response = await _context.Order.ToListAsync();

            return Ok(response);
        }


        [HttpPost("CreateOrder")]
        public async Task<IActionResult> CreateOrder(OrderRequest orderRequest)
        {
            var product = await _context.Product.AnyAsync(x => x.Id == orderRequest.ProductId);
            if (!product)
                return BadRequest("ilgili Ürün Bulunamadı!");

            var stok = await _context.SellerProduct.Where(x => x.Id == orderRequest.SellerProductId).FirstOrDefaultAsync();

            if (orderRequest.ProductQuantity > stok.Stock)
                return BadRequest("Ürünün Stoğundan Fazla Seçim Yapılamaz!");

            var pn = _context.Product.Where(x => x.Id == orderRequest.ProductId).FirstOrDefault();

            stok.Stock -= orderRequest.ProductQuantity;
            _context.SellerProduct.Update(stok);

            Order order = new Order()
            {
                CustomerId = orderRequest.CustomerId,
                SellerProductId = orderRequest.SellerProductId,
                CargoId = orderRequest.CargoId,
                AdressId = orderRequest.AdressId,
                ProductQuantity = orderRequest.ProductQuantity,
            };
            await _context.Order.AddAsync(order);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                orderId = order.Id,
                productName = pn.ProductType,
                message = "İşlem Başarılı"
            });

        }

        [HttpGet("GetOrderQuery")]

        public async Task<IActionResult> GetOrderQuery()
        {
            var query = _context.Product.Join(
                _context.Order,
                product => product.ProductType,
                order => order.ProductType,
                (product, order) => new
                {
                    product.CategoryId,
                    order.Id,
                }
                );
            return Ok(query);
        }
    }
}
