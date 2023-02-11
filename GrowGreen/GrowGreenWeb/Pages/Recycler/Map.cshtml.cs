using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using GrowGreenWeb.Models;
using GrowGreenWeb.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Tensorflow.Keras.Engine;

namespace GrowGreenWeb.Pages.Recycler
{
    public class MapModel : PageModel
    {
        public ItemType? ItemType { get; set; }

        [BindProperty, Required]
        public double Latitude { get; set; }

        [BindProperty, Required]
        public double Longitude { get; set; }

        [BindProperty, Required, DisplayName("Friendly Name")]
        public string Name { get; set; } = null!;

        [BindProperty, Required]
        public string Address { get; set; } = null!;

        [BindProperty, Required, DisplayName("Opening Time")]
        public TimeSpan OpeningTime { get; set; } = new TimeSpan(9, 0, 0);

        [BindProperty, Required, DisplayName("Closing Time")]
        public TimeSpan ClosingTime { get; set; } = new TimeSpan(17, 0, 0);

        [BindProperty, Required, DisplayName("Item Types")]
        public List<int> ItemTypeIds { get; set; }

        [BindProperty, DisplayName("Image (Optional)")]
        public IFormFile? ImageFile { get; set; }

        public List<SelectListItem> ContributionItemTypes { get; set; } = new();
        public User? Learner { get; set; }

        public List<RecyclingLocation> RecyclingLocationPins { get; set; } = null!;
        public string MapsApiKey { get; }

        public bool IsFilteredFromRecycler { get; set; } = false;

        private readonly GrowGreenContext _context;
        private readonly AccountService _accountService;
        private IWebHostEnvironment _hostEnvironment;

        public MapModel(GrowGreenContext context, AccountService accountService,
                        IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _accountService = accountService;
            _hostEnvironment = hostEnvironment;
            MapsApiKey =
                Environment.GetEnvironmentVariable("GOOGLE_MAPS_API_KEY")!;
        }

        /// <summary>
        /// if item types list passed in, filter to those only
        /// if selected item type, then select only that type
        /// otherwise list all item types
        /// </summary>
        /// <param name="itemTypesCsv"></param>
        /// <param name="selectedItemTypeId"></param>
        public async Task<IActionResult> OnGet(string? itemTypesIdsCsv = null,
                                               int? selectedItemTypeId = null)
        {
            // for the sidebar only - searching from the AI image
            #region sidebar
            List<ItemType>? itemTypes = null;

            if (!string.IsNullOrEmpty(itemTypesIdsCsv))
            {
                string[] itemTypesIds = itemTypesIdsCsv.Split(',');

                itemTypes = new List<ItemType>();
                itemTypesIds.ToList().ForEach(id =>
                {
                    // get item type from db
                    ItemType? type = _context.ItemTypes.Find(id);
                    if (type is not null)
                        itemTypes.Add(type);
                });

                IsFilteredFromRecycler = true;
            }

            if (itemTypes?.Any() != true)
            {
                itemTypes = _context.ItemTypes
                    .OrderBy(t => t.Name)
                    .ToList();
            }

            ViewData["ItemTypes"] = itemTypes;
            ViewData["SelectedItemTypeId"] = selectedItemTypeId;
            #endregion
            
            // load map pins
            var allPins = _context.RecyclingLocations.Include(l => l.ItemTypes);

            if (selectedItemTypeId != null)
            {
                ItemType itemType = (await _context.ItemTypes
                    .FindAsync(selectedItemTypeId))!;
                RecyclingLocationPins = await allPins
                    .Where(l => l.ItemTypes.Contains(itemType))
                    .ToListAsync();
                ItemType = itemType;
            }
            else
            {
                RecyclingLocationPins = allPins.ToList();
            }

            // for the Contribution modal
            ContributionItemTypes = _context.ItemTypes
                .Select(t => new SelectListItem
                {
                    Value = t.Id.ToString(),
                    Text = t.Name
                })
                .ToList();

            Learner = _accountService.GetCurrentUser(HttpContext, AccountType.Learner);

            return Page();
        }

        public async Task<IActionResult> OnPostContribute(
            string? itemTypesIdsCsv = null,
            int? selectedItemTypeId = null)
        {
            if (!ModelState.IsValid
                || string.IsNullOrWhiteSpace(Address)
                || string.IsNullOrWhiteSpace(Name)
                || !ItemTypeIds.Any())
            {
                TempData["FlashMessage.Type"] = "danger";
                TempData["FlashMessage.Text"] =
                    "Error contributing. Check your fields again?";
                return await OnGet(itemTypesIdsCsv, selectedItemTypeId);
            }

            string? imageUrl = await UploadFile();

            List<ItemType> itemTypes = new List<ItemType>();

            foreach (int id in ItemTypeIds)
            {
                ItemType? itemType = await _context.ItemTypes.FindAsync(id);
                if (itemType is not null)
                    itemTypes.Add(itemType);
            }

            RecyclingLocation location = new RecyclingLocation
            {
                Name = Name,
                Latitude = Latitude,
                Longitude = Longitude,
                OpeningTime = OpeningTime,
                ClosingTime = ClosingTime,
                ImageUrl = imageUrl
            };

            _context.Add(location);
            await _context.SaveChangesAsync();

            itemTypes.ForEach(t => location.ItemTypes.Add(t));
            await _context.SaveChangesAsync();

            TempData["FlashMessage.Type"] = "success";
            TempData["FlashMessage.Text"] =
                "Successfully added location. Check the map!";
            return await OnGet(itemTypesIdsCsv, selectedItemTypeId);
        }

        private async Task<string?> UploadFile()
        {
            if (ImageFile is null)
                return null;

            string random = Guid.NewGuid().ToString();
            string ext = Path.GetExtension(ImageFile.FileName);
            string filepathWww = "/uploads/maps/" + random + ext;
            string filepath = Path.Combine(_hostEnvironment.WebRootPath, filepathWww.TrimStart('/'));

            Directory.CreateDirectory(Path.GetDirectoryName(filepath)!);

            await using var fileStream =
                new FileStream(filepath, FileMode.Create);
            await ImageFile.CopyToAsync(fileStream);

            return filepathWww;
        }
    }
}