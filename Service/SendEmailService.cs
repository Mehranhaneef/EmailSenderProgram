

using EmailSenderProgram.Helping;
using EmailSenderProgram.IServices;
using EmailSenderProgram.Model;
using System;
using System.Threading.Tasks;

namespace EmailSenderProgram.Services
{
    public class SendEmailService : ISendEmailService
    {

        public SendEmailService() {  }

        // Used the method to Send the Email
        public async Task<ResponseDto> SendEmail(EmailModel emailModel)
        {
            try
            {
                var success = await CommonMethods.DoEmailWork(emailModel);
                //if the day of week is Monday then this will Send Comback Email
                if (DateTime.Now.DayOfWeek.Equals(DayOfWeek.Monday))
                {
                    Console.WriteLine("Send Comebackmail");
                    emailModel.extraBodyOption = "EOComebackToUs";
                    emailModel.Subject = "We miss you as a customer";
                    emailModel.Body = "<br>We miss you as a customer. Our shop is filled with nice products. Here is a voucher that gives you 50 kr to shop for." +
                                 "<br>Voucher: " + emailModel.extraBodyOption +
                                 "<br><br>Best Regards,<br>EO Team";
                    success = await CommonMethods.DoEmailWork2(emailModel);
                }
                var response = new ResponseDto
                {
                    Status = success,
                    Message = "Email Sent Successfully"
                };
                Console.WriteLine($"Status:{response.Status}, Message:{response.Message} ");
                //Check if the sending went OK
                if (success == true)
                {
                    Console.WriteLine("All mails are sent, I hope...");
                }
                //Check if the sending was not going well...
                if (success == false)
                {
                    Console.WriteLine("Oops, something went wrong when sending mail (I think...)");
                }
                Console.ReadKey();
                return response;

            }
            catch (Exception ex)
            {
                return  new ResponseDto
                {
                    Status = false,
                    Message = ex.Message
                };
            }
        }



    }
}
