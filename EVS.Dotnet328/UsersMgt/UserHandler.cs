using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace EVS.Dotnet328.UsersMgt
{
    public class UserHandler
    {
        public void RegisterUser(User user)
        {
            using (TheGarmentsContext context = new TheGarmentsContext())
            {
                context.Entry<Role>(user.Role).State = EntityState.Unchanged;
                context.Entry<City>(user.Address.City).State = EntityState.Unchanged;

                context.Users.Add(user);
                context.SaveChanges();
            }
        }

        public User CurrentUser()
        {
            using (TheGarmentsContext context = new TheGarmentsContext())
            {
                return (from u in context.Users
                        .Include(u => u.Role)
                        .Include(u => u.Address.City.Province.Country)
                        select u).FirstOrDefault();
            }
        }

        public User GetUser(User user)
        {
            using (TheGarmentsContext context = new TheGarmentsContext())
            {
                return (from u in context.Users
                        .Include(u => u.Role)
                        .Include(u => u.Address.City.Province.Country)
                        where (u.LoginId == user.LoginId || u.Email == user.LoginId) && u.Password == user.Password
                        select u).FirstOrDefault();

            }

        }

        public bool ForGetPassword(string email)
        {
            bool status = false;
            TheGarmentsContext context = new TheGarmentsContext();
            User found = (from u in context.Users
                          .Include(u => u.Role)
                         .Include(u => u.Address.City.Province.Country)
                          where u.Email == email
                          select u).FirstOrDefault();
            if (found != null)
            {
                string From = "tdcofficial123@gmail.com";
                string ToEmail = email;
                string password = "abc123!@";
                try
                {
                    System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
                    SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                    mail.To.Add(ToEmail);
                    mail.From = new MailAddress(From, "Your Login Password", System.Text.Encoding.UTF8);
                    mail.SubjectEncoding = System.Text.Encoding.UTF8;
                    mail.Body = ("              Hi " + found.Name + ", <br/>" + "Your Previous Password Is : " + found.Password);
                    mail.BodyEncoding = System.Text.Encoding.UTF8;
                    mail.IsBodyHtml = true;
                    mail.Priority = MailPriority.High;
                    client.Credentials = new System.Net.NetworkCredential(From, password);
                    client.EnableSsl = true;
                    client.Send(mail);

                    status = true;
                }
                catch (Exception ex)
                {

                }
            }
            return status;
        }

        public Role GetUserRole(string role)
        {
            using (TheGarmentsContext context = new TheGarmentsContext())
            {
                return (from r in context.Roles
                        where r.Name == role
                        select r).FirstOrDefault();
            }
        }
    }
}
