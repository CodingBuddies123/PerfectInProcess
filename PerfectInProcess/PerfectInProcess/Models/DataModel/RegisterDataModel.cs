using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace PerfectInProcess.Models.DataModel
{
    public class RegisterDataModel
    {
        public ArrayList listOfErrors = new ArrayList();

        public int AccountId { get; set; }
        private string UserName { get; set; }
        private string Email { get; set; }
        private string FirstName { get; set; }
        private string LastName { get; set; }
        private string Salt { get; set; }
        private string Password { get; set; }
        private string TokenPassword { get; set; }
        private string TokenId { get; set; }

        public RegisterDataModel()
        {

        }
        /// <summary>
        /// Constructor when account is being registered. once register account is instanciated the register account method will continue though the registration process
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="email"></param>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="password"></param>
        public RegisterDataModel(string userName, string email, string firstName,string lastName,string password)
        {
            UserName = userName;
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            Password = password;

            RegisterAccount(Password);
        }
        /// <summary>
        /// Constructor which will be used when we want to resend verification codes to users.
        /// </summary>       
        /// <param name="email"></param>       
        /// <param name="accountID"></param>
        public RegisterDataModel(string email, int accountID)
        {            
            Email = email;           
            AccountId = accountID;       
        }
        /// <summary>
        /// This method will hash the password using a salt then add those values to the DB if the an account with the username and or email isnt taken
        /// This flow will then go to proceed to generating a Token password and TokenId and add those values to the end of the URL which will be sent via email
        /// which we can then verify when the user clicks on the link.
        /// </summary>
        /// <param name="Password"></param>
        private void RegisterAccount(string Password)
        {
            GenerateSalt();//Gets salt
            Password = Password + Salt;//adds salt to end of plain text password
            HashPassword();//gets hashes password with salt

            AddAccountToDB();

            //Send validation Email account was registered and duplicate username and or email was not used
            if (listOfErrors.Count == 0)
            {
                //generates token password stores to db then sends verification email
                GenerateTokeAndSendEmailVerification();
            }
        }


        /// <summary>
        /// Adds account to DB and return accountID for email verification set up to DB
        /// </summary>
        private void AddAccountToDB()
        {
            try
            {
                SqlConnection SqlConnection = new SqlConnection(Convert.ToString(ConfigurationManager.ConnectionStrings["DefaultConnection"]));

                using (SqlCommand command = new SqlCommand("spAccountRegister", SqlConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@UserName", SqlDbType.VarChar).Value = UserName;
                    command.Parameters.Add("@FirstName", SqlDbType.VarChar).Value = FirstName;
                    command.Parameters.Add("@LastName", SqlDbType.VarChar).Value = LastName;
                    command.Parameters.Add("@Email", SqlDbType.VarChar).Value = Email;
                    command.Parameters.Add("@Salt", SqlDbType.VarChar).Value = Salt;
                    command.Parameters.Add("@PasswordHash", SqlDbType.VarChar).Value = Password;
                    command.Parameters.Add("@AccountIDReturn", SqlDbType.Int).Direction = ParameterDirection.Output;

                    SqlConnection.Open();
                    command.ExecuteNonQuery();

                    AccountId = (int)command.Parameters["@AccountIDReturn"].Value;

                    SqlConnection.Close();
                }

               
            }
            catch (SqlException ex)
            {
                listOfErrors.Add(ex.Message);               
            }
        }

        private void AddEmailVerificationInfoToDB()
        {
            try
            {
                SqlConnection SqlConnection = new SqlConnection(Convert.ToString(ConfigurationManager.ConnectionStrings["DefaultConnection"]));

                //this is to grab the tokenID from the proc
                Guid tokenIdGUID;
                
                using (SqlCommand command = new SqlCommand("spSetEmailVerification", SqlConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@TokenId", SqlDbType.UniqueIdentifier).Direction = ParameterDirection.Output;
                    command.Parameters.Add("@TokenPassword", SqlDbType.VarChar).Value = TokenPassword;
                    command.Parameters.Add("@AccountID", SqlDbType.Int).Value = AccountId;
                    //command.Parameters.Add("@Error", SqlDbType.VarChar).Direction = ParameterDirection.Output;

                    SqlConnection.Open();
                    command.ExecuteNonQuery();

                    tokenIdGUID = (Guid)command.Parameters["@TokenId"].Value;

                    TokenId = tokenIdGUID.ToString();

                    //if ((string)command.Parameters["@Error"].Value == "")
                    //{
                    //    string Error = (string)command.Parameters["@Error"].Value;
                    //    listOfErrors.Add(Error);
                    //}


                    SqlConnection.Close();
                }
               
            }
            catch (SqlException ex)
            {
                listOfErrors.Add(ex.Message);                
            }
        }

        public void VerifyEmailTokenIDTokenPassword(string tokenID, string tokenPassword)
        {
            Guid tokenIDGUID = new Guid(tokenID);

            try
            {
                SqlConnection SqlConnection = new SqlConnection(Convert.ToString(ConfigurationManager.ConnectionStrings["DefaultConnection"]));

               

                using (SqlCommand command = new SqlCommand("spVerifyEmailLinkToken", SqlConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@TokenId", SqlDbType.UniqueIdentifier).Value = tokenIDGUID;
                    command.Parameters.Add("@TokenPassword", SqlDbType.VarChar,-1).Value = tokenPassword;
                    command.Parameters.Add("@AccountId", SqlDbType.Int).Direction = ParameterDirection.Output;
                    command.Parameters.Add("@Email", SqlDbType.VarChar,-1).Direction = ParameterDirection.Output;

                    SqlConnection.Open();
                    command.ExecuteNonQuery();

                    AccountId = (int)command.Parameters["@AccountId"].Value;
                    Email = (string)command.Parameters["@Email"].Value;


                    SqlConnection.Close();
                }
            }
            catch (SqlException ex)
            {
                listOfErrors.Add(ex.Message);
            }


            //if error in proc than dont verify the tokens in the db
            if (listOfErrors.Count == 0)
            {
                //code which gets the token password and checks if its expired
                byte[] data = Convert.FromBase64String(tokenPassword);
                DateTime when = DateTime.FromBinary(BitConverter.ToInt64(data, 0));
                if (when < DateTime.UtcNow.AddHours(-72))
                {
                    //generates token password stores to db then resends verification email
                    GenerateTokeAndSendEmailVerification();
                }
                else
                {
                    //token was verifed and account was set to active status and all tokens for this account all set to invalid
                    ChangeAccountStausActive(tokenIDGUID, tokenPassword);
                }
            }

        }

        private void ChangeAccountStausActive(Guid tokenID, string tokenPassword)
        {

            try
            {
                SqlConnection SqlConnection = new SqlConnection(Convert.ToString(ConfigurationManager.ConnectionStrings["DefaultConnection"]));

                using (SqlCommand command = new SqlCommand("spAccountStatusSetActive", SqlConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@TokenId", SqlDbType.UniqueIdentifier).Value = tokenID;
                    command.Parameters.Add("@TokenPassword", SqlDbType.VarChar).Value = tokenPassword;
                    command.Parameters.Add("@AccountId", SqlDbType.Int).Value = AccountId;
                    SqlConnection.Open();
                    command.ExecuteNonQuery();

                    SqlConnection.Close();
                }
            }
            catch (SqlException ex)
            {
                listOfErrors.Add(ex.Message);
            }
        }
        public void GenerateTokeAndSendEmailVerification()
        {
            GenerateTokenPassword();
            AddEmailVerificationInfoToDB();

            string localHost = HttpContext.Current.Request.Url.Authority.ToString();
            string urlWithTokenIdAndPassword = null;

            if (localHost.Contains("localhost"))
            {
                urlWithTokenIdAndPassword  = "http://" + localHost + "/Account/EmailVerify" + "?tokenId=" + TokenId + "?token=" + TokenPassword;
            }
            else
            {
                //URL when site is hosted
                urlWithTokenIdAndPassword = urlWithTokenIdAndPassword = "http://www.perfectinprocess.com" + "/Register/VerifyEmail" + "?tokenId=" + TokenId + "?token=" + TokenPassword;
            }
            
            SendVerificationEmailToken(urlWithTokenIdAndPassword);

        }
        private void GenerateSalt()
        {
            var random = new RNGCryptoServiceProvider();

            // Maximum length of salt
            int max_length = 32;

            // Empty salt array
            byte[] salt = new byte[max_length];

            // Build the random bytes
            random.GetNonZeroBytes(salt);

            Salt = Convert.ToBase64String(salt);
        }

        private void HashPassword()
        {
            byte[] bytes = Encoding.Unicode.GetBytes(Password);
            byte[] src = Encoding.Unicode.GetBytes(Salt);
            byte[] dst = new byte[src.Length + bytes.Length];
            Buffer.BlockCopy(src, 0, dst, 0, src.Length);
            Buffer.BlockCopy(bytes, 0, dst, src.Length, bytes.Length);
            HashAlgorithm algorithm = HashAlgorithm.Create("SHA256");
            byte[] inarray = algorithm.ComputeHash(dst);

            Password = Convert.ToBase64String(inarray);
        }


        private void GenerateTokenPassword()
        {
            byte[] time = BitConverter.GetBytes(DateTime.UtcNow.ToBinary());
            byte[] key = Guid.NewGuid().ToByteArray();
            TokenPassword = Convert.ToBase64String(time.Concat(key).ToArray()); 
        
        }

        public void SendVerificationEmailToken(string URL)
        {
            try
            {
               
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("mail.perfectinprocess.com");

                mail.From = new MailAddress("Admin@PerfectInProcess.com");
                //mail.To.Add("sisco035@gmail.com");
                // for testing I have this set to my email 
                mail.To.Add(Email);
                mail.Subject = "Verify your email";
                //going to say click link to activate account               
                mail.IsBodyHtml = true;
                mail.Body = "Hello " +"<a href=" +'"'+ URL + '"' + ">Click here to activate your account.</a>";

                SmtpServer.Port = 587;
                SmtpServer.Credentials = new NetworkCredential("Admin@PerfectInProcess.com", ConfigurationManager.AppSettings["MAIL_PASSWORD"]);
                SmtpServer.EnableSsl = false;

                SmtpServer.Send(mail);
            }
            catch (Exception ex)
            {
                listOfErrors.Add(ex.Message);
            }
        }

    }
}