

using EmailSenderProgram.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Threading.Tasks;

namespace EmailSenderProgram.Helping
{
    public class CommonMethods
    {
        /// <summary>
		/// Send Welcome mail
		/// </summary>
		/// <returns></returns>
		public async static Task<bool> DoEmailWork(EmailModel model)
        {
            try
            {
                //List all customers
                List<Customer> e = DataLayer.ListCustomers();

                //loop through list of new customers
                for (int i = 0; i < e.Count; i++)
                {
                    //If the customer is newly registered, one day back in time
                    if (e[i].CreatedDateTime > DateTime.Now.AddDays(-1))
                    {
                        //Create a new MailMessage
                        MailMessage m = new MailMessage();
                        //Add customer to reciever list
                        m.To.Add(e[i].Email);
                        //Add subject
                        m.Subject = model.Subject;// "Welcome as a new customer at EO!";
                        //Send mail from info@EO.com
                        m.From = new MailAddress(model.From);
                        //Add body to mail
                        m.Body = "Hi " + e[i].Email + model.Body;

                        //Don't send mails in debug mode, just write the emails in console
                        Console.WriteLine("Send mail to:" + e[i].Email);
                        SmtpConfigurations(m);

                    }
                }
                //All mails are sent! Success!
                return true;
            }
            catch (Exception ex)
            {
                //Something went wrong :(
                throw ex;
            }
        }

        /// <summary>
        /// Send Customer ComebackMail
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public async static Task<bool> DoEmailWork2(EmailModel model)
        {
            try
            {
                //List all customers 
                List<Customer> e = DataLayer.ListCustomers();
                //List all orders
                List<Order> f = DataLayer.ListOrders();

                //loop through list of customers
                foreach (Customer c in e)
                {
                    // We send mail if customer hasn't put an order
                    bool Send = true;
                    //loop through list of orders to see if customer don't exist in that list
                    foreach (Order o in f)
                    {
                        // Email exists in order list
                        if (c.Email == o.CustomerEmail)
                        {
                            //We don't send email to that customer
                            Send = false;
                        }
                    }

                    //Send if customer hasn't put order
                    if (Send == true)
                    {
                        //Create a new MailMessage
                        MailMessage m = new MailMessage();
                        //Add customer to reciever list
                        m.To.Add(c.Email);
                        //Add subject
                        m.Subject = model.Subject;
                        //Send mail from info@EO.com
                        m.From = new MailAddress(model.From);
                        //Add body to mail
                        m.Body = "Hi " + c.Email + model.Body;

                        //Don't send mails in debug mode, just write the emails in console
                        Console.WriteLine("Send mail to:" + c.Email);

                        //Create a SmtpClient to our smtphost: yoursmtphost
                        SmtpConfigurations(m);

                    }
                }
                //All mails are sent! Success!
                return true;
            }
            catch (Exception ex)
            {
                //Something went wrong :(
                throw ex;
            }
        }

        public static void SmtpConfigurations(MailMessage mail)
        {
            try
            {
                string email = ConfigurationSettings.AppSettings["smtpEmail"];
                string password = ConfigurationSettings.AppSettings["smtpPassword"];
                using (SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587))
                {

                    smtpClient.EnableSsl = true;
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Credentials = new NetworkCredential(email, password);

                    // Send the email
                    smtpClient.Send(mail);
                }
            }
            catch (Exception ex)
            {
            }
        }

    }
}
