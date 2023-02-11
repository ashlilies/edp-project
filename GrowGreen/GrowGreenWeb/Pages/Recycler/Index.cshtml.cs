using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Amazon.Rekognition.Model;
using GrowGreenWeb.Models;
using GrowGreenWeb.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Tensorflow;

namespace GrowGreenWeb.Pages.Recycler
{
    public class IndexModel : PageModel
    {
        [BindProperty, Required(ErrorMessage = "Please upload an image"), 
         DisplayName("Upload image of item")]
        public IFormFile? ImageFile { get; set; }

        public string? Result { get; set; }

        private readonly RecyclerService _recyclerService;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly ILogger<IndexModel> _logger;
        private readonly GrowGreenContext _context;

        public IndexModel(RecyclerService recyclerService, 
                          IWebHostEnvironment hostEnvironment, 
                          ILogger<IndexModel> logger, 
                          GrowGreenContext context)
        {
            _recyclerService = recyclerService;
            _hostEnvironment = hostEnvironment;
            _logger = logger;
            _context = context;
        }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || ImageFile is null)
                return Page();

            if (!Constants.AllowedImageExtensions.Contains(
                    Path.GetExtension(ImageFile.FileName)))
            {
                TempData["FlashMessage.Type"] = "danger";
                TempData["FlashMessage.Text"] = "Image type not allowed!";
                return Page();
            }

            string filepath = await UploadFile();
            DetectLabelsResponse response;
            try
            {
                response = await _recyclerService.GetLabels(filepath);
            }
            finally  // temporary file
            {
                _logger.LogInformation("Deleting file {FilePath}", filepath);
                System.IO.File.Delete(filepath);
            }

            // var items = response.Labels
            //     .Select(l => $"{l.Name} ({l.Confidence}%)");
            // Result = string.Join(", ", items);

            var searchItemLabels = response.Labels
                .OrderByDescending(l => l.Confidence)
                .Select(l => l.Name);
            List<int> selectedItemTypeIds = new List<int>();
            foreach (string label in searchItemLabels)
            {
                ItemType itemType = _context.ItemTypes
                    .Single(t => t.Name == label);
                selectedItemTypeIds.Add(itemType.Id);
            }

            string detectedItemTypesIdsCsvStr = string.Join(",", selectedItemTypeIds);
            
            // return Page();
            return RedirectToPage("Map",
                  new { itemTypesIdsCsv = detectedItemTypesIdsCsvStr });
        }

        private async Task<string> UploadFile()
        {
            string random = Guid.NewGuid().ToString();
            string ext = Path.GetExtension(ImageFile!.FileName);
            string filepath = Path.Combine(
                _hostEnvironment.WebRootPath, "uploads", 
                "recycler_tmp", random + ext);

            Directory.CreateDirectory(Path.GetDirectoryName(filepath)!);

            await using var fileStream = 
                new FileStream(filepath, FileMode.Create);
            await ImageFile.CopyToAsync(fileStream);

            return filepath;
        }
    }
}
