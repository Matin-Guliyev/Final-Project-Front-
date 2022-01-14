using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Coffee_Blend_MVC.Areas.Manage.Helper
{
    public static class EmailHelper
    {
        public static IHostingEnvironment WebEnv()
        {
            var _accessor = new HttpContextAccessor();
            return _accessor.HttpContext.RequestServices.GetRequiredService<IHostingEnvironment>();
        }

        public static string path = WebEnv().WebRootPath;


        public static bool SendMailToOneUser(string recipient, string subject)
        {
            SmtpClient client = new SmtpClient("smtp.gmail.com");

            client.Port = 587;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            System.Net.NetworkCredential credentials =
                new System.Net.NetworkCredential("shopwisesite@gmail.com", "shopwise!@123");
            client.EnableSsl = true;
            client.Credentials = credentials;

            try
            {
                var mail = new MailMessage("shopwisesite@gmail.com", recipient.Trim());
                mail.Subject = subject;
                mail.IsBodyHtml = true;
                string body = File.ReadAllText(Path.Combine(path, @"Mail", @"mail.html"));
                body = body.Replace("#MailTopic#", subject);
                body = body.Replace("#Action#", subject);
                //body = body.Replace("#Link#", url);
                //body = body.Replace("#Token#", token);
                mail.Body = body;
                client.Send(mail);

                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }

        public static bool SendMailToManyUsers(List<string> recipients, string subject)
        {
            var smtp = new System.Net.Mail.SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("shopwisesite@gmail.com", "shopwise!@123")
            };

            try
            {
                var mail = new MailMessage();

                foreach (var recipient in recipients)
                {
                    mail.To.Add(recipient);
                }

                mail.From = new MailAddress("shopwisesite@gmail.com", "ShopWise");
                mail.Subject = subject;
                mail.IsBodyHtml = true;
                string body = File.ReadAllText(Path.Combine(path + "Mail" + "mail.html"));
                body = body.Replace("#MailTopic#", subject);
                body = body.Replace("#Action#", subject);

                mail.Body = body;


                smtp.Send(mail);

                return true;

            }
            catch (Exception)
            {
                return false;
            }
        }


    }
}
