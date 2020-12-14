using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bank_App.Models;
using Bank_App.DataLayer;
using System.Web.Security;

namespace Bank_App.Controllers
{
    public class HomeController : Controller
    {
        
        [HttpGet]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [HttpGet]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpGet]
        public ActionResult Registration()
        {
            RegistrationModel model = new RegistrationModel()
            {

            };


            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registration(RegistrationModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (!LoginService.RegisterUser(model.username, model.email, model.password, model.Lastname, model.FirstName))
            {
                return View(model);
            }


            return RedirectToAction("Login");

        }


        [HttpGet]
        [Authorize]
        public ActionResult AccountHome()
        {
            AccountModel model = new AccountModel()
            {
                
            };

            ///model.accounts.Add(new BankAccount() { AccountNum = 111111111111, AccountName="Test", Type="Savings", Balance = 19789.01});
            // model.Transactions.Add(new Transactions() { AccountID = 111111111111, TransactionDateTime = DateTime.Now, TransactionID = 1111, TransactionType = "Withdraw", Amount = 370.67});
            model.accounts = AccountManager.QueryAccounts(LoginService.CurrentUserID);
            model.Transactions = AccountManager.QueryTransactions(LoginService.CurrentUserID);


            return View(model);
        }


        [HttpGet]
        [Authorize]
        public ActionResult AccountForm()
        {
            AccountFormModel model = new AccountFormModel()
            {

            };

            


            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult AccountForm(AccountFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            AccountManager.AddNewBankAccount(model);


            return RedirectToAction("AccountHome");
        }


        [HttpGet]
        public ActionResult Login()
        {
            LoginModel model = new LoginModel()
            {
                //IsUserValid = true
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            //model.IsUserValid = (bool)AzureSqlAdminData.GetUserLogin(model);

            //if (model.IsUserValid)
            //{
            //    // Write the authentication cookie.
            //    FormsAuthentication.SetAuthCookie(model.UserId, false);

            //generate cookie
            if (LoginService.SignIn(model.loginID, model.passwordHash))
            {
                model.loginFailed = false;
                if (LoginService.HasUserName)
                {
                    FormsAuthentication.SetAuthCookie(LoginService.CurrentUserName, false);
                }
                else
                {
                    FormsAuthentication.SetAuthCookie(LoginService.CurrentUserID.ToString(), false);
                }
            }
            else
            {
                model.loginFailed = true;
                return View(model);
            }


            return RedirectToAction("AccountHome");
            //}
            //else
            //{
            //    return View(model);
            //}

        }

        [HttpGet]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();

            LoginService.SignOut();

            return RedirectToAction("Login");
         }

        
        public ActionResult Error()
        {
            return View();
        }

        
        

        protected override void OnException(ExceptionContext filterContext)
        {
            // Log the exception so it's not completely ignored.
            //AppInsights.TelemetryClient.TrackException(filterContext.Exception);

            filterContext.ExceptionHandled = true;
            filterContext.Result = RedirectToAction("Error");
        }






    }
}