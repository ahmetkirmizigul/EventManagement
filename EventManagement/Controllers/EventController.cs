using Microsoft.AspNetCore.Mvc;
using EventManagement.Models;
using Microsoft.EntityFrameworkCore;
using EventManagement.DataAccess;

namespace EventManagement.Controllers;

//TO DO : 
//Anasayfaya iki controller için de buton koyulacak.
//ekleme işlemin de katılımcılar daha iyi gösterilecek


public class EventController : Controller
{
    private readonly ApplicationDbContext _context;

    public EventController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: /Event/
    public async Task<IActionResult> Index()
    {
        var events = await _context.Events.ToListAsync();
        return View(events);
    }

    // GET: /Event/Create
    public IActionResult Create()
    {
        var participants = _context.Participants.ToList();

        // Eğer sistemde hiç katılımcı yoksa boş liste döndür.
        ViewBag.Participants = participants ?? new List<Participant>();

        return View();
    }

    // POST: /Event/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Event model, int[] SelectedParticipants)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Participants = _context.Participants.ToList();
            return View(model);
        }

        _context.Events.Add(model);
        await _context.SaveChangesAsync();

        // Seçilen katılımcıları kontrol et ve ilişkilendir
        if (SelectedParticipants != null && SelectedParticipants.Length > 0)
        {
            foreach (var participantId in SelectedParticipants)
            {
                _context.EventParticipants.Add(new EventParticipant
                {
                    EventId = model.Id,
                    ParticipantId = participantId
                });
            }
            await _context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index));
    }


    // GET: /Event/Details/{id}
    public async Task<IActionResult> Details(int id)
    {
        var evt = await _context.Events.FindAsync(id);
        if (evt == null) return NotFound();
        return View(evt);
    }

    public async Task<IActionResult> GetParticipants(int id)
    {
        var participants = await _context.EventParticipants
            .Where(ep => ep.EventId == id)
            .Select(ep => new { ep.Participant.FirstName, ep.Participant.LastName })
            .ToListAsync();

        return Json(participants);
    }

}
