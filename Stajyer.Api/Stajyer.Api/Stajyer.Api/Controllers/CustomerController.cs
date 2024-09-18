using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stajyer.Api.Data.Context;
using Stajyer.Api.Data.Entities;
using Stajyer.Api.Model.Request.Customer;
using Stajyer.Api.Model.Request.Seller;
using Stajyer.Api.Model.Response.Customer;
using Stajyer.Api.Model.Response.Seller;

namespace Stajyer.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly StajyerDbContext _context;

        public CustomerController(StajyerDbContext stajyerDbContext)
        {
            _context = stajyerDbContext;
        }


        [HttpGet("GetAllCustomer")]
        public async Task<IActionResult> GetAllCustomer()
        {
            var response = await _context.Customer.ToListAsync();
            return Ok(response);
        }

        //Müşteri Ekleme 
        /// <summary>
        /// customerdb adında bir nesne oluşturdum ve bu nesne dbcontextimden gerekli tablomu seçerek gerekli şartı yazıp (requestteki mail ile lamdamdaki mail eşit) nesneme aktardım
        /// nesnemi kontrol ettiriyorum(boş değilse bad request dön) (boş değilse: eşleşirse içine gerekli bir veri geleceğinden bu şartı uygulamamız gerek)
        /// değilse kısmında entity mi customer nesnesi ile tanımlayarak tekrardan çağırıyorum
        /// eklemek istediklerim verileri requestimden yeni oluşturulan nesneme aktarıyorum
        /// vverilerimi dbcontexime gerekli tabloma ekliyorum
        /// değişiklikleri kaydederek kullanıcıya ok mesajı dönüyorum
        /// </summary>
        /// <param name="addCustomerRequest"></param>
        /// <returns></returns>

        [HttpPost("AddCustomer")]
        public async Task<IActionResult> AddCustomer(AddCustomerRequest addCustomerRequest)
        {
            var customerdb = await _context.Customer.SingleOrDefaultAsync(x => x.Mail == addCustomerRequest.Mail);

            if (customerdb != null)
            {
                return BadRequest("Böyle Bir Kayıdınız Mevcuttur");
            }
            Customer customer = new Customer();
            customer.Name = addCustomerRequest.Name;
            customer.Surname = addCustomerRequest.Surname;
            customer.Mail = addCustomerRequest.Mail;
            customer.PhoneNumber = addCustomerRequest.PhoneNumber;
            customer.Password = addCustomerRequest.Password;

            await _context.Customer.AddAsync(customer);
            await _context.SaveChangesAsync();

            return Ok(" Kayıt İşleminiz Başarılı");
        }


        /// <summary>
        /// /// customer adında bir nesne oluşturdum ve bu nesne dbcontextimden  tablomu seçtim gerekli şartı yazıp (requestteki id ile lamdamdaki id eşit) nesneme aktardım
        /// nesnemi kontrol ettiriyorum(boş mu:boşsa kullanıcıya nofound dönecek)
        /// değilse kısmında requestimden verilerimi başta tanımlanan nesneme aktarıyorum
        /// dbcontextime bu değişliklikleri yapıyorum: Update(nesne adım(customer))
        /// değişiklikleri kaydederek kullanıcıya ok mesajı dönüyorum
        /// </summary>
        /// <param name="updateCustomerRequest"></param>
        /// <returns></returns>

        //Güncelleme --name,surname,password

        [HttpPost("UpdateCustomer")] //Todo Id ye çevir
        public async Task<IActionResult> UpdateCustomer(UpdateCustomerRequest updateCustomerRequest)
        {
            var customer = await _context.Customer.SingleOrDefaultAsync(x => x.Id == updateCustomerRequest.Id);
            if (customer == null)
            {
                return NotFound();
            }

            //boş değilse
            customer.Name = updateCustomerRequest.Name;
            customer.Surname = updateCustomerRequest.Surname;
            customer.Password = updateCustomerRequest.Password;
            customer.Mail = updateCustomerRequest.Mail; 
            customer.PhoneNumber= updateCustomerRequest.PhoneNumber;

            _context.Update(customer);
            await _context.SaveChangesAsync();

            return Ok("Güncelleme Başarılı!");

        }


        /// <summary>
        /// http servisimize /id ekleyip id ile giriş yapmak zorunlu(required) yapmış oluruz
        /// bir değişken tanımlayıp içine db contextimden tablomu seçerek where aşrtımı oluşturdum
        /// select ile seçim yapıp requestimi içinde çağırıyorum
        /// getirmek istediğim verileri requestimdeki lamdamdan (requestimdeki) aktarıyorum
        /// eğer ki boşsa kullanıcıya notfound(bulunamadı) dönüyorum
        /// değisle ise de kullanıcıya getirmek sitediklerini aktardığımız nesnemizi(en başta oluşturulan) döndürüyoruz
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [HttpGet("GetCustomer/{id}")] //Todo Formatı bu
        public async Task<IActionResult> GetCustomer(Guid id)
        {

            var customer = await _context.Customer.Where(x => x.Id == id).Select(x=> new GetCustomerResponse
            { 
                Name = x.Name,
                Surname = x.Surname,
                Mail=x.Mail,
                PhoneNumber=x.PhoneNumber
                    
            }).FirstOrDefaultAsync();

            if (customer == null)
                return NotFound();

            return Ok(customer);
        }
      
       
    }
}
