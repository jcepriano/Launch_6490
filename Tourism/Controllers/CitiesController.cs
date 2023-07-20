using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tourism.DataAccess;
using Tourism.Models;

namespace Tourism.Controllers
{
    public class CitiesController : Controller
    {
        private readonly TourismContext _context;

        public CitiesController(TourismContext context)
        {
            _context = context;
        }

        [Route("/States/{stateId:int}/cities")]
        public IActionResult Index(int stateId)
        {
            var state = _context.States
                .Where(s => s.Id == stateId)
                .Include(s => s.Cities)
                .First();

            return View(state);
        }

        [Route("/States/{stateId:int}/cities/new")]
        public IActionResult New(int stateId)
        {
            var cityState = _context.States
            .Where(s => s.Id == stateId)
            .Include(s => s.Cities)
            .FirstOrDefault();

            return View(cityState);
        }

        [HttpPost]
        [Route("/States/{stateId:int}/cities")]
        public IActionResult Create(int stateId, City city)
        {
            var cityState = _context.States
                .Where (s => s.Id == stateId)
                .Include(s => s.Cities)
                .FirstOrDefault();

            cityState.Cities.Add(city);

            _context.SaveChanges();

            return RedirectToAction("index", new {stateId = cityState.Id});
        }
    }
}
