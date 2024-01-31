
using System.Threading.Tasks;
using Ecommerce.Application.Models.Email;

namespace Ecommerce.Application.Contracts.Infrastructure
{
    public interface IMailService
    {
        Task<bool> RunAsync(MailContent content, string token = default);
        Task<bool> RunAsync(MailContent content);
        Task<bool> MailResetPasswordAsync(MailContent content, string token = default);
    }
}