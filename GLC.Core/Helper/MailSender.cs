using GLC.Core.ViewModels;
using System.Net;
using System.Net.Mail;


namespace GLC.Core.Helper
{
    public static class MailSender
    {
        public static string SendMail(MailVM model)
        {
            try
            {   // Protocol for send and riecive mails (HOSt , Port)
                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587); //gmail smtp

                // using secure socket layer to encyrpt the connection
                smtp.EnableSsl = true;

                // email and password ele hnb3t beh w nesta2bl el emailat (Athuntecation)
                smtp.Credentials = new NetworkCredential("abcdefg.asas.125@gmail.com", "R@mbo33");

                //         sender                   reciever     title         body
                smtp.Send("abcdefg.asas.125@gmail.com", model.Mail, model.Title, model.Message);

                var result = "Mail Sent Successfully";
                return result;
            }
            catch (Exception)
            {
                var result = "Faild";
                return result;
            }
        }
    }
}
