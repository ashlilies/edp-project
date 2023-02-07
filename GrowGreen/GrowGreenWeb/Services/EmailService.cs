using GrowGreenWeb.Models;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace GrowGreenWeb.Services;

public class EmailService
{
    // todo update this
    // private static EmailAddress _from = new EmailAddress("admin@growgreen.gg", "GrowGreen");
    private static EmailAddress _from = new EmailAddress("fsdp@purrito.org", "GrowGreen");
    
    private readonly GrowGreenContext _context;
    
    public EmailService(GrowGreenContext context)
    {
        _context = context;
    }

    public async Task SendNewsletter(string html)
    {
        List<EmailAddress> emailAddresses = _context.Emails.Select(e => new EmailAddress(e.Email1, null)).ToList();
        
        await Execute(emailAddresses, "GrowGreen Newsletter", html);
    }
    
    private async Task Execute(List<EmailAddress> emailAddresses, string subject, string html)
    {
        var apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
        var client = new SendGridClient(apiKey);
        
        var plainTextContent = "";
        var htmlContent = html;
        
        var msg = MailHelper.CreateSingleEmailToMultipleRecipients(
            _from, emailAddresses, subject, plainTextContent, htmlContent);
        var response = await client.SendEmailAsync(msg);
        Console.WriteLine(await response.Body.ReadAsStringAsync());
    }
}