using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MultiShop.DtoLayer.CommentDtos;
using MultiShop.Web.Services.CommentServices;

namespace MultiShop.Web.Areas.Admin.Pages.Comment
{
    [AllowAnonymous]
    public class UpdateCommentModel : PageModel
    {
        private readonly ICommentService _commentService;

        public UpdateCommentModel(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [BindProperty]
        public UpdateCommentDto Input { get; set; } = new();

        public async Task OnGetAsync(string id)
        {
            ViewData["v1"] = "Ana Sayfa";
            ViewData["v2"] = "Yorumlar";
            ViewData["v3"] = "Yorum Listesi";
            ViewData["v0"] = "Yorum İşlemleri";
            Input = await _commentService.GetByIdCommentAsync(id);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            Input.Status = true;
            await _commentService.UpdateCommentAsync(Input);
            return RedirectToPage("/Comment/Index", new { area = "Admin" });
        }
    }
}
