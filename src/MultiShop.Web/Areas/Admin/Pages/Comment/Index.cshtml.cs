using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MultiShop.DtoLayer.CommentDtos;
using MultiShop.Web.Services.CommentServices;

namespace MultiShop.Web.Areas.Admin.Pages.Comment
{
    [AllowAnonymous]
    public class IndexModel : PageModel
    {
        private readonly ICommentService _commentService;

        public IndexModel(ICommentService commentService)
        {
            _commentService = commentService;
        }

        public List<ResultCommentDto> Items { get; set; } = new();

        public async Task OnGetAsync()
        {
            ViewData["v1"] = "Ana Sayfa";
            ViewData["v2"] = "Yorumlar";
            ViewData["v3"] = "Yorum Listesi";
            ViewData["v0"] = "Yorum İşlemleri";
            Items = await _commentService.GetAllCommentAsync();
        }
    }
}
