using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using clunComp.Data;

namespace clunComp.Pages.Booking;

public class SuccessModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public SuccessModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public Models.Booking Booking { get; set; } = null!;

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var booking = await _context.Bookings
            .Include(b => b.Computer)
            .FirstOrDefaultAsync(b => b.Id == id);

        if (booking == null)
        {
            return NotFound();
        }

        Booking = booking;
        return Page();
    }
}
