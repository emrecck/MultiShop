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
            Input.ImageUrl = string.IsNullOrWhiteSpace(Input.ImageUrl)
                ? "/online-shop-website-template/img/user.jpg"
                : Input.ImageUrl;
            Input.Rating = Math.Clamp(Input.Rating, 1, 5);
            Input.CreatedDate = DateTime.UtcNow;
            Input.Status = false;
            await _commentService.CreateCommentAsync(Input);

            if (!string.IsNullOrWhiteSpace(Input.ProductId))
            {
                return RedirectToPage("/ProductList/ProductDetail", null, new { id = Input.ProductId }, "tab-pane-3");
            }

            return RedirectToPage("/ProductList/ProductDetail");
        }
    }
}
