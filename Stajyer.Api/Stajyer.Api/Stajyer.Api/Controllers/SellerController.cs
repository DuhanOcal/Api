using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stajyer.Api.Data.Context;
using Stajyer.Api.Data.Entities;
using Stajyer.Api.Model.Request.Seller;
using Stajyer.Api.Model.Response.Customer;
using Stajyer.Api.Model.Response.Seller;

namespace Stajyer.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SellerController : ControllerBase
    {
        private readonly StajyerDbContext _context;

        public SellerController(StajyerDbContext stajyerDbContext)
        {
            _context = stajyerDbContext;
        }

        [HttpGet("GetAllSeller")]
        public async Task<IActionResult> GetAllSeller()
        {
            var allseller= await _context.Seller.ToListAsync();
            return Ok(allseller);
        }

        /// <summary>
        /// sellerdb adında bir nesne oluşturdum ve nesneye dbcontextimdeki tabloyu seçerek tekli kayıt gönderdim(single kullanarak lambdamdaki değer ile requestimi eşitleek şart koydum)
        /// nesnemi sorguluyorum boş değilse:bir değer gelecek (eşitlik durumundan) ve kullanıcıya badrequest dönücez (mevcuttur)
        /// entitymi nesne tanımlayarak tekrardan burda oluşturuyorum ve entity nesneme requestimdeki verileri aktarıyorum
        /// entity nesnemi contextime add ile ekliyorum ve değişiklikleri kaydediyorum
        /// kullanıcıya ok dönüyorum
        /// </summary>
        /// <param name="addSellerRequest"></param>
        /// <returns></returns>
        /// 

        //Satıcı Ekleme
        [HttpPost("AddSeller")]
        public async Task<IActionResult> AddSeller(AddSellerRequest addSellerRequest)
        {
            var sellerdb = await _context.Seller.SingleOrDefaultAsync(x => x.Mail == addSellerRequest.Mail);
            //var sellerdb2 =await _context.Seller.Select(x => x.Mail == addSellerRequest.Mail).SingleOrDefaultAsync();
            //var sellerdb3 =await _context.Seller.Where(x => x.Mail == addSellerRequest.Mail).FirstOrDefaultAsync();
            // var sellerdb =await _context.Seller.Where(x => x.Mail == addSellerRequest.Mail).ToListAsync();

            if (sellerdb != null)
            {
                return BadRequest("Kaydınız Mevcuttur, Lütfen Başka Mail adresi ile deneyiniz");
            }

            Seller seller = new Seller();
            seller.Name = addSellerRequest.Name;
            seller.Adress = addSellerRequest.Adress;
            seller.PhoneNumber = addSellerRequest.PhoneNumber;
            seller.TaxNumber = addSellerRequest.TaxNumber;
            seller.Mail = addSellerRequest.Mail;
            seller.Password = addSellerRequest.Password;

            await _context.Seller.AddAsync(seller);
            await _context.SaveChangesAsync();

            return Ok("Kayıt işlemi Başarılı");
        }

        /// <summary>
        /// seller adında bir nesne oluşturdum ve nesneye dbcontextimdeki tabloyu seçerek tekli kayıt gönderdim(where kullanarak lambdamdaki değer ile requestimi eşitleek şart koydum)
        /// select kullanımımda repsonseumu oluşturarak where şartımdaki lamda değerlerimi repsonsuma aktarıyorum
        /// eğer ki seller nesnem null ise kullanıcıya badrequest dönecek
        /// değilse kullanıcıya seller nesnemi döndürerek ona istediği değerleri göstermiş olucam
        /// </summary>
        /// <param></param>
        /// <returns></returns>

        //Tüm hepsini Listele Methot

        [HttpGet("GetSeller")] //todo formatı
        public async Task<IActionResult> GetSeller(Guid Id)
        {
            var seller = await _context.Seller.Where(x => x.Id == Id).Select(x => new GetSellerResponse
            {
                Name = x.Name,
                TaxNumber = x.TaxNumber,
                Adress = x.Adress,
                Mail = x.Mail,
            }).FirstOrDefaultAsync();

            if (seller == null)
                return BadRequest();

            return Ok(seller);
        }

        /// <summary>
        /// seller nesnemi oluşturup içine cotextimdeki tabloyu seçerek ve tekli kayıt alıp(lamda id==request.id) nesneye aktarıyorum
        /// nesneyi kontrol ettirerek (seller(nesnem)==null ise: kullanıcıya badrequest döndürüyorum)
        /// değilse kısmında ise requestimdeki değerleri alıp baştaki nesneme aktarıyorum
        /// contextime update ile nesnemi yazarak update çekiyorum
        /// değişiklikleri kaydederek kullanıcıya ok dönüyorum
        /// </summary>
        /// <param name="updateSellerRequest"></param>
        /// <returns></returns>

        //Update --Name, Password, Adress

        [HttpPost("UpdateSeller")]
        public async Task<IActionResult> UpdateSeller(UpdateSellerRequest updateSellerRequest)
        {
            var seller = await _context.Seller.SingleOrDefaultAsync(x => x.Id == updateSellerRequest.Id);

            if (seller == null)   
                
                return Ok("Satıcı Bulunamadı");
            
            // Boş Değilse

            seller.Adress = updateSellerRequest.Address;
            seller.Password = updateSellerRequest.Password;
            seller.Name = updateSellerRequest.Name;

            _context.Update(seller);
            await _context.SaveChangesAsync();

            return Ok("İşlem Başarılı");
        }


    }
}
