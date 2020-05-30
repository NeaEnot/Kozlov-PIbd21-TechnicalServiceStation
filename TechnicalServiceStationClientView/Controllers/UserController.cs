using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechnicalServiceStationBusinessLogic.BindingModels;
using TechnicalServiceStationBusinessLogic.Interfaces;
using TechnicalServiceStationBusinessLogic.ViewModels;

namespace TechnicalServiceStationClientView.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserLogic logic;

        private readonly int _passwordMaxLength = 255;
        private readonly int _passwordMinLength = 8;

        public UserController(IUserLogic logic)
        {
            this.logic = logic;
        }

        [HttpGet]
        public ActionResult LogIn(string Email, string Password)
        {
            try
            {
                UserViewModel user = logic.Read(new UserBindingModel { Email = Email, Password = Password })?[0];
            }
            catch
            {
                return Redirect("/User/Enter?errMessage=Неправильная почта или пароль");
            }

            return Redirect("/User/Enter/#");
        }

        [HttpPost]
        public ActionResult RegisterOrUpdateData(string Email, string Password)
        {
            UserBindingModel model = new UserBindingModel { Email = Email, Password = Password };

            try
            {
                CheckData(model);
                logic.CreateOrUpdate(model);
            }
            catch (Exception e)
            {
                return Redirect("/User/UserInfo?errMessage=" + e.Message);
            }

            return Redirect("/User/Enter");
        }

        private void CheckData(UserBindingModel model)
        {
            if (!Regex.IsMatch(model.Email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"))
            {
                throw new Exception("Почта должна соответствовать формату user@domain.tld");
            }

            if (model.Password.Length > _passwordMaxLength ||
                model.Password.Length < _passwordMinLength ||
                !Regex.IsMatch(model.Password, @"^((\w+\d+\W+)|(\w+\W+\d+)|(\d+\w+\W+)|(\d+\W+\w+)|(\W+\w+\d+)|(\W+\d+\w+))[\w\d\W]*$"))
            {
                throw new Exception($"Пароль должен быть длиной от {_passwordMinLength} до { _passwordMaxLength } и должен состоять из цифр, букв и небуквенных символов");
            }
        }

        // GET: User/Enter
        [HttpGet]
        public ActionResult Enter(string errMessage)
        {
            ViewData["errMessage"] = errMessage;
            return View();
        }

        // GET: User/UserInfo
        public ActionResult UserInfo(string errMessage)
        {
            ViewData["errMessage"] = errMessage;
            return View();
        }
    }
}