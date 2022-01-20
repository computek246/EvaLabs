using System.Threading.Tasks;
using EvaLabs.Helper;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;

namespace EvaLabs.Security.Implementations
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailSettings _options;

        public EmailSender(IOptions<EmailSettings> options)
        {
            _options = options.Value;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            await Task.FromResult(true);
        }
    }
}