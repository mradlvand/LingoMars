using Data.Context;
using Learn.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.Models;
using System.Diagnostics;

namespace Learn.Controllers
{
    public class HomeController : BaseController
    {
        private readonly DBLearnContext _context;

        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _env;

        public HomeController(ILogger<HomeController> logger, DBLearnContext context, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _context = context;
            _env = webHostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Login()
        {
            var model = new User();

            return View(model);
        }

        [HttpPost, AllowAnonymous]
        public IActionResult LoginNow(User model)
        {
            var admin = _context.Users.Where(x => x.UserName.Trim() == model.UserName.Trim() && x.Password == model.Password).FirstOrDefault();
            try
            {
                if (admin != null)
                {
                    _ = CreateAuthenticationTicket(admin);

                    return RedirectToAction("Index", "Levels");
                }
                else
                {
                    return View("LoginError", model);
                }
            }
            catch (Exception ex)
            {
                return View("Index", model);
            }
        }

        [HttpPost]
        [RequestFormLimits(MultipartBodyLengthLimit = 2147483648)]
        public async Task<IActionResult> FileUpload([FromForm] IFormFile formFile)
        {
            var name = Common.Common.UploadFile(formFile);

            return Json(new { status = "success", fileName = name });
        }
    }
}
