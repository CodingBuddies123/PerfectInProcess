using PerfectInProcess.Models.DataModel;
using PerfectInProcess.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PerfectInProcess.Controllers
{
    public class AccountController : BaseController
    {
        // GET: Account 
        public ActionResult Register()
        {
            return View("Register");
        }

        [HttpPost]
        public ActionResult RegisterAccount(RegisterViewModel viewModel)
        {
            if (ModelState.IsValid)
            {               
                RegisterDataModel RegisterAccount = new RegisterDataModel(viewModel.UserName, viewModel.Email, viewModel.FirstName, viewModel.LastName,
                    viewModel.Password);

                if (RegisterAccount.GetError().Count != 0)
                {
                    //if errors get all errors from list tro display to page 
                    foreach (string error in (RegisterAccount.GetError()))
                    {
                        ModelState.AddModelError("", error);
                    }
                    //return to register page and show errors
                    return View("Register");
                }

                base.Account.FirstName = viewModel.FirstName;
                base.Account.LastName = viewModel.LastName;
                base.Account.Email = viewModel.Email;
                base.Account.AccountId = RegisterAccount.AccountId;
                base.SaveBase();                 
                
                return View("VerifyEmail");
            }
            else
            {
                //This will show registeration page with validation errors from registration page
                return View("Register");
            }
        }
        public ActionResult EmailVerify()
        {         
            RegisterDataModel RegisteredAccount = new RegisterDataModel();

            //gets raw url clicked on by user and gets the tokenID and TokenPassword from the url
            string[] urlClicked = Request.RawUrl.ToString().Split('=');
            string tokenID = urlClicked[1].Replace("?token", "");
            string tokenPassword = urlClicked[2];

            //verifies if tokan is valid if not valid resends verification link
            RegisteredAccount.VerifyEmailTokenIDTokenPassword(tokenID, tokenPassword);

            if (RegisteredAccount.listOfErrors.Count != 0)
            {
                foreach (string error in RegisteredAccount.listOfErrors)
                {
                    ModelState.AddModelError("", error);
                }

                //Shows expired token page so user knows a new link was sent
                return View("TokenExpired");
            }

            ViewBag.Verifed = "You account has been verified and has been set to active status.";
            //Directs to Login page
            return View("Login");
        }
        public ActionResult ResendVerification()
        {            
            RegisterDataModel RegisteredAccount = new RegisterDataModel(base.Account.Email,base.Account.AccountId);

            RegisteredAccount.GenerateTokeAndSendEmailVerification();             

            if (RegisteredAccount.listOfErrors.Count != 0)
            {
                foreach (string error in RegisteredAccount.listOfErrors)
                {
                    ModelState.AddModelError("", error);
                }
              
                //Shows expired token page so user knows a new link was sent
                return View("TokenExpired");
            }


            ViewBag.Sent = "New verification email was sent to the email on file.";

            //Directs to Login page
            return View("VerifyEmail");
        }
    }
}