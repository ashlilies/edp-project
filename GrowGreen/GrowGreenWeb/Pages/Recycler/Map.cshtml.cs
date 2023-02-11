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

        public User? Learner { get; set; }
        public string MapsApiKey { get; }

        private readonly GrowGreenContext _context;
        private readonly AccountService _accountService;

        public MapModel(GrowGreenContext context, AccountService accountService)
        {
            _context = context;
            _accountService = accountService;
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
        public void OnGet(string? itemTypesIdsCsv = null,
                          int? selectedItemTypeId = null)
        {
            List<ItemType>? itemTypes = null;

            if (itemTypesIdsCsv is not null)
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
            }

            if (itemTypes?.Any() != true)
            {
                itemTypes = _context.ItemTypes
                    .OrderBy(t => t.Name)
                    .ToList();
            }

            ViewData["ItemTypes"] = itemTypes;
            ViewData["SelectedItemTypeId"] = selectedItemTypeId;

            Learner = _accountService.GetCurrentUser(HttpContext, AccountType.Learner);
        }
    }
}