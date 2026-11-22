using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MultiShop.DtoLayer.CommentDtos;
using MultiShop.Web.Services.CommentServices;

namespace MultiShop.Web.Pages.ProductList
{
    public class AddCommentModel : PageModel
    {
        private readonly ICommentService _commentService;

        public AddCommentModel(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [BindProperty]
        public CreateCommentDto Input { get; set; } = new();

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            Input.ImageUrl = "test";
            Input.Rating = 1;
            Input.CreatedDate = DateTime.Parse(DateTime.Now.ToShortDateString());
            Input.Status = false;
            await _commentService.CreateCommentAsync(Input);
            return RedirectToPage("/Default/Index");
        }
    }
}
