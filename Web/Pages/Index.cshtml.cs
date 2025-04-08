using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Data;
using Data.Models;

public class IndexModel : PageModel
{
    [BindProperty] public string Name { get; set; } = null!;
    [BindProperty] public DateTime Dob { get; set; }
    [BindProperty] public int Age { get; set; }
    [BindProperty] public int NameLength { get; set; }

    private readonly ApplicationDbContext _context;
    private readonly HttpClient _api;
    private readonly HttpClient _napi;

    public IndexModel(ApplicationDbContext context, IHttpClientFactory factory)
    {
        _context = context;
        _api = factory.CreateClient("api");
        _napi = factory.CreateClient("napi");
    }

    public async Task<IActionResult> OnPostAsync()
    {
        _context.Users.Add(new User { Name = Name, Dob = Dob });
        await _context.SaveChangesAsync();
        return RedirectToPage();
    }

    public async Task<IActionResult> OnPostConsoleAsync()
    {
        _context.Users.Add(new User { Name = Name, Dob = Dob });
        await _context.SaveChangesAsync();
        return RedirectToPage();
    }

    public async Task<IActionResult> OnPostCalculateAgeAsync()
    {
        Age = await _api.GetFromJsonAsync<int>($"api/age?dob={Dob:yyyy-MM-dd}");
        return Page();
    }

    public async Task<IActionResult> OnPostNameLengthAsync()
    {
        NameLength = await _api.GetFromJsonAsync<int>($"api/name?name={Name}");
        return Page();
    }
}