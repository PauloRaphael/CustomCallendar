using DataRepository.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

public class LoginController : Controller
{
    private readonly LoginService _loginService;

    public LoginController(LoginService loginService)
    {
        _loginService = loginService;
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    [AllowAnonymous]
    public IActionResult Index(string inputPassword)
    {
        if (_loginService.Authorize(inputPassword))
        {
            HttpContext.Session.SetString("Authenticated", "true");
            return RedirectToAction("Index", "Blocks");
        }

        ViewBag.Error = "Invalid Password";
        return View();
    }

    [HttpPost]
    public IActionResult Logout()
    {
        // Clear the session
        HttpContext.Session.Clear();

        // Redirect to the login page
        return RedirectToAction("Index", "Login");
    }
}
