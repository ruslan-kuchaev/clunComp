using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using clunComp.Data;
using clunComp.Models;
using Microsoft.AspNetCore.Authorization;

namespace clunComp.Pages.Admin;

[Authorize(Roles = "Admin")]
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

    public async Task<IActionResult> OnPostAddComputerAsync(string name, string zone, string specifications, decimal pricePerHour)
    {
        var computer = new Computer
        {
            Name = name,
            Zone = zone,
            Specifications = specifications,
            PricePerHour = pricePerHour,
            IsAvailable = true
        };

        _context.Computers.Add(computer);
        await _context.SaveChangesAsync();

        return RedirectToPage();
    }

    public async Task<IActionResult> OnPostEditComputerAsync(int id, string name, string zone, string specifications, decimal pricePerHour)
    {
        var computer = await _context.Computers.FindAsync(id);
        if (computer == null)
        {
            return NotFound();
        }

        computer.Name = name;
        computer.Zone = zone;
        computer.Specifications = specifications;
        computer.PricePerHour = pricePerHour;

        await _context.SaveChangesAsync();

        return RedirectToPage();
    }

    public async Task<IActionResult> OnPostDeleteComputerAsync(int id)
    {
        var computer = await _context.Computers.FindAsync(id);
        if (computer == null)
        {
            return NotFound();
        }

        // Проверяем, есть ли активные бронирования
        var hasActiveBookings = await _context.Bookings
            .AnyAsync(b => b.ComputerId == id && 
                          (b.Status == BookingStatus.Pending || b.Status == BookingStatus.Confirmed));

        if (hasActiveBookings)
        {
            // Можно добавить TempData сообщение об ошибке
            return RedirectToPage();
        }

        _context.Computers.Remove(computer);
        await _context.SaveChangesAsync();

        return RedirectToPage();
    }
}
