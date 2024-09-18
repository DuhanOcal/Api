using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql.TypeMapping;
using Stajyer.Api.Data.Context;
using Stajyer.Api.Data.Entities;
using Stajyer.Api.Model.Request.Login;
using Stajyer.Api.Model.Request.Password;
using Stajyer.Api.Model.Response.Account;
using Stajyer.Api.Model.Response.Password;

namespace Stajyer.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly StajyerDbContext _context;

        public AccountController(StajyerDbContext stajyerDbContext)
        {
            _context = stajyerDbContext;
        }

        /// <summary>
        /// Hangi tür kullanıcı girişi olması için ayrı response oluşturdum. Her respons farklı kullanıcıtı tanımlar
        /// hangi kullanıcı giriş yaptı diye switch case kulanrak kontrol edilir ona göre kod satırına girer
        /// db bizdeki şartla requestimizdeki şart eşitse responsumuzdan veri çekişi yapıyoruz
        /// en sonda responsumuzu ok olarak dönüyoruz
        /// </summary>
        /// <param name="loginAccountRequest"></param>
        /// <returns></returns>

        [HttpPost("LoginAccount")]//Type 1 ise Customer  -- 2 ise Seller
        public async Task<IActionResult> LoginAccount(LoginAccountRequest loginAccountRequest)
        {
            LoginResponse loginResponse = new LoginResponse();
            LoginUserResponse loginUserResponse = new LoginUserResponse();

            switch (loginAccountRequest.UserType)
            {
                case "1":

                    loginUserResponse = await _context.Customer.Where(x => x.Mail == loginAccountRequest.Mail).Select(x => new LoginUserResponse
                    {
                        Name = x.Name,
                        Surname = x.Surname,
                        Id = x.Id,
                        Mail = x.Mail,
                        Phone = x.PhoneNumber,
                        Password = x.Password,
                        UserType=loginUserResponse.UserType,
                        
                    }).SingleOrDefaultAsync();
                    break;

                case"2":
                    loginUserResponse = await _context.Seller.Where(x => x.Mail == loginAccountRequest.Mail).Select(x => new LoginUserResponse
                    {
                        Name = x.Name,
                        Adress = x.Adress,
                        Id = x.Id,
                        Mail = x.Mail,
                        Phone = x.PhoneNumber,
                        Password = x.Password,
                        UserType = loginUserResponse.UserType,
                    }).SingleOrDefaultAsync();

                    break;

                default:
                    return BadRequest("İşlem Tipi Bulunamadı");
            }

            if (loginUserResponse == null)

                return BadRequest("Girdiğiniz Mail adresi bulunamadı!");


            if (loginUserResponse.Password != loginAccountRequest.Password)

                return BadRequest("Girdiğiniz şifre hatalı !");

            loginResponse.Id = loginUserResponse.Id;
            loginResponse.Name = loginUserResponse.Name;
            loginResponse.Surname = loginUserResponse.Surname;
            loginResponse.Mail = loginUserResponse.Mail;
            loginResponse.Phone = loginUserResponse.Phone;
            loginResponse.Adress = loginUserResponse.Adress;
            loginResponse.UserType = loginAccountRequest.UserType;

          //  return Ok("Giriş Başarılı");
            return Ok(new
            {
                message = "Hoşgeldiniz",
               loginResponse.Name,
            });
        }


        /// <summary>
        /// müşteri ve satıcı entitylerimi nesnelerle tanımlayarak belleğimde tutuyorum
        /// müşteri mi satıcı mı diye kontrol edip girişe göre şifre kontrolü yapıyorum
        /// eğer farklı bir değer girilirse yanlış işlem yapıldı diye kullanıcıya dönüyorum
        /// şifreler önceden bellekte tuttuğum nesnelere aktrdım
        /// tanımlanan nesnedeki şifre eski şifre ile eşleşiyosa badrequest döndürdüm
        /// eğer şifreler eşleşmiyosa müşteri mi satıcı mı olduğu şartına göre şifre güncelleştirilmesi yapıp entityde güncelleme kaydettim
        /// </summary>
        /// <param name="changePasswordRequest"></param>
        /// <returns></returns>

        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword(ChangePasswordRequest changePasswordRequest)
        {
            ChangePasswordResponse changePasswordResponse = new ChangePasswordResponse();
            Customer customer = new Customer();
            Seller seller = new Seller();

            switch (changePasswordRequest.UserType)
            {
                case 1:
                    customer = await _context.Customer.Where(x => x.Id == changePasswordRequest.Id).SingleOrDefaultAsync();
                    break;

                case 2:
                    seller = await _context.Seller.Where(x => x.Id == changePasswordRequest.Id).SingleOrDefaultAsync();
                    break;

                default:
                    return BadRequest("İşlem Tipi Bulunamadı");
            }
            if (changePasswordRequest.UserType == 1)
            {

                if (customer.Password != changePasswordRequest.Password)
                {
                    customer.Password = changePasswordRequest.Password;
                    _context.Customer.Update(customer);
                    _context.SaveChanges();
                    return Ok("Güncelleme Başarılı");
                }

                return BadRequest("Yeni Şifreniz Eski Şifre ile Aynı Olamaz");
            }

            if (changePasswordRequest.UserType == 2)
            {
                if (seller.Password != changePasswordRequest.Password)
                {
                    seller.Password = changePasswordRequest.Password;
                    _context.Seller.Update(seller);
                    _context.SaveChanges();
                    return Ok("Güncelleme Başarılı");
                }
                return BadRequest("Şifreniz Eski Şifre ile Aynı Olamaz");
            }

            return BadRequest("Gerçekleşmedi");
        }
    }
}

