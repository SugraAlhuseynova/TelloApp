using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Tello.Service.Email
{
    public class EmailSender
    {
        public static void SendEmail(string email, string link)
        {
            MailMessage message = new MailMessage();
            SmtpClient smtp = new SmtpClient();
            message.From = new MailAddress("sugra.alhuseynova@gmail.com");
            message.To.Add(new MailAddress(email));
            message.Subject = "Test";
            message.IsBodyHtml = true; //to make message body as html  
            message.Body = $"<a href='{link}'>Click here for reset password<h1>";
            smtp.Port = 587;
            smtp.Host = "smtp.gmail.com"; //for gmail host  
            smtp.EnableSsl = true;
            smtp.Credentials = new NetworkCredential("sugra.alhuseynova@gmail.com", "amckoneoyjzelwsr");
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            try
            {
                smtp.Send(message);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
