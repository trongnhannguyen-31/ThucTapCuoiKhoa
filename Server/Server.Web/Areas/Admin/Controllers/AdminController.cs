using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Falcon.Web.Mvc.Security;
using Phoenix.Server.Services.MainServices.Auth;
using Phoenix.Server.Web.Areas.Admin.Models.Common;
using Phoenix.Server.Services.Framework;
using Phoenix.Server.Services.MainServices.Users;

namespace Phoenix.Server.Web.Areas.Admin.Controllers
{
    [AllowAnonymous]
    public class AdminController : BaseController
    {
        private readonly UserAuthService _userAuthService;
        private readonly IUserService _userService;
        public AdminController(UserAuthService userAuthService, IUserService userService)
        {
            _userAuthService = userAuthService;
            _userService = userService;
        }

        // GET: Admin/Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login(string returnUrl = "")
        {
            return View(new LoginModel() { ReturnUrl = returnUrl });
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var ticket = await _userAuthService.Validate(model.UserName, model.Password);
                if (ticket.UserId > 0)
                {
                    //log user in
                    ticket.IsPersistent = model.RememberMe;
                    int cookieExpires = 24 * 365; //TODO make configurable
                    ticket.Expiration = DateTime.Now.AddHours(cookieExpires);
                    var encryptedTicket = new WebContextHelper().CreateCookie(ticket);
                    var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                    cookie.HttpOnly = true;
                    if (ticket.IsPersistent)
                    {
                        cookie.Expires = ticket.Expiration;
                    }
                    cookie.Secure = FormsAuthentication.RequireSSL;
                    cookie.Path = FormsAuthentication.FormsCookiePath;
                    if (FormsAuthentication.CookieDomain != null)
                    {
                        cookie.Domain = FormsAuthentication.CookieDomain;
                    }
                    Response.Cookies.Add(cookie);
                    var currentUser = _userService.GetUserById(ticket.UserId);
                    if (currentUser != null)
                    {
                        Session.Add("CurrentUser", currentUser.DisplayName);
                        Session.Add("Role", currentUser.Roles);
                        Session.Add("UserId", currentUser.Id);
                        if (currentUser.AvatarUrl != null)
                        {
                            Session.Add("Avatar", currentUser.AvatarUrl);
                        }
                    }

                    if (Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    return RedirectToAction("Index", "Dashboard", new { area = "Admin" });
                }
                ModelState.AddModelError("", "Mật khẩu hoặc tài khoản không hợp lệ");
            }
            //error
            return View(model);
        }
        public ActionResult LogOut()
        {
            Session.Clear();
            Session.RemoveAll();
            Session.Abandon(); // it will clear the session at the end of request
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Admin");
        }
    }
}