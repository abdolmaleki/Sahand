using Application.Services;
using Repository;
using Repository.Models.ViewModels;
using System;
using System.Data.Entity.Core.Objects;
using System.Web.Mvc;
using Utility.Constant;

namespace Gardeshgaran.Controllers
{
    public class AccountController : BaseController
    {
        private AccountService AccountService;
        public AccountController()
        {
            AccountService = AccountService.GetInstance();
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginViewModel loginModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (GRGEntity context = new GRGEntity())
                    {
                        ObjectParameter objPersonID = new ObjectParameter("personID", typeof(int));
                        context.SP_Persons_Validate(loginModel.Email, loginModel.Password, objPersonID);
                        if ((int)objPersonID.Value > 0)
                        {
                            Session[SessionKey.UserID] = objPersonID.Value;
                            Session[SessionKey.UserFullName] = AccountService.GetUserFullName((int)objPersonID.Value);
                            return RedirectToAction("SearchFlight", "Home");
                        }
                        else
                        {
                            ViewBag.Error = Utility.Locale.Message.LoginFailed.ToString();
                        }
                    }
                }
                catch (Exception e)
                {
                    return RedirectToAction("HTTP500", "Error", new { ErrorMessage = e.Message });
                }
            }
            else
            {
                ViewBag.IsValidModelState = false;
            }

            return View(loginModel);
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (GRGEntity context = new GRGEntity())
                    {
                        ObjectParameter objpersonID = new ObjectParameter("personID", typeof(int));
                        context.SP_Persons_Insert(objpersonID, registerViewModel.FirstName, registerViewModel.LastName, "", "", "", "", "", "", true, "", "", registerViewModel.Email, "", "", "", "", false, false, null, 0);
                        int PersonID = (int)objpersonID.Value;
                        if (PersonID > 0)
                        {
                            string UserPassword = AccountService.ResetPassword(registerViewModel.NationalCode, registerViewModel.Email);
                            if (UserPassword != null)

                            {

                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    return RedirectToAction("HTTP500", "Error", new { ErrorMessage = e.Message });
                }
            }
            else
            {
                ViewBag.IsValidModelState = false;
            }
            return View();
        }
        public ActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ForgotPassword(ForgotPasswordViewModel forgotPasswordViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (GRGEntity context = new GRGEntity())
                    {
                        AccountService.ResetPassword(forgotPasswordViewModel.NationalCode, forgotPasswordViewModel.Email);
                    }
                }
                catch (Exception e)
                {

                }
            }

            else
            {
                ViewBag.IsValidModelState = false;
            }

            return View(forgotPasswordViewModel);
        }
    }
}
