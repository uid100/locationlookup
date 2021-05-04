using Microsoft.AspNetCore.Mvc;
using locationlookup.Models;
using System.Threading.Tasks;

namespace locationlookup.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() => View(new Location());

        [HttpPost]
        public async Task<IActionResult> IndexAsync(string zip) 
                => View(await Location.Lookup(zip));
    }
}
