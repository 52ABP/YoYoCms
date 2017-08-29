using Abp.Authorization.Users;
using Abp.AutoMapper;
using Abp.BackgroundJobs;
using Abp.Configuration.Startup;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Extensions;
using Abp.Localization;
using Abp.Notifications;
using Abp.Runtime.Session;
using Abp.Threading;
using Abp.Timing;
using Abp.UI;
using Abp.Web.Models;
using Abp.Web.Mvc.Authorization;
using YoYo.Cms.Authorization;
using YoYo.Cms.Authorization.Roles;
using YoYo.Cms.MultiTenancy;
using YoYo.Cms.Web.Controllers.Results;
using YoYo.Cms.Web.Models.Account;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Abp.Authorization;
using YoYo.Cms.Notifications;
using YoYo.Cms.UserManagerment.Users;

namespace YoYo.Cms.Web.Controllers
{
    public class AccountController : CmsControllerBase
    {
        private readonly TenantManager _tenantManager;
        private readonly UserManager _userManager;
        private readonly RoleManager _roleManager;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IMultiTenancyConfig _multiTenancyConfig;
        private readonly LogInManager _logInManager;
        private readonly ILanguageManager _languageManager;

        private readonly INotificationManager _notificationManager;
    
        //短信验证
        private readonly IBackgroundJobManager _backgroundJobManager;

        private readonly IRepository<User, long> _userRepository;



        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        public AccountController(
            TenantManager tenantManager,
            UserManager userManager,
            RoleManager roleManager,
            IUnitOfWorkManager unitOfWorkManager,
            IMultiTenancyConfig multiTenancyConfig,
            LogInManager logInManager,
            ILanguageManager languageManager,
            IBackgroundJobManager backgroundJobManager,
            IRepository<User, long> userRepository, INotificationManager notificationManager)
        {
            _tenantManager = tenantManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _unitOfWorkManager = unitOfWorkManager;
            _multiTenancyConfig = multiTenancyConfig;
            _logInManager = logInManager;
            _languageManager = languageManager;
        
            _backgroundJobManager = backgroundJobManager;
            _userRepository = userRepository;
            _notificationManager = notificationManager;
        }

        #region Login / Logout

        public ActionResult Login(string returnUrl = "")
        {
            if (string.IsNullOrWhiteSpace(returnUrl))
            {
                returnUrl = Request.ApplicationPath;
            }

            return View(
                new LoginFormViewModel
                {
                    ReturnUrl = returnUrl,
                    IsMultiTenancyEnabled = _multiTenancyConfig.IsEnabled
                });
        }

        [HttpPost]
        public async Task<JsonResult> Login(LoginViewModel loginModel, string returnUrl = "", string returnUrlHash = "")
        {
            CheckModelState();

            var loginResult = await GetLoginResultAsync(
                loginModel.UsernameOrEmailAddress,
                loginModel.Password,
                loginModel.TenancyName
                );

            await SignInAsync(loginResult.User, loginResult.Identity, loginModel.RememberMe);

            if (string.IsNullOrWhiteSpace(returnUrl))
            {
                returnUrl = Request.ApplicationPath;
            }

            if (!string.IsNullOrWhiteSpace(returnUrlHash))
            {
                returnUrl = returnUrl + returnUrlHash;
            }

            return Json(new AjaxResponse { TargetUrl = returnUrl });
        }

        private async Task<AbpLoginResult<Tenant, User>> GetLoginResultAsync(string usernameOrEmailAddress, string password, string tenancyName)
        {
            try
            {

                var loginResult = await _logInManager.LoginAsync(usernameOrEmailAddress, password, tenancyName);

                switch (loginResult.Result)
                {
                    case AbpLoginResultType.Success:
                        return loginResult;
                    default:
                        throw CreateExceptionForFailedLoginAttempt(loginResult.Result, usernameOrEmailAddress, tenancyName);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }

        private async Task SignInAsync(User user, ClaimsIdentity identity = null, bool rememberMe = false)
        {
            if (identity == null)
            {
                identity = await _userManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            }

            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties { IsPersistent = rememberMe }, identity);
        }

        private Exception CreateExceptionForFailedLoginAttempt(AbpLoginResultType result, string usernameOrEmailAddress, string tenancyName)
        {
            switch (result)
            {
                case AbpLoginResultType.Success:
                    return new ApplicationException("Don't call this method with a success result!");
                case AbpLoginResultType.InvalidUserNameOrEmailAddress:
                case AbpLoginResultType.InvalidPassword:
                    return new UserFriendlyException(L("LoginFailed"), L("InvalidUserNameOrPassword"));
                case AbpLoginResultType.InvalidTenancyName:
                    return new UserFriendlyException(L("LoginFailed"), L("ThereIsNoTenantDefinedWithName{0}", tenancyName));
                case AbpLoginResultType.TenantIsNotActive:
                    return new UserFriendlyException(L("LoginFailed"), L("TenantIsNotActive", tenancyName));
                case AbpLoginResultType.UserIsNotActive:
                    return new UserFriendlyException(L("LoginFailed"), L("UserIsNotActiveAndCanNotLogin", usernameOrEmailAddress));
                case AbpLoginResultType.UserEmailIsNotConfirmed:
                    return new UserFriendlyException(L("LoginFailed"), "UserEmailIsNotConfirmedAndCanNotLogin");
                case AbpLoginResultType.LockedOut:
                    return new UserFriendlyException(L("LoginFailed"), L("UserLockedOutMessage"));
                default: //Can not fall to default actually. But other result types can be added in the future and we may forget to handle it
                    Logger.Warn("Unhandled login fail reason: " + result);
                    return new UserFriendlyException(L("LoginFailed"));
            }
        }

        public ActionResult Logout()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Login");
        }

        #endregion

        #region Register

        public ActionResult Register()
        {
            return RegisterView(new RegisterViewModel());
        }

        private ActionResult RegisterView(RegisterViewModel model)
        {
            ViewBag.IsMultiTenancyEnabled = _multiTenancyConfig.IsEnabled;

            return View("Register", model);
        }

        [HttpPost]
        [UnitOfWork]
        public virtual async Task<ActionResult> Register(RegisterViewModel model)
        {
            try
            {
                CheckModelState();


                //model.Name = "Name";
         //       model.Surname = "Surname";


                //Get tenancy name and tenant
                if (!_multiTenancyConfig.IsEnabled)
                {
                    model.TenancyName = Tenant.DefaultTenantName;
                }
                else if (model.TenancyName.IsNullOrEmpty())
                {
                    throw new UserFriendlyException(L("TenantNameCanNotBeEmpty"));
                }

                var tenant = await GetActiveTenantAsync(model.TenancyName);

                //Create user
                var user = new User
                {
                    TenantId = tenant.Id,
                    Name = model.Name,
                    Surname = model.Surname,
                    EmailAddress = model.EmailAddress,
                    IsActive = true
                };

                //Get external login info if possible
                ExternalLoginInfo externalLoginInfo = null;
                if (model.IsExternalLogin)
                {
                    externalLoginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
                    if (externalLoginInfo == null)
                    {
                        throw new ApplicationException("Can not external login!");
                    }

                    user.Logins = new List<UserLogin>
                    {
                        new UserLogin
                        {
                            TenantId = tenant.Id,
                            LoginProvider = externalLoginInfo.Login.LoginProvider,
                            ProviderKey = externalLoginInfo.Login.ProviderKey
                        }
                    };

                    if (model.UserName.IsNullOrEmpty())
                    {
                        model.UserName = model.EmailAddress;
                    }

                    model.Password = UserManagerment.Users.User.CreateRandomPassword();

                    if (string.Equals(externalLoginInfo.Email, model.EmailAddress, StringComparison.InvariantCultureIgnoreCase))
                    {
                        user.IsEmailConfirmed = true;
                    }
                }
                else
                {
                    //Username and Password are required if not external login
                    if (model.UserName.IsNullOrEmpty() || model.Password.IsNullOrEmpty())
                    {
                        throw new UserFriendlyException(L("FormIsNotValidMessage"));
                    }
                }

                user.UserName = model.UserName;
                user.Password = new PasswordHasher().HashPassword(model.Password);

                user.EmailAddress = string.Empty;
                //Switch to the tenant
                _unitOfWorkManager.Current.EnableFilter(AbpDataFilters.MayHaveTenant); //TODO: Needed?
                _unitOfWorkManager.Current.SetTenantId(tenant.Id);

                //Add default roles
                user.Roles = new List<UserRole>();
                foreach (var defaultRole in await _roleManager.Roles.Where(r => r.IsDefault).ToListAsync())
                {
                    user.Roles.Add(new UserRole { RoleId = defaultRole.Id });
                }

                //Save user
               

               // _userManager.Create(user);


                try
                {
                    CheckErrors(await _userManager.CreateAsync(user));
                    await _unitOfWorkManager.Current.SaveChangesAsync();
                    await _notificationManager.WelcomeToCmsAsync(user);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }

                //Directly login if possible
                if (user.IsActive)
                {
                    AbpLoginResult<Tenant, User> loginResult;
                    if (externalLoginInfo != null)
                    {
                        loginResult = await _logInManager.LoginAsync(externalLoginInfo.Login, tenant.TenancyName);
                    }
                    else
                    {
                        loginResult = await GetLoginResultAsync(user.UserName, model.Password, tenant.TenancyName);
                    }

                    if (loginResult.Result == AbpLoginResultType.Success)
                    {
                        await SignInAsync(loginResult.User, loginResult.Identity);
                        return Redirect(Url.Action("Index", "Home"));
                    }

                    Logger.Warn("New registered user could not be login. This should not be normally. login result: " + loginResult.Result);
                }

                //If can not login, show a register result page
                return View("RegisterResult", new RegisterResultViewModel
                {
                    TenancyName = tenant.TenancyName,
                    NameAndSurname = user.Name + " " + user.Surname,
                    UserName = user.UserName,
                    EmailAddress = user.EmailAddress,
                    IsActive = user.IsActive
                });
            }
            catch (UserFriendlyException ex)
            {
                ViewBag.IsMultiTenancyEnabled = _multiTenancyConfig.IsEnabled;
                ViewBag.ErrorMessage = ex.Message;

                return View("Register", model);
            }
        }

        #endregion

        #region External Login 扩展登陆

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            return new ChallengeResult(
                provider,
                Url.Action(
                    "ExternalLoginCallback",
                    "Account",
                    new
                    {
                        ReturnUrl = returnUrl
                    })
                );
        }

        public virtual async Task<ActionResult> ExternalLoginCallback(string returnUrl, string tenancyName = "")
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            //Try to find tenancy name
            if (tenancyName.IsNullOrEmpty())
            {
                var tenants = await FindPossibleTenantsOfUserAsync(loginInfo.Login);
                switch (tenants.Count)
                {
                    case 0:
                        return await RegisterView(loginInfo);
                    case 1:
                        tenancyName = tenants[0].TenancyName;
                        break;
                    default:
                        return View("TenantSelection", new TenantSelectionViewModel
                        {
                            Action = Url.Action("ExternalLoginCallback", "Account", new { returnUrl }),
                            Tenants = tenants.MapTo<List<TenantSelectionViewModel.TenantInfo>>()
                        });
                }
            }

            var loginResult = await _logInManager.LoginAsync(loginInfo.Login, tenancyName);

            switch (loginResult.Result)
            {
                case AbpLoginResultType.Success:
                    await SignInAsync(loginResult.User, loginResult.Identity, false);

                    if (string.IsNullOrWhiteSpace(returnUrl))
                    {
                        returnUrl = Url.Action("Index", "Home");
                    }

                    return Redirect(returnUrl);
                case AbpLoginResultType.UnknownExternalLogin:
                    return await RegisterView(loginInfo, tenancyName);
                default:
                    throw CreateExceptionForFailedLoginAttempt(loginResult.Result, loginInfo.Email ?? loginInfo.DefaultUserName, tenancyName);
            }
        }

        private async Task<ActionResult> RegisterView(ExternalLoginInfo loginInfo, string tenancyName = null)
        {
            var name = loginInfo.DefaultUserName;
            var surname = loginInfo.DefaultUserName;

            var extractedNameAndSurname = TryExtractNameAndSurnameFromClaims(loginInfo.ExternalIdentity.Claims.ToList(), ref name, ref surname);

            var viewModel = new RegisterViewModel
            {
                TenancyName = tenancyName,
                EmailAddress = loginInfo.Email,
                Name = name,
                Surname = surname,
                IsExternalLogin = true
            };

            if (!tenancyName.IsNullOrEmpty() && extractedNameAndSurname)
            {
                return await Register(viewModel);
            }

            return RegisterView(viewModel);
        }

        protected virtual async Task<List<Tenant>> FindPossibleTenantsOfUserAsync(UserLoginInfo login)
        {
            List<User> allUsers;
            using (_unitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
            {
                allUsers = await _userManager.FindAllAsync(login);
            }

            return allUsers
                .Where(u => u.TenantId != null)
                .Select(u => AsyncHelper.RunSync(() => _tenantManager.FindByIdAsync(u.TenantId.Value)))
                .ToList();
        }

        private static bool TryExtractNameAndSurnameFromClaims(List<Claim> claims, ref string name, ref string surname)
        {
            string foundName = null;
            string foundSurname = null;

            var givennameClaim = claims.FirstOrDefault(c => c.Type == ClaimTypes.GivenName);
            if (givennameClaim != null && !givennameClaim.Value.IsNullOrEmpty())
            {
                foundName = givennameClaim.Value;
            }

            var surnameClaim = claims.FirstOrDefault(c => c.Type == ClaimTypes.Surname);
            if (surnameClaim != null && !surnameClaim.Value.IsNullOrEmpty())
            {
                foundSurname = surnameClaim.Value;
            }

            if (foundName == null || foundSurname == null)
            {
                var nameClaim = claims.FirstOrDefault(c => c.Type == ClaimTypes.Name);
                if (nameClaim != null)
                {
                    var nameSurName = nameClaim.Value;
                    if (!nameSurName.IsNullOrEmpty())
                    {
                        var lastSpaceIndex = nameSurName.LastIndexOf(' ');
                        if (lastSpaceIndex < 1 || lastSpaceIndex > (nameSurName.Length - 2))
                        {
                            foundName = foundSurname = nameSurName;
                        }
                        else
                        {
                            foundName = nameSurName.Substring(0, lastSpaceIndex);
                            foundSurname = nameSurName.Substring(lastSpaceIndex);
                        }
                    }
                }
            }

            if (!foundName.IsNullOrEmpty())
            {
                name = foundName;
            }

            if (!foundSurname.IsNullOrEmpty())
            {
                surname = foundSurname;
            }

            return foundName != null && foundSurname != null;
        }

        #endregion

        #region Common private methods

        private async Task<Tenant> GetActiveTenantAsync(string tenancyName)
        {
            var tenant = await _tenantManager.FindByTenancyNameAsync(tenancyName);
            if (tenant == null)
            {
                throw new UserFriendlyException(L("ThereIsNoTenantDefinedWithName{0}", tenancyName));
            }

            if (!tenant.IsActive)
            {
                throw new UserFriendlyException(L("TenantIsNotActive", tenancyName));
            }

            return tenant;
        }

        #endregion

        #region Child actions

        /// <summary>
        /// 语言列表
        /// </summary>
        /// <returns></returns>
        [ChildActionOnly]
        public PartialViewResult Languages()
        {
            return PartialView("~/Views/Account/_Languages.cshtml",
                new LanguagesViewModel
                {
                    AllLanguages = _languageManager.GetLanguages(),
                    CurrentLanguage = _languageManager.CurrentLanguage
                });
        }

        #endregion Child actions

        #region 找回密码/重置密码

        public ActionResult FindMyPassword()
        {
            return View();
        }
        public ActionResult FindMyPasswordTypes()
        {
            return View();
        }
        
        public void VerificationImgCode(string Vailcode)
        {
            //获取服务器中的图片验证码
            var sessionCode = Session["accountvalicode"].ToString().ToLower();
            //获取客户回传的图片验证码
            var code = Vailcode.ToLower();
            if (!sessionCode.Equals(code))
                throw new UserFriendlyException("图片验证码错误！");
        }


        #endregion

        #region 测试通知系统


        [AbpMvcAuthorize]
        public async Task<ActionResult> TestNotification( string message = "" , NotificationSeverity severity = NotificationSeverity.Info)
        {
            if (message.IsNullOrEmpty())
            {
                message = "这是一条测试消息 " + Clock.Now;
            }
            await _notificationManager.SendMessageAsync(AbpSession.ToUserIdentifier(), message, severity: severity);
          

            return Content("发送提示信息: " + message);
        }



        #endregion

    }
}