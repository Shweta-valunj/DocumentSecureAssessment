using Microsoft.AspNetCore.Mvc;

namespace DocumentSecureAssessment.Controllers
{
    public class FilesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
