using GrowGreenWeb.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GrowGreenWeb.Pages.Recycler;

public class ImportData : PageModel
{
    private readonly DataImportService _svc;

    public ImportData(DataImportService svc)
    {
        _svc = svc;
    }

    public async Task<IActionResult> OnGet()
    {
        await _svc.ImportRekognitionLabels(
            "Assets/AmazonRekognitionAllLabels_v3.0.csv");

        return RedirectToPage("Index");
    }
}