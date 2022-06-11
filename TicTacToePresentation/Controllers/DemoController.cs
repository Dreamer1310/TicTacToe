using Microsoft.AspNetCore.Mvc;

namespace TicTacToePresentation.Controllers
{
    public class DemoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
