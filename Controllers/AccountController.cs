using Microsoft.AspNetCore.Mvc;

namespace HRSystem.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService accountService;
        private readonly SignInManager<Hr> signInManager;

        public AccountController(IAccountService accountService, SignInManager<Hr> signInManager)
        {
            this.accountService = accountService;
            this.signInManager = signInManager;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                if (await accountService.GetByEmail(registerViewModel.Email) != null)
                {
                    ModelState.AddModelError("Email", "This Email Already Exist!!");
                    return View(registerViewModel);
                }
                else if (accountService.GetByPhone(registerViewModel.PhoneNumber) != null)
                {
                    ModelState.AddModelError("PhoneNumber", "This Phone Number Already Exist!!");
                    return View(registerViewModel);
                }
                IdentityResult result = await accountService.Create(registerViewModel);
                if (!result.Succeeded)
                {

                    return View(registerViewModel);
                }
                return RedirectToAction("Login");

            }

            return View(registerViewModel);
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                Hr hr = await accountService.GetByEmail(loginViewModel.Email);
                if (hr == null)
                {
                    ModelState.AddModelError("Email", "This Email Not Found!!");
                    return View(loginViewModel);
                }
                else if (await accountService.CheckPassword(hr, loginViewModel.passwrod))
                {
                    await signInManager.SignInAsync(hr, loginViewModel.RemeberMe);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("Password", "Invalid Password!!");
                    return View(loginViewModel);
                }


            }
            return View();
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
