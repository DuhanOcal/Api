using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stajyer.Api.Data.Context;
using Stajyer.Api.Data.Entities;
using Stajyer.Api.Model.Request.Product;
using Stajyer.Api.Model.Request.SellerProduct;

namespace Stajyer.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SellerProductController : ControllerBase
    {
        private readonly StajyerDbContext _context;

        public SellerProductController(StajyerDbContext stajyerDbContext)
        {
            _context = stajyerDbContext;
        }

        /// <summary>
        /// bir nesne oluşturup nesneme conteximdeki tabloyu seçip içindeki tüm verileri listeleyip nesneye aktarıyorum
        /// nesneyi de ok dönerek kullanıcıya dönüyorum
        /// </summary>
        /// <returns></returns>

        [HttpGet("GetAllSellerProcuct")]

        public async Task<IActionResult> GetAllSellerProduct()
        {
            var response = await _context.SellerProduct.ToListAsync();

            return Ok(response);
            
        }

        /// <summary>
        /// bir nesne oluşturup Seller tablomuzdan belirtilen koşul aktarılır (Any)
        /// 
        /// eğer seller false dönerse kullanıcıya BadRequest döner
        /// 
        /// başka bir tablo için nesne oluşturulup belirtilen koşul aktarılır (Any)
        /// 
        /// eğer product false dönerse kullanıcıya BadRequest döner
        /// 
        /// hiçbiri olmazsa entity mize veri ekleme yaparız
        /// 
        /// entitymizi temsil eden _contex'e Add methodu ile entitymize verileri aktarırız
        /// 
        /// değişiklikleri kaydedip kullanıcıya "işlem tamamlandı" dönüyoruz
        /// 
        /// </summary>
        /// <param name="addSellerProductRequest"></param>
        /// <returns></returns>
        
        [HttpPost("AddSellerProduct")]
        
        public async Task<IActionResult> AddSellerProduct(AddSellerProductRequest addSellerProductRequest)
        {
            var seller = await _context.Seller.AnyAsync(x => x.Id == addSellerProductRequest.SellerId);
            if (!seller)
                return BadRequest("ilgili Satıcı Bulunamadı!");


            var product = await _context.Product.AnyAsync(x => x.Id == addSellerProductRequest.ProductId);
            if (!product)
                return BadRequest("ilgili Ürün Bulunamadı!");

            SellerProduct sellerProduct = new SellerProduct
            {
                SellerId = addSellerProductRequest.SellerId,
                ProductId = addSellerProductRequest.ProductId,
                Explanation = addSellerProductRequest.Explanation,
                Stock = addSellerProductRequest.Stock,
            };

            await _context.AddAsync(sellerProduct);
            await _context.SaveChangesAsync();

            return Ok("İşlem Başarılı");
        }

        /// <summary>
        /// response adında bir nesne oluşturdum ve nesneye dbcontextimdeki tabloyu seçerek tekli kayıt gönderdim(single kullanarak lambdamdaki değer ile requestimi eşitleek şart koydum)
        /// kontrol ettirerek (==null) eğer ki şart true dönerse kullanıcıya badrequest dönecek
        /// değisle gerekli alanları requstten response nesneme aktararak dbcontextimde update ediyorum
        /// değişiklikleri kaydederek kullanıcıya ok dönüyorum
        /// </summary>
        /// <param name="updateSellerProductRequest"></param>
        /// <returns></returns>

        [HttpPost("UpdateSellerProduct")]
        public async Task<IActionResult> UpdateSellerProduct(UpdateSellerProductRequest updateSellerProductRequest) 
        {
            var response =await _context.SellerProduct.SingleOrDefaultAsync(x=>x.Id==updateSellerProductRequest.Id);

            if (response == null)
            {
                return BadRequest("Hatalı Giriş Yaptıznız");
            }
            response.Explanation = updateSellerProductRequest.Explanation;
            response.Stock = updateSellerProductRequest.Stock;

            _context.Update(response);
            await _context.SaveChangesAsync();

            return Ok("Güncellendi");
        }

    }
}
