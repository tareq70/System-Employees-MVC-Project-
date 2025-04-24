using DAL.Models;
using System.Net;
using System.Net.Mail;

namespace Demo.PL.Helper
{
	public class EmailSettings
	{
		public static void SendEmail(Email email)
		{
			var Client = new SmtpClient("smtp.gmail.com", 587);
			Client.EnableSsl = true;
			Client.Credentials = new NetworkCredential("tarekelspagh707@gmail.com", "peabjbxopfomefyl");
			Client.Send("tarekelspagh707@gmail.com", email.To, email.Subject, email.Body);
		}
	}
}