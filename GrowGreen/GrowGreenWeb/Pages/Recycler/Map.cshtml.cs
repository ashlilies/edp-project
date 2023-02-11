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
            ViewData["ItemTypeIdsCsv"] = itemTypesIdsCsv;

            if (!string.IsNullOrEmpty(itemTypesIdsCsv))
            {
                string[] itemTypesIds = itemTypesIdsCsv.Split(',');

                itemTypes = new List<ItemType>();
                itemTypesIds.ToList().ForEach(id =>
                {
                    // get item type from db
                    int itemTypeId = Convert.ToInt32(id);
                    ItemType? type = _context.ItemTypes.Find(itemTypeId);
                    if (type is not null)
                        itemTypes.Add(type);
                });

                IsFilteredFromRecycler = true;
                ViewData["IsFilteredFromRecycler"] = true;
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
            var allPins = (await _context.RecyclingLocations
                    .Include(l => l.ItemTypes)
                    .Include(l => l.User)
                    .ToListAsync())
                .Where(l => l.ItemTypes.Intersect(itemTypes).Any());

            if (selectedItemTypeId != null)
            {
                ItemType itemType = (await _context.ItemTypes
                    .FindAsync(selectedItemTypeId))!;
                RecyclingLocationPins = allPins
                    .Where(l => l.ItemTypes.Contains(itemType))
                    .ToList();
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
                Address = Address,
                Latitude = Latitude,
                Longitude = Longitude,
                OpeningTime = OpeningTime,
                ClosingTime = ClosingTime,
                ImageUrl = imageUrl,
                UserId = _accountService.GetCurrentUser(HttpContext)!.Id
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

        public async Task<IActionResult> OnGetDelete(int id)
        {
            User? user = _accountService.GetCurrentUser(HttpContext);
            RecyclingLocation? toDelete = await _context.RecyclingLocations
                .FindAsync(id);

            if (toDelete is null)
            {
                TempData["FlashMessage.Type"] = "danger";
                TempData["FlashMessage.Text"] =
                    "Error deleting your contribution.";
                return await OnGet();
            }

            if (toDelete.UserId != user.Id)
            {
                TempData["FlashMessage.Type"] = "danger";
                TempData["FlashMessage.Text"] =
                    "Error deleting your contribution.";
                return await OnGet();
            }

            _context.Remove(toDelete);
            await _context.SaveChangesAsync();


            TempData["FlashMessage.Type"] = "success";
            TempData["FlashMessage.Text"] =
                "Successfully removed your contribution.";

            return await OnGet();
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