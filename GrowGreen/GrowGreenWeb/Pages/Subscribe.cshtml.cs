using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using GrowGreenWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GrowGreenWeb.Pages
{
    public class SubscribeModel : PageModel
    {
        [BindProperty, Required, DataType(DataType.EmailAddress)]
        public string Email { get; set; } = null!;

        public NewsletterSignupStatus Status { get; set; }
        private readonly GrowGreenContext _context;

        public SubscribeModel(GrowGreenContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
            Status = NewsletterSignupStatus.InvalidEmail;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || !IsValidEmail(Email))
            {
                Status = NewsletterSignupStatus.InvalidEmail;
                return Page();
            }

            // check existing signup
            Models.Email? email = _context.Emails.SingleOrDefault(e => e.Email1 == Email);

            if (email != null)
            {
                Status = NewsletterSignupStatus.AlreadySignedUp;
            }
            else
            {
                _context.Add(new Email
                {
                    Email1 = Email,
                    Timestamp = DateTime.Now
                });

                await _context.SaveChangesAsync();
                
                Status = NewsletterSignupStatus.Success;
            }

            return Page();
        }
        
        public async Task<IActionResult> OnPostUnsubscribeAsync()
        {
            if (!ModelState.IsValid || !IsValidEmail(Email))
            {
                Status = NewsletterSignupStatus.InvalidEmail;
                return Page();
            }

            // check existing signup
            Models.Email? email = _context.Emails.SingleOrDefault(e => e.Email1 == Email);

            if (email != null)
            {
                _context.Remove(email);
                await _context.SaveChangesAsync();
            }

            Status = NewsletterSignupStatus.SuccessUnsubscribe;
            return Page();
        }

        private static bool IsValidEmail(string email)
        {
            var valid = true;

            try
            {
                var emailAddress = new MailAddress(email);
            }
            catch (Exception)
            {
                valid = false;
            }

            return valid;
        }
    }

    public enum NewsletterSignupStatus
    {
        InvalidEmail,
        AlreadySignedUp,
        Success,
        SuccessUnsubscribe
    }
}