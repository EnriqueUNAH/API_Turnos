using Microsoft.AspNetCore.Mvc;

namespace FrontTurnos.Controllers
{
    public class UsuariosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
