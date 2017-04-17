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
                AccountDataModel Account = new AccountDataModel(viewModel.UserName, viewModel.Email, viewModel.FirstName, viewModel.LastName);
                RegisterDataModel RegisterAccount = new RegisterDataModel(Account, viewModel.Password);


                if (RegisterAccount.listOfErrors.Count != 0)
                {
                    //if errors get all errors from list tro display to page 
                    foreach (string error in RegisterAccount.listOfErrors)
                    {
                        ModelState.AddModelError("", error);
                    }
                    //return to register page and show errors
                    return View("Register");
                }

                base.Account.FirstName = Account.FirstName;
                base.Account.FirstName = Account.LastName;
                base.Account.FirstName = Account.Email;
                base.SaveBase();

                //redirect to login page account created want to send a message saying verification email was sent
                return View("Login");
            }
            else
            {
                //This will show registeration page with validation errors from registration page
                return View("Register");
            }
        }
        public ActionResult EmailVerify()
        {
            AccountDataModel Account = new AccountDataModel();
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

                //return to register page and show errors
                return View("Register");
            }


            //Directs to Login page
            return View("Login");
        }
    }
}