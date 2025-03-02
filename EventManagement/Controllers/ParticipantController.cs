using Microsoft.AspNetCore.Mvc;
using EventManagement.Models;
using Microsoft.EntityFrameworkCore;
using EventManagement.DataAccess;

namespace EventManagement.Controllers
{
    public class ParticipantController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ParticipantController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Participant/
        public async Task<IActionResult> Index()
        {
            var participants = await _context.Participants.ToListAsync();
            return View(participants);
        }

        // GET: /Participant/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Participant/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Participant model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            _context.Participants.Add(model);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        // GET: /Participant/Details/{id}
        public async Task<IActionResult> Details(int id)
        {
            var participant = await _context.Participants.FindAsync(id);
            if (participant == null) return NotFound();
            return View(participant);
        }
    }
}
