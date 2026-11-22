using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MultiShop.Web.Services.CommentServices;

namespace MultiShop.Web.Areas.Admin.Pages.Comment
{
    [AllowAnonymous]
    public class DeleteCommentModel : PageModel
    {
        private readonly ICommentService _commentService;

        public DeleteCommentModel(ICommentService commentService)
        {
            _commentService = commentService;
        }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            await _commentService.DeleteCommentAsync(id);
            return RedirectToPage("/Comment/Index", new { area = "Admin" });
        }
    }
}
