using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using MessagingApp.Models;
using MessagingApp.ViewModels;

namespace MessagingApp.Controllers
{
    public class LoginController : Controller
    {
        private UserManager<AppUser> userManager;
        private SignInManager<AppUser> signInManager;
        private IUserValidator<AppUser> userValidator;
        private IPasswordValidator<AppUser> passwordValidator;
        private IPasswordHasher<AppUser> passwordHasher;
        public LoginController(
                UserManager<AppUser> usrMgr,
                SignInManager<AppUser> signinMgr,
                IUserValidator<AppUser> userValid,
                IPasswordValidator<AppUser> passValid,
                IPasswordHasher<AppUser> passwordHash)
        {
            userManager = usrMgr;
            signInManager = signinMgr;
            userValidator = userValid;
            passwordValidator = passValid;
            passwordHasher = passwordHash;
        }
        public ViewResult Index()
        {
            return View();
        }

        public ViewResult Signup()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                AppUser user = await userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    await signInManager.SignOutAsync();
                    var result = await signInManager.PasswordSignInAsync(user, model.Password, false, false);
                    if (result.Succeeded)
                    {
                        return Redirect(returnUrl ?? "/Home/Index");
                    }
                }
                ModelState.AddModelError(nameof(LoginViewModel.Email), "Invalid email or password");
            }
            return View("Index",model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Signup(CreateUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                if(model.ConfirmPassword == model.Password)
                {
                    AppUser user = new AppUser
                    {
                        UserName = model.Username,
                        Email = model.Email
                    };
                    IdentityResult result
                        = await userManager.CreateAsync(user, model.Password);

                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        foreach (IdentityError error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError(nameof(CreateUserViewModel.ConfirmPassword), "Passwords must match");
                }
            }
            return View(model);
        }
    }
}
