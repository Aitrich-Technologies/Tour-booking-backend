using System.Threading.Tasks;
using AutoMapper.Internal;
using Domain.Helper;
using Domain.Services.Email.Helper;

namespace Domain.Services.Email.Interface
{
    public interface IMailService
    {
        Task SendEmailAsync(MailRequest request);
    }
}
