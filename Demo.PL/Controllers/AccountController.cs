using DAL.Models;
using Demo.PL.Helper;
using Demo.PL.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Demo.PL.Controllers
{
	public class AccountController : Controller
	{
		private readonly UserManager<ApplicationUser> _userManager;

		public SignInManager<ApplicationUser> _signInManager { get; }

		public AccountController(UserManager<ApplicationUser> userManager,
			SignInManager<ApplicationUser> signInManager)
		{
			_userManager = userManager;
			_signInManager = signInManager;
		}
		//Register
		#region Register

		public IActionResult Register()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Register(RegisterViewModel model)
		{
			if (ModelState.IsValid)//Server Side Validation
			{
				var User = new ApplicationUser()
				{
					UserName = model.Email.Split('@')[0],
					Email = model.Email,
					Fname = model.FName,
					Lname = model.LName,
					IsAgree = model.IsAgree,

				};
				var Result = await _userManager.CreateAsync(User, model.Password);

				if (Result.Succeeded)
					return RedirectToAction(nameof(Login));
				else
					foreach (var error in Result.Errors)
						ModelState.AddModelError(string.Empty, error.Description);
				//return View(model);
			}
			return View(model);
		}
		#endregion

		//Login
		#region Login
		public IActionResult Login() { return View(); }
		[HttpPost]
		public async Task<IActionResult> Login(LoginViewModel model)
		{
			if (ModelState.IsValid)
			{
				var User = await _userManager.FindByEmailAsync(model.Email);
				if (User is not null)
				{
					//Login
					var flag = await _userManager.CheckPasswordAsync(User, model.Password);
					if (flag)
					{
						//Login
						var Result = await _signInManager.PasswordSignInAsync(User, model.Password, model.RememberMe, false);
						if (Result.Succeeded)
							return RedirectToAction("Index", "Home");
					}
					else
					{
						ModelState.AddModelError(string.Empty, "Incorrect Password");
					}
				}
				else
				{
					ModelState.AddModelError(string.Empty, "Email is not exsits");
				}

			}
			return View(model);

		}
        #endregion

        //Sign Out

        #region Sign Out
        public new async Task<IActionResult> SignOut()
		{
			await _signInManager.SignOutAsync();
			return RedirectToAction(nameof(Login));
		}
        #endregion

        //Forget Password 
        #region Forget Password 
        public IActionResult ForgetPassword() { return View(); }
		[HttpPost]
		public async Task<IActionResult> SendEmail(ForgetPasswordViewModel model)
		{
			if (ModelState.IsValid)
			{
				var User = await _userManager.FindByEmailAsync(model.Email);

				if (User is not null)
				{
					var token = await _userManager.GeneratePasswordResetTokenAsync(User);
					var ResetPasswordLink = Url.Action("ResetPassword", "Account", new { email = User.Email, Token = token }, Request.Scheme);
					var email = new Email()
					{
						To = model.Email,
						Subject = "Reset Password",
						Body = ResetPasswordLink
					};
					EmailSettings.SendEmail(email);
					return RedirectToAction(nameof(CheckYourInbox));
				}
				else
				{
					ModelState.AddModelError(string.Empty, "Email is not Exists");
				}
			}
			return View("ForgetPassword", model);
		}

		public IActionResult CheckYourInbox()
		{
			return View();
		} 
		#endregion

		public IActionResult ResetPassword(string email , string token)
		{
			TempData["email"] =email;
			TempData["token"] =token;
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
		{
			if(ModelState.IsValid) 
			{
				string email = TempData["email"] as string;
				string token = TempData["token"] as string;
				var user = await _userManager.FindByEmailAsync(email);
				var Result =await _userManager.ResetPasswordAsync(user, token, model.NewPassword);
				if (Result.Succeeded)
				{
					return RedirectToAction(nameof(Login));
				}
				else
				{
					foreach(var error in Result.Errors)
					{
						ModelState.AddModelError(string.Empty, error.Description);
					}
				}
			}
			return View(model);
		}


	}
}
