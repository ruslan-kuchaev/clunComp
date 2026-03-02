using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using clunComp.Data;
using clunComp.Models;

namespace clunComp.Pages.Booking;

public class CreateModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public CreateModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public List<Computer> Computers { get; set; } = new();

    [BindProperty]
    public Models.Booking Booking { get; set; } = new();

    public async Task OnGetAsync()
    {
        Computers = await _context.Computers.ToListAsync();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            Computers = await _context.Computers.ToListAsync();
            return Page();
        }

        var computer = await _context.Computers.FindAsync(Booking.ComputerId);
        if (computer == null)
        {
            return NotFound();
        }

        Booking.TotalPrice = computer.PricePerHour * Booking.DurationHours;
        Booking.UserId = User.Identity?.Name ?? "Guest";
        Booking.Status = BookingStatus.Pending;
        Booking.CreatedAt = DateTime.Now;

        _context.Bookings.Add(Booking);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Success", new { id = Booking.Id });
    }
}
