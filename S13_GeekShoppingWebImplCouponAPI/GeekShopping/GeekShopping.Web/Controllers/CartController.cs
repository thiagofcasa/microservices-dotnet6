using GeekShopping.Web.Models;
using GeekShopping.Web.Services.IServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.Web.Controllers
{
    public class CartController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICartService _cartService;
        private readonly ICouponService _couponService;

        public CartController(IProductService productService, ICartService cartService, ICouponService couponService)
        {
            _productService = productService;
            _cartService = cartService;
            _couponService = couponService;
        }

        [Authorize]
        public async Task<IActionResult> CartIndex()
        {
            return View(await FindUserCart());
        }

        [HttpPost]
        [ActionName("ApplyCoupon")]
        public async Task<IActionResult> ApplyCoupon(CartViewModel model)
        {
            var token = await HttpContext.GetTokenAsync("access_token");
            var userId = User.Claims.Where(u => u.Type == "sub")?.FirstOrDefault()?.Value;
            var response = await _cartService.ApplyCoupon(model, token);

            if (response)
                return RedirectToAction(nameof(CartIndex));

            return View(await FindUserCart());
        }

        [HttpPost]
        [ActionName("RemoveCoupon")]
        public async Task<IActionResult> RemoveCoupon()
        {
            var token = await HttpContext.GetTokenAsync("access_token");
            var userId = User.Claims.Where(u => u.Type == "sub")?.FirstOrDefault()?.Value;

            var response = await _cartService.RemoveCoupon(userId, token);

            if (response)
                return RedirectToAction(nameof(CartIndex));

            return View(await FindUserCart());
        }


        public async Task<IActionResult> Remove(int id)
        {
            var token = await HttpContext.GetTokenAsync("access_token");
            var userId = User.Claims.Where(u => u.Type == "sub")?.FirstOrDefault()?.Value;

            var response = await _cartService.RemoveFromCart(id, token);

            if (response)
            {
                return RedirectToAction(nameof(CartIndex));
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Checkout()
        {
            return View(await FindUserCart());
        }


        [HttpPost]
        public async Task<IActionResult> Checkout(CartViewModel model)
        {
            var token = await HttpContext.GetTokenAsync("access_token");

            var response = await _cartService.Checkout(model.CartHeader, token);

            if (response != null)
            {
                //Redireciona para a Action Confirmation.
                return RedirectToAction(nameof(Confirmation));
            }
            else
            {
                return View(model);
            }
        }

        //Retorna a View sem nenhum parametro
        [HttpGet]
        public async Task<IActionResult> Confirmation()
        {
            return View();
        }


        private async Task<CartViewModel> FindUserCart()
            {
                var token = await HttpContext.GetTokenAsync("access_token");
                var userId = User.Claims.Where(u => u.Type == "sub")?.FirstOrDefault()?.Value;

                var response = await _cartService.FindCartByUserId(userId, token);

                if (response?.CartHeader != null)
                {
                    //Se o coupon não estiver nulo vai obter o coupon através do microserviço de coupon.
                    if (!string.IsNullOrEmpty(response.CartHeader.CouponCode))
                    {
                        var coupon = await _couponService.GetCoupon(response.CartHeader.CouponCode, token);

                        //Se o coupon for diferente de nulo e se o coupon code for diferente de nulo então setamos o desconto.
                        if (coupon?.CouponCode != null)
                        {
                            response.CartHeader.DiscountAmount = coupon.DiscountAmount;
                        }
                    }

                    foreach (var detail in response.CartDetails)
                    {
                        response.CartHeader.PurchaseAmount += (detail.Product.Price * detail.Count);
                    }

                    response.CartHeader.PurchaseAmount -= response.CartHeader.DiscountAmount;
                }

                return response;
            }
        }
    }
