using Cine_Nauta.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cine_Nauta.Controllers
{
    public class FunctionsController : Controller
    {

        private readonly DataBaseContext _context;

        public FunctionsController(DataBaseContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> Index()
        {

            return View(await _context.Functions
                 .Include(c => c.Movie)
                 .Include(c => c.Room)
                .ToListAsync());
        }
    }
}
