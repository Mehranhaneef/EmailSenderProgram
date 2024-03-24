

using EmailSenderProgram.Model;
using System.Threading.Tasks;

namespace EmailSenderProgram.IServices
{
    public interface ISendEmailService
    {

        Task<ResponseDto> SendEmail(EmailModel emailModel);

    }
}
