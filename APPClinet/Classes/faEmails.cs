using Microsoft.AspNetCore.Identity.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using APPClinet.Messages;
using System.IO;
using APPClinet.Areas.Identity.Data.Interfaces;

namespace APPClinet.Classes
{
    public class faEmails
    {
        //codewithmukesh.com/blog/send-emails-with-aspnet-core/
        //github.com/iammukeshm/MailService.WebApi
        public void SendEmail(string emailuser, string subject, string callbackUrl, string emailsite, string filepath, string description, string text,
            string texthistory, string activation, string textlink, string url, string websitemainzlogo, string websitekelisayesalib,
            string facebook, string twitter, string instagram, string youtube, string telegram, string email, string street, int number, int postcode,
            string city, string country)
        {
            StreamReader str = new StreamReader(filepath);
            string MailText = str.ReadToEnd();
            str.Close();
            MailText = MailText.Replace("[username]", emailuser).Replace("[callbackUrl]", callbackUrl).Replace("[description]", description)
                .Replace("[text]", text).Replace("[texthistory]", texthistory).Replace("[activation]", activation).Replace("[textlink]", textlink)
                .Replace("[url]", url).Replace("[websitemainzlogo]", websitemainzlogo).Replace("[websitekelisayesalib]", websitekelisayesalib)
                .Replace("[facebook]", facebook).Replace("[twitter]", twitter).Replace("[instagram]", instagram).Replace("[youtube]", youtube)
                .Replace("[telegram]", telegram).Replace("[email]", email).Replace("[street]", street).Replace("[number]", number.ToString())
                .Replace("[postcode]", postcode.ToString()).Replace("[city]", city).Replace("[country]", country).Replace("[year]", DateTime.Now.Year.ToString());
            MailMessage msg = new MailMessage();
            msg.Body = MailText;
            msg.BodyEncoding = Encoding.UTF8;
            msg.IsBodyHtml = true;
            msg.From = new MailAddress(emailsite, TitleApp.app_en + TitleApp.app_fa, Encoding.UTF8);
            msg.Priority = MailPriority.Normal;
            msg.Sender = msg.From;
            msg.Subject = TitleApp.app_en + " - " + subject;
            msg.SubjectEncoding = Encoding.UTF8;
            msg.To.Add(new MailAddress(emailuser, emailuser, Encoding.UTF8));

            SmtpClient smtp = new SmtpClient();
            smtp.Host = faSmtp.Host;
            smtp.Port = faSmtp.Port;
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(emailsite, faSmtp.Password);

            smtp.Send(msg);
        }
        //litmus.com/community/learning/27-how-to-add-a-countdown-timer-to-your-email
        //github.com/Omgitsonlyalex/EmailCountdown

        //countdownmail.com
        //sendtric.com
        public void SendNewsletterEmail(string emailuser, string subject, string callbackUrl, string emailsite, string filepath, string titlesubcategory,
            string text, string datetimefa, string datetimeen, string timeevent, string textviewbutton, string url, string websitemainzlogo,
            string websitekelisayesalib,
            string facebook, string twitter, string instagram, string youtube, string telegram, string email, string street, int number, int postcode,
            string city, string country)
        {
            StreamReader str = new StreamReader(filepath);
            string MailText = str.ReadToEnd();
            str.Close();
            MailText = MailText.Replace("[callbackUrl]", callbackUrl).Replace("[year]", DateTime.Now.Year.ToString())
                .Replace("[titlesubcategory]", titlesubcategory).Replace("[text]", text).Replace("[datetimefa]", datetimefa)
                .Replace("[datetimeen]", datetimeen).Replace("[timeevent]", timeevent).Replace("[textviewbutton]", textviewbutton)
                .Replace("[url]", url).Replace("[websitemainzlogo]", websitemainzlogo).Replace("[websitekelisayesalib]", websitekelisayesalib)
                .Replace("[facebook]", facebook).Replace("[twitter]", twitter).Replace("[instagram]", instagram).Replace("[youtube]", youtube)
                .Replace("[telegram]", telegram).Replace("[email]", email).Replace("[street]", street).Replace("[number]", number.ToString())
                .Replace("[postcode]", postcode.ToString()).Replace("[city]", city).Replace("[country]", country).Replace("[year]", DateTime.Now.Year.ToString());
            MailMessage msg = new MailMessage();
            msg.Body = MailText;
            msg.BodyEncoding = Encoding.UTF8;
            msg.IsBodyHtml = true;
            msg.From = new MailAddress(emailsite, TitleApp.app_en + TitleApp.app_fa, Encoding.UTF8);
            msg.Priority = MailPriority.Normal;
            msg.Sender = msg.From;
            msg.Subject = subject;
            msg.SubjectEncoding = Encoding.UTF8;
            msg.To.Add(new MailAddress(emailuser, emailuser, Encoding.UTF8));

            SmtpClient smtp = new SmtpClient();
            smtp.Host = faSmtp.Host;
            smtp.Port = faSmtp.Port;
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(emailsite, faSmtp.Password);

            smtp.Send(msg);
        }
    }
}
