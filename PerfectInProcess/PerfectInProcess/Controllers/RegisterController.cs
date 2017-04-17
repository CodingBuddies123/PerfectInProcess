using PerfectInProcess.Models.DataModel;
using PerfectInProcess.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PerfectInProcess.Controllers
{
    public class RegisterController : BaseController
    {
        // GET: Register
        public ActionResult Index()
        {
            return View("Register");
        }

        [HttpPost]
        public ActionResult RegisterAccount(RegisterViewModel viewModel)
        {              
            if (ModelState.IsValid)
            {
                AccountDataModel Account = new AccountDataModel(viewModel.UserName,viewModel.Email,viewModel.FirstName,viewModel.LastName);
                RegisterDataModel RegisterAccount = new RegisterDataModel(Account,viewModel.Password);


                if(RegisterAccount.listOfErrors.Count != 0)
                {
                    foreach (string error in RegisterAccount.listOfErrors)
                    {
                        ModelState.AddModelError("", error);
                    }
                    //return to register page and show errors
                    return View("Register");
                }

                Account = (AccountDataModel)Session["AccountDataModel"];

                //return back to home page with a message saying a verification email was sent.
                return View("index");
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

            RegisteredAccount.VerifyEmailTokenIDTokenPassword(tokenID,tokenPassword);


            //return account login homepage
            return View("Register");
        }
    }
}