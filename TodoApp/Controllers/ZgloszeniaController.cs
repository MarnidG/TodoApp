using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using TodoApp.Data;

public class ZgloszeniaController : Controller
{
    private readonly ApplicationDbContext _context;

    public ZgloszeniaController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var Zgloszenia = _context.Zgloszenia.ToList();
        return View(Zgloszenia);
    }
    [Authorize(Roles = "Admin")]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public IActionResult Create(Zgloszenia model)
    {
        if (ModelState.IsValid)
        {
            _context.Zgloszenia.Add(model);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        return View(model);
    }

    public IActionResult Details(int id)
    {
        var Zgloszenia = _context.Zgloszenia.FirstOrDefault(a => a.Id == id);

        if (Zgloszenia == null)
        {
            return NotFound();
        }

        return View(Zgloszenia);
    }
}
