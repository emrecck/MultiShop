using Microsoft.AspNetCore.Mvc.RazorPages;
using MultiShop.Web.Services.CommentServices;
using MultiShop.Web.Services.StatisticServices.CatalogStatisticServices;
using MultiShop.Web.Services.StatisticServices.DiscountStatisticServices;
using MultiShop.Web.Services.StatisticServices.MessageStatisticServices;
using MultiShop.Web.Services.StatisticServices.UserStatisticServices;

namespace MultiShop.Web.Areas.Admin.Pages.Statistic
{
    public class IndexModel : PageModel
    {
        private readonly ICatalogStatisticService _catalogStatisticService;
        private readonly IUserStatisticService _userStatisticService;
        private readonly ICommentService _commentService;
        private readonly IDiscountStatisticService _discountStatisticService;
        private readonly IMessageStatisticService _messageStatisticService;

        public IndexModel(
            ICatalogStatisticService catalogStatisticService,
            IUserStatisticService userStatisticService,
            ICommentService commentService,
            IDiscountStatisticService discountStatisticService,
            IMessageStatisticService messageStatisticService)
        {
            _catalogStatisticService = catalogStatisticService;
            _userStatisticService = userStatisticService;
            _commentService = commentService;
            _discountStatisticService = discountStatisticService;
            _messageStatisticService = messageStatisticService;
        }

        public async Task OnGetAsync()
        {
            ViewData["getBrandCount"] = await _catalogStatisticService.GetBrandCount();
            ViewData["getProductCount"] = await _catalogStatisticService.GetProductCount();
            ViewData["getCategoryCount"] = await _catalogStatisticService.GetCategoryCount();
            ViewData["getMaxPriceProductName"] = await _catalogStatisticService.GetMaxPriceProductName();
            ViewData["getMinPriceProductName"] = await _catalogStatisticService.GetMinPriceProductName();

            ViewData["getUserCount"] = await _userStatisticService.GetUsercount();

            ViewData["getTotalCommentCount"] = await _commentService.GetTotalCommentCount();
            ViewData["getActiveCommentCount"] = await _commentService.GetActiveCommentCount();
            ViewData["getPassiveCommentCount"] = await _commentService.GetPAssiveCommentCount();

            ViewData["getDiscountCouponCount"] = await _discountStatisticService.GetDiscountCouponCount();
            ViewData["getMessageTotalCount"] = await _messageStatisticService.GetTotalMessageCount();
        }
    }
}
