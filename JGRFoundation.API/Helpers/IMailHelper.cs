using JGRFoundation.Shared.Responses;

namespace JGRFoundation.API.Helpers
{
    public interface IMailHelper
    {
        Response SendMail(string toName, string toEmail, string subject, string body);
    }
}
