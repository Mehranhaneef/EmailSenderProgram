using EmailSenderProgram.Model;
using EmailSenderProgram.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmailSenderProgram
{
	internal class Program
	{
		/// <summary>
		/// This application is run everyday
		/// </summary>
		/// <param name="args"></param>
		private static async Task Main(string[] args)
		{
			SendEmailService sendEmailServices = new SendEmailService();
			await sendEmailServices.SendEmail(new EmailModel
			{
				Subject= "Welcome as a new customer at EO!",
				From= "info@EO.com",
				Body= "<br>We would like to welcome you as customer on our site!<br><br>Best Regards,<br>EO Team"
            });
			

		}
				
	}
}