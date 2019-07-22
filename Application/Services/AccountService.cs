using Kavenegar;
using Repository;
using System;
using System.Configuration;
using System.Data.Entity.Core.Objects;
using System.Net.Mail;
using System.Net.Mime;

namespace Application.Services
{
    public class AccountService
    {
        private AccountService()
        {

        }
        private static AccountService accountService;
        public static AccountService GetInstance()
        {
            if (accountService == null)
            {
                accountService = new AccountService();
            }

            return accountService;
        }
        public bool SendSMS(string MobileNumber, String Message)
        {
            try
            {
                string smsApiKey = ConfigurationManager.AppSettings["smsApiKey"];
                string smsSender = ConfigurationManager.AppSettings["smsSender"];
                KavenegarApi api = new KavenegarApi(smsApiKey);
                api.Send(smsSender, MobileNumber, Message);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        public string ResetPassword(string NationalID, string Email)
        {
            using (GRGEntity context = new GRGEntity())
            {
                ObjectParameter objNewPass = new ObjectParameter("newPass", typeof(string));
                ObjectParameter objReturnValue = new ObjectParameter("retVal", typeof(int));
                context.SP_Persons_ResetPassword(NationalID, Email, objNewPass, objReturnValue);
                if ((int)objReturnValue.Value == 1)
                {
                    return objNewPass.Value.ToString();
                }
                else
                {
                    return null;
                }
            }
        }
        public string GetUserFullName(int personId)
        {
            string fullName;
            try
            {
                using (GRGEntity context = new GRGEntity())
                {
                    ObjectParameter firstName = new ObjectParameter("firstName", typeof(string));
                    ObjectParameter lastName = new ObjectParameter("lastName", typeof(string));
                    ObjectParameter eFirstName = new ObjectParameter("eFirstName", typeof(string));
                    ObjectParameter eLastName = new ObjectParameter("eLastName", typeof(string));
                    ObjectParameter fatherName = new ObjectParameter("fatherName", typeof(string));
                    ObjectParameter nationalID = new ObjectParameter("nationalID", typeof(string));
                    ObjectParameter passportNo = new ObjectParameter("passportNo", typeof(string));
                    ObjectParameter birthDate = new ObjectParameter("birthDate", typeof(string));
                    ObjectParameter gender = new ObjectParameter("gender", typeof(string));
                    ObjectParameter mobileNumber = new ObjectParameter("mobileNumber", typeof(string));
                    ObjectParameter telNumber = new ObjectParameter("telNumber", typeof(string));
                    ObjectParameter eMail = new ObjectParameter("eMail", typeof(string));
                    ObjectParameter address = new ObjectParameter("address", typeof(string));
                    ObjectParameter job = new ObjectParameter("job", typeof(string));
                    ObjectParameter iDNumber = new ObjectParameter("iDNumber", typeof(string));
                    ObjectParameter birthLocate = new ObjectParameter("birthLocate", typeof(string));
                    ObjectParameter userID = new ObjectParameter("userID", typeof(string));
                    ObjectParameter hasAccount = new ObjectParameter("hasAccount", typeof(string));
                    ObjectParameter sendSMS = new ObjectParameter("sendSMS", typeof(string));
                    ObjectParameter sendEMail = new ObjectParameter("sendEMail", typeof(string));
                    ObjectParameter registerDate = new ObjectParameter("registerDate", typeof(string));
                    ObjectParameter passportExpireDate = new ObjectParameter("passportExpireDate", typeof(string));
                    ObjectParameter nationality = new ObjectParameter("nationality", typeof(string));
                    context.SP_Persons_Get(personId, firstName, lastName, eFirstName, eLastName, fatherName, nationalID, passportNo, birthDate, gender, mobileNumber, telNumber, eMail, address, job, iDNumber, birthLocate, userID, hasAccount, sendSMS, sendEMail, registerDate, passportExpireDate, nationality);
                    if (userID.Value != null)
                    {
                        return firstName.Value + " " + lastName.Value;
                    }
                    else
                    {
                        return Utility.Locale.Static.Unknown.ToString();
                    }
                }
            }
            catch (Exception e)
            {
                return Utility.Locale.Static.Unknown.ToString();
            }
        }
        public bool SendEmail(string ToEmail, string subject, string Message)
        {
            try
            {
                string SMTPServer = ConfigurationManager.AppSettings["SMTPServer"].ToString();
                string SMTPServerUserName = ConfigurationManager.AppSettings["SMTPServerUserName"].ToString();
                string SMTPServerPassword = ConfigurationManager.AppSettings["SMTPServerPassword"].ToString();
                string FromEmail = ConfigurationManager.AppSettings["FromEmail"].ToString();

                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient(SMTPServer);

                mail.From = new MailAddress(FromEmail);
                mail.To.Add(ToEmail);
                mail.Subject = subject;
                mail.Body = Message;
                mail.IsBodyHtml = true;
                SmtpServer.UseDefaultCredentials = false;
                SmtpServer.Credentials = new System.Net.NetworkCredential(SMTPServerUserName, SMTPServerPassword,"");
                SmtpServer.Send(mail);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
