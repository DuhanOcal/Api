using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stajyer.Api.Data.Context;
using Stajyer.Api.Data.Entities;
using Stajyer.Api.Model.Request.Adress;
using Stajyer.Api.Model.Response.Order;

namespace Stajyer.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdressController : ControllerBase
    {
        private readonly StajyerDbContext _context;

        public AdressController(StajyerDbContext stajyerDbContext)
        {
            _context = stajyerDbContext;
        }

        /// <summary>
        /// entity mizdeki istenilen tablodaki bütün verileri dönmek için bir nesne tanımlayıp içine list yapıp aktarılıyor
        /// aktarılan nesneyi ise ok olarak dönüyoruz
        /// </summary>
        /// <returns></returns>

        [HttpGet("GeAlltAdress")]
        public async Task<IActionResult> GetAllAdress()
        {
            var response = await _context.Adress.ToListAsync();
            return Ok(response);
        }

        /// <summary>
        /// Id ye Göre Veri Çağırma
        /// http servisimize id girişi ekliyoruz girişte GİRİLMESİ ZORUNLU(required) oluyor
        /// bir nesne oluşturum ve where sorgusu ile id eşitleme yaptım
        /// eşitlenen verimi seçip requestime aktarıp nesnemi döndürüyorum
        /// </summary>
        /// <param ></param>
        /// <returns></returns>

        [HttpGet("GetAdressByCustomerId/{CustomerId}")]
        public async Task<IActionResult> GetAdressByCustomerId(Guid CustomerId)
        {
            var getAdress = await _context.Adress.Where(x => x.CustomerId == CustomerId).Select(x => new GetAdressByCustomerIdRequest
            {
                AdressName = x.AdressName,

            }).FirstOrDefaultAsync();

            return Ok(getAdress);
        }


        /// <summary>
        /// Yeni Adres Ekleme 
        /// adres isimli nesne tanımladım = contextimden tablomu seçtim tek kayıt seçmek için single kullandım requestimle lambamdaki(entity) veriler eşleşiyosa bad request dönüyorum
        /// Mevcut değilse Adress entitymi referans alarak yeni bir nesne oluşturuyorum(bellek bunu tutuyor)
        /// eklenecek olan verilerimi requestten yeni tanımlanan nesneme aktarıyorum
        /// add methodunu kullanarak bellekte tutulan yeni adres nesneme ekleme işlemi yapıp değişiklikleri kaydediyorum ve ok dönüyorum
        /// </summary>
        /// <param name="addAdressRequest"></param>
        /// <returns></returns>

        [HttpPost("AddAdress")]
        public async Task<IActionResult> AddAdress(AddAdressRequest addAdressRequest)
        {
            var adress = await _context.Adress.SingleOrDefaultAsync(x => x.Id == addAdressRequest.Id);
            if (adress != null)
                return BadRequest("Böyle Bir Adres Kaydınız Mevcuttur!");

            Adress adres = new Adress();
            adres.AdressName = addAdressRequest.AdressName;

            await _context.AddAsync(adres);
            await _context.SaveChangesAsync();

            return Ok("Adres Ekleme İşleminiz Başarılı!");
        }


        /// <summary>
        /// Adres Güncelleme
        /// adresdb nesneme contextimdeki adres tablomu seçiyorum.Tek kayıt kullanmak için single kullandım şartlarım eşleşiyosa eğer kullanıcıma badrequest döndüm
        /// değilse requestimden nesneme güncellenecek veriyi aktraıp db me kayıt ettirip kullanıcıma ok dönüyorum
        /// </summary>
        /// <param name="updateAdressRequest"></param>
        /// <returns></returns>

        [HttpPost("UpdateAdress")]
        public async Task<IActionResult> UpdateAdress(UpdateAdressRequest updateAdressRequest)
        {
            var adressdb = await _context.Adress.SingleOrDefaultAsync(x => x.Id == updateAdressRequest.Id);
            if (adressdb == null)
                return BadRequest("Adresiniz Bulunamadı");

            adressdb.AdressName = updateAdressRequest.AdressName;
            _context.Update(adressdb);
            _context.SaveChanges();

            return Ok("Güncelleme İşleminiz Başarılı");
        }


        [HttpPost("GetAdressQuery")]
        public async Task<IActionResult> GetAdressQuery()
        {

            var result = await _context.Order.Join(
                  _context.Cargo,
                  order => order.CargoId,
                  cargo => cargo.Id,
                  (order, cargo) => new
                  {
                      order = order,
                      cargo = cargo
                  }).Join(_context.Adress,

                  order => order.order.AdressId,
                  adress => adress.Id,
                  (order, adress) => new
                  {
                      order = order,
                      adress = adress

                  }).Join(_context.Customer,
                  order => order.order.order.CustomerId,
                  customer => customer.Id,
                  (order, customer) => new OrderJoinResponse
                  {
                      Name = customer.Name,
                      Surname = customer.Surname,
                      Telephone = customer.PhoneNumber,
                      Mail = customer.Mail,
                      CargoCompanyName = order.order.cargo.CargoCompanyName,
                      AdressName = order.adress.AdressName,                    
                  }).ToListAsync();

            return Ok(result);

        }
    }
}
