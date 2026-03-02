using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using clunComp.Data;
using clunComp.Models;

namespace clunComp.Pages.Booking;

public class MyBookingsModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public MyBookingsModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public List<Models.Booking> Bookings { get; set; } = new();

    public async Task OnGetAsync()
    {
        Bookings = await _context.Bookings
            .Include(b => b.Computer)
            .OrderByDescending(b => b.BookingDate)
            .ToListAsync();
    }

    public async Task<IActionResult> OnPostCancelAsync(int id)
    {
        var booking = await _context.Bookings.FindAsync(id);
        if (booking == null)
        {
            return NotFound();
        }

        booking.Status = BookingStatus.Cancelled;
        await _context.SaveChangesAsync();

        return RedirectToPage();
    }
}
