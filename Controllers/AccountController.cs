using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Okta.AspNetCore;
using yarlab5.Models;

namespace yarlab5.Controllers
{
	public class AccountController : Controller
	{

		[HttpGet]
		public IActionResult SignIn()
		{
			if (!HttpContext.User.Identity.IsAuthenticated)
			{
				return Challenge(OktaDefaults.MvcAuthenticationScheme);
			}
			return RedirectToAction("Index", "Home");
		}

		[HttpPost]
		public IActionResult SignOut()
		{
			return new SignOutResult(
				new[]
				{
					OktaDefaults.MvcAuthenticationScheme,
					CookieAuthenticationDefaults.AuthenticationScheme,
				},
				new AuthenticationProperties { RedirectUri = "/Home/" });
		}

		[HttpGet]
		public IActionResult Profile()
		{
			return View(new UserProfileModel()
			{
				UserName = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "name").Value.ToString(),
				Email = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "email").Value.ToString(),
				EmailVerified = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "email_verified").Value.ToString()
			});
		}
	}
}