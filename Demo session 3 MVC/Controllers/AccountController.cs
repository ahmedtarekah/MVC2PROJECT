using Demo_session_3_MVC.Helpers;
using Demo_session_3_MVC.ViewModels;
using Demoo.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Demo_session_3_MVC.Controllers
{
    public class AccountController : Controller
    {
		//Register
		//Login 
		//Sign Out
		//Forget Password 
		//Reset Password 


		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signinmanager;
		public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
		{
			_userManager = userManager;
			_signinmanager = signInManager;
		}
        #region Register
        public IActionResult Register()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Register(RegisterViewModel model)
		{
			if (ModelState.IsValid)
			{
				var User = new ApplicationUser()
				{
					UserName = model.Email.Split('@')[0],
					Email = model.Email,
					FName = model.FName,
					LName = model.LName,
					isAgree = model.IsAgree,

				};
				var Result = await _userManager.CreateAsync(User, model.Password);
				if (Result.Succeeded)
					return RedirectToAction(nameof(Login));
				else
					foreach (var error in Result.Errors)
						ModelState.AddModelError(string.Empty, error.Description);

			}
			return View(model);
		} 
		#endregion
		#region Login
		public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
		{
			if (ModelState.IsValid)
			{
				var User = await _userManager.FindByEmailAsync(model.Email);
				if (User is not null)
				{
					var Flag = await _userManager.CheckPasswordAsync(User, model.Password);
					if (Flag)
					{
						var Result = await _signinmanager.PasswordSignInAsync(User, model.Password, model.RememberMe, false);
						if (Result.Succeeded)
						{
							return RedirectToAction("Index", "Home");
						}
					}
					else
					{
						ModelState.AddModelError(string.Empty, "Incorrect Password");
					}

				}
				else
				{
					ModelState.AddModelError(string.Empty, "Email Is Not Exist");
				}
			}
			return View(model);
        }

		#endregion
		#region Sign Out


		public new async Task<IActionResult> SignOut()
		{
			await _signinmanager.SignOutAsync();
			return RedirectToAction(nameof(Login));
			
		}
		#endregion
		#region Forget Password

		public IActionResult ForgetPassword()
		{
			return View();
		}
		
		[HttpPost]

		public async Task<IActionResult> SendEmail(ForgetPsswordViewModel model)
		{
			if (ModelState.IsValid)
			{
				var User = await _userManager.FindByEmailAsync(model.Email);
				if (User is not null)
				{
					var token = await _userManager.GeneratePasswordResetTokenAsync(User);
					//Vaild for only one time for user 
                    //https://localhost:44327/Account/ResutPassword?email=ahmeedkassimm@gmail.com  
                    var ResutPasswordLink = Url.Action("ResetPassword", "Account", new { email=User.Email , Token = token } ,Request.Scheme);
					//Send Email
					var Email = new Email()
					{
						Subject = "Reset Password",
						To = model.Email,
						Body = ResutPasswordLink


					};
					EmailSettings.SendEmail(Email);
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
			return View() ;
		}
		#endregion
		#region Reset Password

		public IActionResult ResetPassword(string email,string token)
		{
			TempData["email"]=email;
			TempData["token"]=token;
			return View();
		}
		[HttpPost]
		public async Task <IActionResult> ResetPassword(ResetPasswordViewModel model)
		{

			if(ModelState.IsValid)
			{

		    string email = TempData["email"]as string;
			string token = TempData["token"]as string;
			var User = await _userManager.FindByEmailAsync(email);
			var Result=	await _userManager.ResetPasswordAsync(User,token,model.NewPassword);
            if (Result.Succeeded)
			return RedirectToAction(nameof(Login));	
			else
				
            foreach (var error in Result.Errors)
            
		    ModelState.AddModelError(string.Empty, error.Description);   
            }
			return View(model);
		}
		#endregion
	}
}
