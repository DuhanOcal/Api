using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stajyer.Api.Data.Context;
using Stajyer.Api.Data.Entities;
using Stajyer.Api.Model.Request.Product;
using Stajyer.Api.Model.Request.Seller;
using Stajyer.Api.Model.Response.Product;

namespace Stajyer.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly StajyerDbContext _context;

        public ProductController(StajyerDbContext stajyerDbContext)
        {
            _context = stajyerDbContext;
        }


        /// <summary>
        /// product adında bir nesne oluşturdum ve nesneye dbcontextimdeki tabloyu seçerek tekli kayıt gönderdim(single kullanarak lambdamdaki değer ile requestimi eşitleek şart koydum)
        /// nesnemi sorguluyorum boş değilse:bir değer gelecek (eşitlik durumundan) ve kullanıcıya badrequest dönücez (mevcuttur)
        /// entitymi nesne tanımlayarak tekrardan burda oluşturuyorum ve entity nesneme requestimdeki verileri aktarıyorum
        /// entity nesnemi contextime add ile ekliyorum ve değişiklikleri kaydediyorum
        /// kullanıcıya ok dönüyorum
        /// </summary>
        /// <param name="addProductRequest"></param>
        /// <returns></returns>
        /// 
        //Ürün Ekleme

        [HttpPost("AddProduct")]
        public async Task<IActionResult> AddProduct(AddProductRequest addProductRequest)
        {
            var product = await _context.Product.SingleOrDefaultAsync(x => x.SeriesNumber == addProductRequest.SeriesNumber);
            if (product != null)      
                return Ok("Kaydınız Mevcuttur Lütfen Başka Seri Numarası ile Deneyiniz");
            
            Product productadd = new Product();
            productadd.SeriesNumber = addProductRequest.SeriesNumber;
            productadd.BarcodeNumber = addProductRequest.BarcodeNumber;
            productadd.Weight = addProductRequest.Weight;
            productadd.ProductType = addProductRequest.ProductType;


            await _context.Product.AddAsync(productadd);
            await _context.SaveChangesAsync();

            return Ok("işlem Başarılı");
        }

        /// <summary>
        /// product adında bir nesne oluşturdum ve nesneye dbcontextimdeki tabloyu seçerek tekli kayıt gönderdim(single kullanarak lambdamdaki değer ile requestimi eşitleek şart koydum)
        /// kontrol ettirerek (==null) eğer ki şart true dönerse kullanıcıya badrequest dönecek
        /// değisle gerekli alanları requstten product nesneme aktararak dbcontextimde update ediyorum
        /// değişiklikleri kaydederek kullanıcıya ok dönüyorum
        /// </summary>
        /// <param name="updateProductRequest"></param>
        /// <returns></returns>

        //Ürün Güncelleştirme

        [HttpPost("UpdateProduct")] // Id ile yap
        public async Task<IActionResult> UpdateProduct(UpdateProductRequest updateProductRequest)
        {
            var product = await _context.Product.SingleOrDefaultAsync(x => x.Id == updateProductRequest.Id);

            if (product == null)          
                return BadRequest("Ürün Bulunamadı");
            
            product.SeriesNumber = updateProductRequest.SeriesNumber;
            product.BarcodeNumber = updateProductRequest.BarcodeNumber;
            product.Weight = updateProductRequest.Weight;
            product.ProductType = updateProductRequest.ProductType;

            _context.Update(product);
            await _context.SaveChangesAsync();

            return Ok("İşlem Başarılı!");
        }

        /// <summary>
        /// http servisimde {id} kullanarak orayı zorunlu(required) olarak ayarlıyorum
        /// product adında bir nesne oluşturdum ve nesneye dbcontextimdeki tabloyu seçerek tekli kayıt gönderdim(where kullanarak lambdamdaki değer ile requestimi eşitleek şart koydum)
        /// select kullanımımda repsonseumu oluşturarak where şartımdaki lamda değerlerimi repsonsuma aktarıyorum
        /// eğer ki product nesnem null ise kullanıcıya badrequest dönecek
        /// değilse kullanıcıya product nesnemi döndürerek ona istediği değerleri göstermiş olucam
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        //Ürün Listeleme
        [HttpGet("GetProduct/{id}")] 
        public async Task<IActionResult> GetProduct(Guid id)
        {
            var product = await _context.Product.Where(x => x.Id == id).Select(x => new GetProductResponse
            {

                SeriesNumber = x.SeriesNumber,
                BarcodeNumber = x.BarcodeNumber,
                Weight = x.Weight,
                ProductType = x.ProductType

            }).FirstOrDefaultAsync();

           if(product == null)
                return BadRequest("Kullanıcı Bulunamadı");

           return Ok(product);

        }


        /// <summary>
        /// http servisimde {CategoryId} kullanarak orayı zorunlu(required) giriş yaptım
        /// response adında bir nesne oluşturdum ve nesneye dbcontextimdeki tabloyu seçerek tekli kayıt gönderdim(where kullanarak lambdamdaki değer ile requestimi eşitleek şart koydum)
        /// select kullanımımda repsonseumu oluşturarak where şartımdaki lamda değerlerimi repsonsuma aktarıyorum
        /// eğer ki product nesnem null ise kullanıcıya badrequest dönecek
        /// değilse kullanıcıya product nesnemi döndürerek ona istediği değerleri göstermiş olucam
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [HttpGet("GetProductsByCategoryId/{CategoryId}")]
        public async Task<IActionResult> GetProductsByCustomerId(Guid CategoryId) 
        {
            var response = await _context.Product.Where(x => x.CategoryId == CategoryId).Select(x => new GetProductByCatIdRequest
            {
                SeriesNumber = x.SeriesNumber,
                BarcodeNumber = x.BarcodeNumber,
                Weight = x.Weight,
                ProductType = x.ProductType

            }).FirstOrDefaultAsync();

            if (response == null)
                return BadRequest("Categorye Göre Ürün Bulunamadı");

            return Ok(response);
        }

    }
}
