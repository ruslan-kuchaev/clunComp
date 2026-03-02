using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using clunComp.Data;
using clunComp.Models;

namespace clunComp.Pages.Admin;

public class IndexModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public IndexModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public List<Models.Booking> PendingBookings { get; set; } = new();
    public List<Models.Booking> ConfirmedBookings { get; set; } = new();
    public List<Computer> Computers { get; set; } = new();

    public async Task OnGetAsync()
    {
        PendingBookings = await _context.Bookings
            .Include(b => b.Computer)
            .Where(b => b.Status == BookingStatus.Pending)
            .OrderBy(b => b.BookingDate)
            .ToListAsync();

        ConfirmedBookings = await _context.Bookings
            .Include(b => b.Computer)
            .Where(b => b.Status == BookingStatus.Confirmed)
            .OrderBy(b => b.BookingDate)
            .ToListAsync();

        Computers = await _context.Computers.ToListAsync();
    }

    public async Task<IActionResult> OnPostConfirmAsync(int id)
    {
        var booking = await _context.Bookings.FindAsync(id);
        if (booking == null)
        {
            return NotFound();
        }

        booking.Status = BookingStatus.Confirmed;
        await _context.SaveChangesAsync();

        return RedirectToPage();
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

    public async Task<IActionResult> OnPostCompleteAsync(int id)
    {
        var booking = await _context.Bookings.FindAsync(id);
        if (booking == null)
        {
            return NotFound();
        }

        booking.Status = BookingStatus.Completed;
        await _context.SaveChangesAsync();

        return RedirectToPage();
    }

    public async Task<IActionResult> OnPostToggleAvailabilityAsync(int id)
    {
        var computer = await _context.Computers.FindAsync(id);
        if (computer == null)
        {
            return NotFound();
        }

        computer.IsAvailable = !computer.IsAvailable;
        await _context.SaveChangesAsync();

        return RedirectToPage();
    }
}
