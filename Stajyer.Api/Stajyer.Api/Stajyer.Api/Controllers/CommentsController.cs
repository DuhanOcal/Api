using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stajyer.Api.Data.Context;
using Stajyer.Api.Data.Entities;
using Stajyer.Api.Model.Request.Comments;
using Stajyer.Api.Model.Request.Product;

namespace Stajyer.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly StajyerDbContext _context;

        public CommentsController(StajyerDbContext stajyerDbContext)
        {
            _context = stajyerDbContext;
        }

        /// <summary>
        /// bir nesne oluşturup nesneme conteximdeki tabloyu seçip içindeki tüm verileri listeleyip nesneye aktarıyorum
        /// nesneyi de ok dönerek kullanıcıya dönüyorum
        /// </summary>
        /// <returns></returns>

        [HttpGet("GetAllComments")]
        public async Task<IActionResult> GetAllComments()
        {

            var response = await _context.Product.ToListAsync();

            return Ok(response);
        }


        /// <summary>
        /// db contextime ilgili tabloyu seçip where ile şartımı verdim daha sonra select ile seçme işlemimi yapmak için requestimi çağırdım ve parametlerine aktarım yaptım
        /// kontrol işlemi yaptırıp kullanıcıya gerekli retun ü döndüm
        /// </summary>
        /// <param name="SellerProductId"></param>
        /// <returns></returns>

        [HttpGet("GetCommentsByCustomerId/{SellerProductId}")]
        public async Task<IActionResult> GetCommentsByCustomerId(Guid SellerProductId)
        {
            var response = await _context.Comments.Where(x => x.SellerProductId == SellerProductId).Select(x => new GetCommentBySellerProductIdRequest //comment tablosundan sellerproductıd ye göre istenilen alanları çektim
            {
                SellerName = x.SellerName,
                Explanation =x.Explanation,

            }).FirstOrDefaultAsync();

            if (response == null)
                return BadRequest("Satıcı Ürününe Göre Yorum Bulunamadı");

            return Ok(response);
        }

        /// <summary>
        /// entityimden tablomu nesne ile tanımlayarak çağırdım ve requestimdekileri tanımladığım nesneye aktardım
        /// değişiklikleri ekleyip kaydettim ve kullanıcıya ok döndüm
        /// </summary>
        /// <param name="addCommentsRequest"></param>
        /// <returns></returns>
        
        [HttpPost("AddComments")]
        public async Task<IActionResult> AddComment(AddCommentsRequest addCommentsRequest)
        {
            //var comment= await _context.Comments.SingleOrDefaultAsync(x=>x.CustomerName==addCommentsRequest.CustomerName); // 
            //if (comment != null)
            //    return BadRequest("Zaten Ürünle İlgili Böyle Bir Yorum Var");

            Comments comments=new Comments();
            comments.SellerName = addCommentsRequest.SellerName;
            comments.Explanation = addCommentsRequest.Explanation; 

           await _context.Comments.AddAsync(comments);
           await _context.SaveChangesAsync();

            return Ok("Yorumunuz Eklenmiştir");
        }

        /// <summary>
        /// bir nesneme db contextimden tablomu seçtim tek kayıt için single kullandım ve requestteki verimle lamdandaki veriyi eşleştirdim
        /// eğer boş geliyosa badrequest döndüm
        /// değilse ise güncelleme için gerekli alanları requestten çektiklerimi nesneme aktarıyorum
        /// db me update i gönderip değişiklikleri kaydedip kullanıcıya ok dnönüyorum
        /// </summary>
        /// <param name="updateCommentsRequest"></param>
        /// <returns></returns>

        [HttpPost("UpdateComment")]
        public async Task<IActionResult> UpdateComment(UpdateCommentsRequest updateCommentsRequest) 
        {
            var comment = await _context.Comments.SingleOrDefaultAsync(x=>x.SellerProductId==updateCommentsRequest.SellerProductId);
            if (comment == null)
                return BadRequest("Yorumunuz Bulunamadı");

            comment.Explanation = updateCommentsRequest.Explanation;
            comment.SellerName = updateCommentsRequest.SellerName;

            _context.Comments.Update(comment);
            await _context.SaveChangesAsync();

            return Ok("Güncellemeniz Başarılı");
        }

        /// <summary>
        /// public bir task tanımlayıp adını belirleyip içine requestimizi tanımlıyoruz 
        /// bir nesne oluşturup içine db contextimden tablomu seçerek şartlarımı yazarak içine aktarıyorum
        /// nesnemi kontrol ettiriyorum eğer ki boş dönüyosa badrequest dönüyorum
        /// değilse ise silmek için silinecek olan verinin tutulduğu nesneyi remove ederek siliyoruz
        /// değişiklikleri kaydedip kullanıcıya ok dönüyoruz
        /// </summary>
        /// <param name="deleteCommentRequest"></param>
        /// <returns></returns>


        [HttpDelete("DeleteComment")]//Todo Id Çevir Remove kullan 
        public async Task<IActionResult> DeleteComment(DeleteCommentRequest deleteCommentRequest)
        {
            var comment = await _context.Comments.SingleOrDefaultAsync(x => x.Id == deleteCommentRequest.Id);
            if (comment != null)
                return BadRequest("Yorumunuz Bulunamadı");

            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();

            return Ok("Silme İşleminiz Başarılı");
        }
    }
}
